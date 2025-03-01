CREATE TABLE [States]
(
    [Id]                UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    [Status]            INT,
    [Priority]          INT,
    [Description]       NVARCHAR(MAX) NOT NULL,
    [CardId]            UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Cards (Id) ON DELETE CASCADE,
    [CreatedAtUtc]      DATETIME      NOT NULL       DEFAULT GETUTCDATE(),
    [CreatedBy]         NVARCHAR(100) NOT NULL,
    [LastModifiedAtUtc] DATETIME                     DEFAULT NULL,
    [LastModifiedBy]    NVARCHAR(100)                DEFAULT NULL
)