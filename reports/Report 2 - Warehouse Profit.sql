 /* 
Report 2: Warehouse Profit
List of the 3 warehouses - with a column showing total delivery revenue and total unit count for each, and total revenue/units average profit.
Order by most revenue to least. 

*/


SELECT w.[Name],
       Sum(d.TotalPrice)                    AS [Total Delivery Revenue],
       Sum(d.UnitCount)                     AS [Total Unit Count],
       Sum(d.TotalPrice) / Sum(d.UnitCount) AS [Average Profit]
FROM   delivery AS d
       JOIN warehouse AS w
         ON d.WarehouseId = w.Id
GROUP  BY w.NAME
ORDER  BY Sum(d.TotalPrice) DESC 