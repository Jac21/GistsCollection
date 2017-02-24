SELECT FirstName + ' ' + LastName as Name, Email, Country,
InvoiceId, InvoiceDate, Total
FROM Customer
INNER JOIN Invoice ON Invoice.CustomerId = Customer.CustomerId
WHERE Country = 'USA'
OR Country = 'Canada'
ORDER BY Country DESC, LastName DESC;