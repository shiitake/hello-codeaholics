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