SELECT FirstName, LastName,
(SELECT FirstName + ' ' + LastName 
	FROM Employee Bosses 
	WHERE Employee.ReportsTo = Bosses.EmployeeId) AS Boss
FROM Employee;