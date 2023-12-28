/*
	Report 1 - Delivery Detail
	All delivery records - dont show any ID columns - but instead show the Warehouse From name and the Pharmacy To name. 
*/

SELECT w.[Name] AS [Warehouse From],
	   p.[Name] AS [Pharmacy To],
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