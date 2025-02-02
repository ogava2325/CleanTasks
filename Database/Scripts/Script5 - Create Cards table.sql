CREATE TABLE [Cards] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    [Title] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(MAX) NOT NULL,
    [StateId] UNIQUEIDENTIFIER,
    [CreatedAtUtc] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [CreatedBy] NVARCHAR(100) NOT NULL,
    [LastModifiedAtUtc] DATETIME DEFAULT NULL,
    [LastModifiedBy] NVARCHAR(100) DEFAULT NULL
)