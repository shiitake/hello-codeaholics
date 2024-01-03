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