SELECT InvoiceId, InvoiceDate, Total,
DATEPART(quarter, InvoiceDate) AS quarter,
DATEPART(month, InvoiceDate) AS month, 
DATEPART(year, InvoiceDate) AS year, 
DATEPART(day, InvoiceDate) AS day,
DATEDIFF(month, '01/01/2005', InvoiceDate) AS MonthsInBusiness
FROM Invoice
ORDER BY MonthsInBusiness;