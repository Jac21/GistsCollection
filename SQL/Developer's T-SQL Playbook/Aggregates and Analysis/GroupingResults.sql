SELECT BillingCountry,
SUM(Total) AS AllTimeSales
FROM Invoice
GROUP BY BillingCountry;