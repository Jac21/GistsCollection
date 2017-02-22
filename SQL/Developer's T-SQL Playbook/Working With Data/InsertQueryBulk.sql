INSERT INTO Users(Email, First, Last)
SELECT Email, FirstName, LastName
from Chinook.dbo.Customer