SELECT Workers.FirstName, Workers.LastName,
Bosses.FirstName + ' ' + Bosses.LastName AS Boss
FROM Employee Workers
LEFT JOIN Employee AS Bosses 
ON Workers.ReportsTo = Bosses.EmployeeId