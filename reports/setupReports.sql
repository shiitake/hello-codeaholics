/*
	Report 1 - Delivery Detail
	All delivery records - dont show any ID columns - but instead show the Warehouse From name and the Pharmacy To name. 



*/

CREATE VIEW [dbo].[DeliveryDetail]
as 
SELECT w.[Name] AS [WarehouseFrom],
	   p.[Name] AS [PharmacyTo],
       d.DrugName,
       d.UnitCount,
       d.UnitPrice,
       d.TotalPrice,
       d.DeliveryDate
FROM   delivery AS d
       JOIN warehouse AS w
         ON d.WarehouseId = w.Id
       JOIN pharmacy AS p
         ON d.PharmacyId = p.Id 
GO

IF OBJECT_ID ( 'dbo.Report_WarehouseProfit', 'P' ) IS NOT NULL
    DROP PROCEDURE Report_WarehouseProfit;
GO


CREATE PROCEDURE Report_WarehouseProfit
  @Rank INT = 0,  
  @RangeCount INT = 0
AS
  BEGIN
      /* 
      Report 2: Warehouse Profit
      List of the 3 warehouses - with a column showing total delivery revenue and total unit count for each, and total revenue/units average profit.
      Ordered by most revenue to least. 
      
      Optional parameters:
    @Rank (int) - specify ranking to return.  e.g. top most ranked warehouse, etc.  Negative numbers return worst ranking.    
    @RangeCount (int) allow you to specify how many to return in a range. Default is 0 which returns all.   
      
      
      Examples
      EXEC Report_WareHouseProfit -- returns all warehouses
    EXEC Report_WareHouseProfit @rank = 1  -- returns top ranked warehouse
    EXEC Report_WareHouseProfit @rank = 2  -- returns 2nd ranked warehouse
    EXEC Report_WareHouseProfit @rank = -1  -- returns worst ranked warehouse
    EXEC Report_WareHouseProfit @rank = -1  -- returns 2nd worst ranked warehouse
    EXEC Report_WareHouseProfit @Rank = 1, @RangeCount = 2  -- returns top 2 ranked warehouses
    EXEC Report_WareHouseProfit @Rank = -2, @RangeCount = 2  -- returns bottom 2 ranked warehouses
      
      */
      SET nocount ON;

    DECLARE @ReturnRange bit = 0; 
    IF @RangeCount <> 0 AND @Rank <> 0
    SELECT @ReturnRange = 1;

      IF EXISTS(SELECT *
                FROM   [tempdb].sys.objects
                WHERE  NAME LIKE '%#WarehouseRevenue%')
        BEGIN
            DROP TABLE #warehouserevenue
        END

      CREATE TABLE #warehouserevenue
        (
           NAME           NVARCHAR(50) NOT NULL,
           totalrevenue   DECIMAL(18, 2) NOT NULL,
           totalunitcount INT NOT NULL,
           averageprofit  DECIMAL(18, 2) NOT NULL,
           rankbyrevenue  INT NOT NULL
        )

      INSERT INTO #warehouserevenue
      SELECT w.[name],
             Sum(d.totalprice)                    AS [TotalRevenue],
             Sum(d.unitcount)                     AS [TotalUnitCount],
             Sum(d.totalprice) / Sum(d.unitcount) AS [AverageProfit],
             Row_number()
               OVER (
                 ORDER BY Sum(d.totalprice) DESC) AS RankByRevenue
      FROM   [dbo].delivery AS d
             JOIN [dbo].warehouse AS w
               ON d.warehouseid = w.id
      GROUP  BY w.NAME

      /* By default return all rankings  */
      IF ( @Rank = 0 AND @ReturnRange = 0)
        BEGIN
            SELECT NAME,
                   totalrevenue   AS [Total Delivery Revenue],
                   totalunitcount AS [Total Unit Count],
                   averageprofit  AS [Average Profit],
           rankbyrevenue AS [Rank By Revenue]
            FROM   #warehouserevenue
            ORDER  BY rankbyrevenue ASC
        END
      ELSE
        BEGIN
      /* If ranking is larger than highest rank or less than 0 return the highest rank */
            DECLARE @Maxrank INT = (SELECT Max(rankbyrevenue)
               FROM   #warehouserevenue);

            IF @Rank > @Maxrank                
        SELECT @Rank = @MaxRank;
      IF @Rank < 0
        SELECT @Rank = @Rank + @Maxrank + 1;
      
      IF @ReturnRange = 1
        BEGIN
        DECLARE @Start INT = @Rank;       
        IF @RangeCount <= 0
          SELECT @RangeCount = 3;     
        
        
        
        DECLARE @End INT = @Start + @RangeCount - 1;
        IF @End > @Maxrank
          SELECT @End = @Maxrank
        
        SELECT NAME,
                   totalrevenue   AS [Total Delivery Revenue],
                 totalunitcount AS [Total Unit Count],
               averageprofit  AS [Average Profit],
             rankByRevenue  AS [Rank By Revenue]
        FROM   #warehouserevenue
        WHERE  rankbyrevenue >= @start AND rankbyrevenue <= @end
        ORDER BY rankbyrevenue ASC        

        END
      ELSE
        BEGIN

              SELECT NAME,
                   totalrevenue   AS [Total Delivery Revenue],
                 totalunitcount AS [Total Unit Count],
               averageprofit  AS [Average Profit],
             rankByRevenue  AS [Rank By Revenue]
        FROM   #warehouserevenue
        WHERE  rankbyrevenue = @Rank        
        END
        END

      DROP TABLE #warehouserevenue
  END; 


IF OBJECT_ID ( 'dbo.Report_PharmacistProduction', 'P' ) IS NOT NULL
    DROP PROCEDURE Report_PharmacistProduction;
GO


CREATE PROCEDURE Report_PharmacistProduction
  @Month INT = NULL
AS
  BEGIN
      /*
      Report 3: Pharmacist Production
      List all Pharmacists each with Pharmacy Name they work at, name of their primary drug, total unit count that their pharmacy sold of their primary drug, and total sold not of their primary drug.
      
      Optional parameters:
        @Month (int) - specify month to return.
        
                      
      Examples:
    EXEC Report_PharmacistProduction  -- returns pharmacists for the entire year-to-date
    EXEC Report_PharmacistProduction  @month = 1  -- returns pharmacists for the month of January
            
      
      */
      SET nocount ON;

      IF EXISTS(SELECT *
                FROM   [tempdb].sys.objects
                WHERE  NAME LIKE '%#drugcount%')
        BEGIN
            DROP TABLE #drugcount
        END

      CREATE TABLE #drugcount
        (
           pharmacyid INT NOT NULL,
           drugname   NVARCHAR(50) NOT NULL,
           unitcount  INT NOT NULL
        )

      IF @Month IS NOT NULL
         AND @Month > 0
         AND @Month < 13
        BEGIN
            INSERT INTO #drugcount
            SELECT p.id             AS [PharmacyId],
                   d.drugname       AS [DrugName],
                   Sum(d.unitcount) AS [UnitCount]
            FROM   delivery AS d
                   JOIN pharmacy AS p
                     ON d.pharmacyid = p.id
            WHERE  Month(d.deliverydate) = @Month
            GROUP  BY p.id,
                      d.drugname
        END
      ELSE
        BEGIN
            INSERT INTO #drugcount
            SELECT p.id             AS [PharmacyId],
                   d.drugname       AS [DrugName],
                   Sum(d.unitcount) AS [UnitCount]
            FROM   delivery AS d
                   JOIN pharmacy AS p
                     ON d.pharmacyid = p.id
            WHERE  Year(d.deliverydate) = Year(Getdate())
            GROUP  BY p.id,
                      d.drugname
        END

      SELECT pt.firstname + ' ' + pt.lastname AS [Pharmacist],
             p.NAME                           AS [Pharmacy],
             pt.primarydrugsold               AS [Primary Drug],
             Sum(d.unitcount)                 AS [Primary Drug Unit Count],
             d2.totalunits - Sum(d.unitcount) AS [Other Drugs Unit Count]
      FROM   pharmacist AS pt
             JOIN pharmacy AS p
               ON pt.pharmacyid = p.id
             JOIN #drugcount d
               ON pt.pharmacyid = d.pharmacyid
             JOIN (SELECT pharmacyid,
                          Sum(unitcount) AS [TotalUnits]
                   FROM   #drugcount
                   GROUP  BY pharmacyid) AS d2
               ON pt.pharmacyid = d2.pharmacyid
      GROUP  BY p.NAME,
                pt.firstname,
                pt.lastname,
                pt.primarydrugsold,
                d.drugname,
                d2.totalunits
      HAVING d.drugname = pt.primarydrugsold
      ORDER  BY pt.firstname,
                p.NAME

      DROP TABLE #drugcount
  END 


IF OBJECT_ID ( 'dbo.Report_MostDemandedDrug', 'P' ) IS NOT NULL
    DROP PROCEDURE Report_MostDemandedDrug;
GO

  CREATE PROCEDURE Report_MostDemandedDrug
  @Rank INT = NULL
AS
BEGIN
  /*
  Report 4 - Most Demanded Drug By Pharmacy

  Optional parameters:
    @Rank (int) - specify ranking to return.  e.g. top most purchased medince.       
      
    Examples
    EXEC Report_MostDemandedDrug -- returns top drug for all pharmacies
    EXEC Report_MostDemandedDrug @rank = 2  -- returns 2nd most popular drug


  */
  SET nocount ON;

  IF EXISTS(SELECT * FROM [tempdb].sys.objects WHERE name LIKE '%#drugcount2%') 
  BEGIN
    DROP TABLE #drugcount2
  END


  CREATE TABLE #drugcount2
    (
     PharmacyId INT,
     DrugName   NVARCHAR(50),
     UnitCount  INT
    )

  INSERT INTO #drugcount2
  SELECT p.Id             AS [PharmacyId],
       d.DrugName       AS [DrugName],
       Sum(d.UnitCount) AS [UnitCount]
  FROM   delivery AS d
       JOIN pharmacy AS p
       ON d.pharmacyId = p.Id
  GROUP  BY p.Id,
        d.DrugName 

  IF @Rank IS NULL OR @Rank <= 0
    SELECT @Rank = 1;
  
  SELECT 
  p.[Name] as [Pharmacy], 
  d.DrugName as [Drug Name], 
  d.UnitCount as [Unit Count],  
  d.rn as [Rank]
  FROM (
    SELECT PharmacyId, 
        DrugName, 
        UnitCount,
      ROW_NUMBER() OVER (
        PARTITION BY PharmacyID ORDER BY UnitCount DESC) as rn
    FROM #drugcount2
  ) as d
  join Pharmacy as p
  on d.PharmacyId = p.Id
  WHERE d.rn = @Rank


  DROP TABLE #drugcount2;

END