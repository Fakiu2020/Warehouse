DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Separation]') AND [c].[name] = N'MeasuresUnit');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Separation] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Separation] DROP COLUMN [MeasuresUnit];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Separation]') AND [c].[name] = N'SeparationDate');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Separation] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Separation] ALTER COLUMN [SeparationDate] datetime2 NOT NULL;
ALTER TABLE [Separation] ADD DEFAULT '2021-06-09T19:33:30.8120987-03:00' FOR [SeparationDate];

GO

ALTER TABLE [Product] ADD [MeasuresUnit] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210609223330_addMeasureUnitToProduct', N'2.2.2-servicing-10034');

GO

