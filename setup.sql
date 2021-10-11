CREATE DATABASE InAndOut;
GO
USE InAndOut;

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

CREATE TABLE [Items] (
    [Id] int NOT NULL IDENTITY,
    [Borrower] nvarchar(max) NULL,
    CONSTRAINT [PK_Items] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210706045904_addItemsDatabase', N'5.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Items] ADD [ItemName] nvarchar(max) NULL;
GO

ALTER TABLE [Items] ADD [Lender] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210706141239_addLenderNameColumns', N'5.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Expenses] (
    [Id] int NOT NULL IDENTITY,
    [ExpenseName] nvarchar(max) NULL,
    [Amount] int NOT NULL,
    CONSTRAINT [PK_Expenses] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210706220418_addExpenses', N'5.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Expenses]') AND [c].[name] = N'ExpenseName');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Expenses] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Expenses] ALTER COLUMN [ExpenseName] nvarchar(max) NOT NULL;
ALTER TABLE [Expenses] ADD DEFAULT N'' FOR [ExpenseName];
GO

CREATE TABLE [ExpenseTypes] (
    [Id] int NOT NULL IDENTITY,
    [ExpenseTypeName] nvarchar(max) NULL,
    CONSTRAINT [PK_ExpenseTypes] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210708010515_addExpenseType', N'5.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ExpenseTypes]') AND [c].[name] = N'ExpenseTypeName');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ExpenseTypes] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ExpenseTypes] ALTER COLUMN [ExpenseTypeName] nvarchar(max) NOT NULL;
ALTER TABLE [ExpenseTypes] ADD DEFAULT N'' FOR [ExpenseTypeName];
GO

ALTER TABLE [Expenses] ADD [ExpenseTypeId] int NOT NULL DEFAULT 0;
GO

CREATE INDEX [IX_Expenses_ExpenseTypeId] ON [Expenses] ([ExpenseTypeId]);
GO

ALTER TABLE [Expenses] ADD CONSTRAINT [FK_Expenses_ExpenseTypes_ExpenseTypeId] FOREIGN KEY ([ExpenseTypeId]) REFERENCES [ExpenseTypes] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210708040023_addExpenseTypeFk', N'5.0.7');
GO

COMMIT;
GO

