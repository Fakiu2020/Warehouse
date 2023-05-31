DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Separation]') AND [c].[name] = N'CreationTime');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Separation] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Separation] ALTER COLUMN [CreationTime] datetime2 NOT NULL;
ALTER TABLE [Separation] ADD DEFAULT '2019-07-18T17:00:28.0196545-03:00' FOR [CreationTime];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'CreationTime');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Product] ALTER COLUMN [CreationTime] datetime2 NOT NULL;
ALTER TABLE [Product] ADD DEFAULT '2019-07-18T17:00:28.0150989-03:00' FOR [CreationTime];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Plant]') AND [c].[name] = N'CreationTime');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Plant] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Plant] ALTER COLUMN [CreationTime] datetime2 NOT NULL;
ALTER TABLE [Plant] ADD DEFAULT '2019-07-18T17:00:28.0086822-03:00' FOR [CreationTime];

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Municipio]') AND [c].[name] = N'CreationTime');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Municipio] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Municipio] ALTER COLUMN [CreationTime] datetime2 NOT NULL;
ALTER TABLE [Municipio] ADD DEFAULT '2019-07-18T17:00:28.0235839-03:00' FOR [CreationTime];

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Category]') AND [c].[name] = N'CreationTime');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Category] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Category] ALTER COLUMN [CreationTime] datetime2 NOT NULL;
ALTER TABLE [Category] ADD DEFAULT '2019-07-18T17:00:28.0270284-03:00' FOR [CreationTime];

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200418235516_removeDefaultValueCreationTime';

GO

