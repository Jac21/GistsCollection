SELECT 
SUM(Total) AS AllTimeSales,
AVG(Total) AS AvgSale,
COUNT(Total) AS SalesCount,
MIN(Total) AS Smallest,
MAX(Total) AS Bigges
FROM Invoice;