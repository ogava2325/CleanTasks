CREATE TABLE [Comments]
(
    [Id]                UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    [Content]           NVARCHAR(MAX),
    [CardId]            UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Cards (Id),
    [UserId]            UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Users (Id),
    [CreatedAtUtc]      DATETIME      NOT NULL       DEFAULT GETUTCDATE(),
    [CreatedBy]         NVARCHAR(100) NOT NULL,
    [LastModifiedAtUtc] DATETIME                     DEFAULT NULL,
    [LastModifiedBy]    NVARCHAR(100)                DEFAULT NULL
)
