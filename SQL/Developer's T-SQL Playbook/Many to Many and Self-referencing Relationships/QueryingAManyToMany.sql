SELECT Email, Name
FROM Users
INNER JOIN UserRole on Users.Id = UserRole.UserId
INNER JOIN Role on Role.Id = UserRole.RoleId;