CREATE TABLE Users(
	Id INTEGER PRIMARY KEY IDENTITY(1,1),
	email VARCHAR(50) NOT NULL
);

CREATE TABLE Role(
	Id INTEGER PRIMARY KEY IDENTITY(1,1),
	Name VARCHAR(50)
);

CREATE TABLE UserRole(
	UserId INTEGER REFERENCES Users(Id) ON DELETE CASCADE,
	RoleId INTEGER REFERENCES Role(Id) ON DELETE CASCADE,
	PRIMARY KEY(UserId, RoleId)
);