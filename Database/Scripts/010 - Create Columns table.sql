CREATE TABLE [Columns]
(
    [Id]                UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    [Title]             NVARCHAR(255)    NOT NULL,
    [ProjectId]         UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Projects (Id) ON DELETE CASCADE,
    [CreatedAtUtc]      DATETIME         NOT NULL    DEFAULT GETUTCDATE(),
    [CreatedBy]         NVARCHAR(100)    NOT NULL,
    [LastModifiedAtUtc] DATETIME                     DEFAULT NULL,
    [LastModifiedBy]    NVARCHAR(100)                DEFAULT NULL
);

ALTER TABLE Cards
    ADD CONSTRAINT FK_Cards_Columns
        FOREIGN KEY (ColumnId) REFERENCES Columns(Id) ON DELETE CASCADE;