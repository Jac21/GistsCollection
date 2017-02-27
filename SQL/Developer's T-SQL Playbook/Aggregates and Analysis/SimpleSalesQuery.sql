SELECT FirstName + ' ' + LastName AS Customer, BillingCountry,
SUM(Total) AS SalesTotal, AVG(Total) as AvgPurchase
FROM Invoice
INNER JOIN Customer on Invoice.CustomerId = Customer.CustomerId
GROUP BY FirstName, LastName, BillingCountry
ORDER BY BillingCountry, LastName;