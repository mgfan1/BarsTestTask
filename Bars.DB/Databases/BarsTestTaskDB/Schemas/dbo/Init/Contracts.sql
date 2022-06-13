SET IDENTITY_INSERT dbo.Contracts ON

MERGE INTO dbo.Contracts AS Target
USING (VALUES
	 (1, '2022-06-06 12:00:00', '2022-06-06 12:00:00')
	,(2, '2022-05-05 13:00:00', '2022-05-05 13:00:00')
	,(3, '2022-04-04 14:00:00', '2022-04-04 14:00:00')
	,(4, '2022-03-03 15:00:00', '2022-03-01 15:00:00')) AS Source ([Number], [Date], [LastModifiedDate])
ON (Target.[Number] = Source.[Number])
WHEN NOT MATCHED BY TARGET THEN
	INSERT(
		 [Number]
		,[Date]
		,[LastModifiedDate])
	VALUES(
		 Source.[Number]
		,Source.[Date]
		,Source.[LastModifiedDate])

WHEN MATCHED AND (
	Source.[Date] <> Target.[Date] OR
	Source.[LastModifiedDate] <> Target.[LastModifiedDate]
	) THEN
	UPDATE SET
		[Date] = Source.[Date]
		,[LastModifiedDate] = Source.[LastModifiedDate]

OUTPUT 'Contracts' as 'Table', $action, Inserted.*;
GO

SET IDENTITY_INSERT dbo.Contracts OFF
GO
