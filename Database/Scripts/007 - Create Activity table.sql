CREATE TABLE [Activity]
(
    [Id]                UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    [Description]       NVARCHAR(255),
    [UserId]            UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Users (Id),
    [CardId]            UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Cards (Id),
    [CreatedAtUtc]      DATETIME      NOT NULL       DEFAULT GETUTCDATE(),
    [CreatedBy]         NVARCHAR(100) NOT NULL,
    [LastModifiedAtUtc] DATETIME                     DEFAULT NULL,
    [LastModifiedBy]    NVARCHAR(100)                DEFAULT NULL
)