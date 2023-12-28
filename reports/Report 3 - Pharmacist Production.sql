 /*
 
Report 3: Pharmacist Production
List all Pharmacists each with Pharmacy Name they work at, name of their primary drug, total unit count that their pharmacy sold of their primary drug, and total sold not of their primary drug. 

*/

IF EXISTS(SELECT * FROM [tempdb].sys.objects WHERE name LIKE '%#drugcount%') 
BEGIN
	DROP TABLE #drugcount
END


CREATE TABLE #drugcount
  (
     PharmacyId INT,
     DrugName   NVARCHAR(50),
     UnitCount  INT
  )

INSERT INTO #drugcount
SELECT p.Id             AS [PharmacyId],
       d.DrugName       AS [DrugName],
       Sum(d.UnitCount) AS [UnitCount]
FROM   delivery AS d
       JOIN pharmacy AS p
         ON d.pharmacyId = p.Id
GROUP  BY p.Id,
          d.DrugName 


SELECT pt.FirstName + ' ' + pt.LastName AS [Pharmacist],
       p.NAME                           AS [Pharmacy],
       pt.PrimaryDrugSold               AS [Primary Drug],
       Sum(d.UnitCount)                 AS [Primary Drug Unit Count],
       d2.TotalUnits - Sum(d.UnitCount) AS [Other Drugs Unit Count]
FROM   pharmacist AS pt
       JOIN pharmacy AS p
         ON pt.PharmacyId = p.Id
       JOIN #drugcount d
         ON pt.PharmacyId = d.PharmacyId
       JOIN (SELECT PharmacyId,
                    Sum(UnitCount) AS [TotalUnits]
             FROM   #drugcount
             GROUP  BY PharmacyId) AS d2
         ON pt.PharmacyId = d2.PharmacyId
GROUP  BY p.NAME,
          pt.Firstname,
          pt.LastName,
          pt.PrimaryDrugSold,
          d.DrugName,
          d2.TotalUnits
HAVING d.DrugName = pt.PrimaryDrugSold
ORDER  BY pt.FirstName,
          p.NAME 

DROP TABLE #drugcount
