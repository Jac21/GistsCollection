SELECT FirstName + ' ' + LastName as Name, Email, Country,
InvoiceId, InvoiceDate, Total
FROM Customer
INNER JOIN Invoice ON Invoice.CustomerId = Customer.CustomerId
WHERE Country IN ('USA', 'Canada', 'Argentina')
AND Total > 5
ORDER BY Country, LastName;