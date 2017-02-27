SELECT FirstName + ' ' + LastName AS Customer, BillingCountry,
SUM(UnitPrice * Quantity) AS SalesTotal, 
AVG(UnitPrice * Quantity) as AvgPurchase
FROM Invoice
INNER JOIN Customer on Invoice.CustomerId = Customer.CustomerId
INNER JOIN InvoiceLine on InvoiceLine.InvoiceId = Invoice.InvoiceId
GROUP BY FirstName, LastName, BillingCountry
ORDER BY BillingCountry, LastName;