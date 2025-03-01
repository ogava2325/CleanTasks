CREATE TABLE [Users]
(
    [Id]           UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    [FirstName]    NVARCHAR(100)        NOT NULL,
    [LastName]     NVARCHAR(100)        NOT NULL,
    [Email]        NVARCHAR(100) UNIQUE NOT NULL,
    [PasswordHash] NVARCHAR(MAX)        NOT NULL
)