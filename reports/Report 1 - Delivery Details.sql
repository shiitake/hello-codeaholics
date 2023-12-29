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

