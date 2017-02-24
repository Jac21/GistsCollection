SELECT FirstName + ' ' + LastName as Name, Email, Country,
InvoiceId, InvoiceDate, Total
FROM Customer
INNER JOIN Invoice ON Invoice.CustomerId = Customer.CustomerId
WHERE Country = 'USA'
ORDER BY Total DESC
OFFSET 1 ROWS;