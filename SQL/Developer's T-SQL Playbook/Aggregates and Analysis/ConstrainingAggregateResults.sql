SELECT BillingCountry,
SUM(UnitPrice * Quantity) AS SalesTotal
FROM Invoice
INNER JOIN InvoiceLine on InvoiceLine.InvoiceId = Invoice.InvoiceId
WHERE BillingCountry IN('Germany', 'Argentina', 'United Kingdom')
GROUP BY BillingCountry HAVING SUM(UnitPrice * Quantity) > 40
ORDER BY BillingCountry;