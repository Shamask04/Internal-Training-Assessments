IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Kings] (
    [Id] int NOT NULL IDENTITY,
    [KingName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Kings] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Militaries] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [KingId] int NOT NULL,
    CONSTRAINT [PK_Militaries] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Militaries_Kings_KingId] FOREIGN KEY ([KingId]) REFERENCES [Kings] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Quotes] (
    [Id] int NOT NULL IDENTITY,
    [Text] nvarchar(max) NOT NULL,
    [MilitaryId] int NOT NULL,
    CONSTRAINT [PK_Quotes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Quotes_Militaries_MilitaryId] FOREIGN KEY ([MilitaryId]) REFERENCES [Militaries] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Militaries_KingId] ON [Militaries] ([KingId]);
GO

CREATE INDEX [IX_Quotes_MilitaryId] ON [Quotes] ([MilitaryId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251228071205_init', N'8.0.0');
GO

COMMIT;
GO

