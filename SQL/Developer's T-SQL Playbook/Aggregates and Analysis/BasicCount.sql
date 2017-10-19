SELECT BillingCountry, COUNT(*) AS CountOf 
FROM Invoice
GROUP BY BillingCountry
HAVING COUNT(*)>1
ORDER BY CountOf DESC