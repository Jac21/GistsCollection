SELECT Invoice.InvoiceId,InvoiceDate,UnitPrice,Quantity
FROM Invoice
INNER JOIN InvoiceLine 
ON InvoiceLine.InvoiceId = Invoice.InvoiceId;