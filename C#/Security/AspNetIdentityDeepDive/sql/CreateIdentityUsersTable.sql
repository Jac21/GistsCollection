USE [MyIdentityDatabase]
GO

CREATE TABLE [dbo].[MyIdentityUsers](
    [Id] [nvarchar](450) NOT NULL,
    [UserName] [nvarchar](256) NULL,
    [NormalizedUserName] [nvarchar](256) NULL,
    [PasswordHash] [nvarchar](max) NULL,
CONSTRAINT [PK_MyIdentityUsers] PRIMARY KEY CLUSTERED(
    [Id] ASC
))
GO

SELECT * FROM MyIdentityUsers