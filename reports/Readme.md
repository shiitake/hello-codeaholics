## Reports


#### Setup
To install the reporting sprocs and views you can run the `setupReports.sql` script in SSMS.  NOTE: This requires that your database has been setup per the database [Readme](../database/Readme.md). 


### Report 1 - Delivery Detail

All delivery records - dont show any ID columns - but instead show the Warehouse From name and the Pharmacy To name. 

`SELECT * FROM DeliveryDetail`


### Report 2 -  Warehouse Profit

List of the warehouses - with a column showing total delivery revenue and total unit count for each, and total revenue/units average profit.
Ordered by most revenue to least. 

##### Optional parameters 
* Rank (int) - specify ranking to return.  e.g. top most ranked warehouse, etc.  Negative numbers return worst ranking.	  
* RangeCount (int) allow you to specify how many to return in a range. Default is 0 which returns all.   


##### Examples
`EXEC Report_WareHouseProfit -- returns all warehouses`  
`EXEC Report_WareHouseProfit @rank = 1  -- returns top ranked warehouse`  
`EXEC Report_WareHouseProfit @rank = 2  -- returns 2nd ranked warehouse`  
`EXEC Report_WareHouseProfit @rank = -1  -- returns worst ranked warehouse`  
`EXEC Report_WareHouseProfit @rank = -1  -- returns 2nd worst ranked warehouse`  
`EXEC Report_WareHouseProfit @Rank = 1, @RangeCount = 2  -- returns top 2 ranked warehouses`  
`EXEC Report_WareHouseProfit @Rank = -2, @RangeCount = 2  -- returns bottom 2 ranked warehouses`  


### Report 3 - Pharmacist Production

List all Pharmacists each with Pharmacy Name they work at, name of their primary drug, total unit count that their pharmacy sold of their primary drug, and total sold not of their primary drug. 

##### Optional parameters 
* Month (int) - specify month to return. 


##### Examples
`EXEC Report_PharmacistProduction  -- returns pharmacists for the entire year-to-date`  
`EXEC Report_PharmacistProduction  @month = 1  -- returns pharmacists for the month of January`  


### Report 4 - 	Most Demanded Drug By Pharmacy

List the most demanded drug sold by each pharmacy. 

##### Optional parameters 
* Rank (int) - specify ranking to return.  e.g. top most purchased medince.

##### Examples
`EXEC Report_MostDemandedDrug -- returns top drug for all pharmacies`  
`EXEC Report_MostDemandedDrug @rank = 2  -- returns 2nd most popular drug`  


