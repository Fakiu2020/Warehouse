ALTER TABLE [Plant] ADD [Latitude] bigint NOT NULL DEFAULT CAST(0 AS bigint);

GO

ALTER TABLE [Plant] ADD [Longitude] bigint NOT NULL DEFAULT CAST(0 AS bigint);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210426202653_AddLatLongToPlant', N'2.2.2-servicing-10034');

GO

