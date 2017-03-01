SELECT InvoiceId, InvoiceDate, Total,
DATEPART(quarter, InvoiceDate) AS quarter,
DATEPART(month, InvoiceDate) AS month, 
DATEPART(year, InvoiceDate) AS year, 
DATEPART(day, InvoiceDate) AS day
FROM Invoice;