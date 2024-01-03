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