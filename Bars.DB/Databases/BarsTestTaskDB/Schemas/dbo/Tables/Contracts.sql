DECLARE @schema sysname = 'dbo'
,@ContractTableName sysname = 'Contracts';

IF NOT EXISTS
(
    SELECT 1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_SCHEMA = @schema
          AND TABLE_NAME = @ContractTableName
)
BEGIN
    CREATE TABLE dbo.Contracts
    (
        [Number]			INT IDENTITY (1, 1) NOT NULL
        ,[Date]				DATETIME NOT NULL
		,[LastModifiedDate] DATETIME NOT NULL
        CONSTRAINT PK_Number PRIMARY KEY CLUSTERED ([Number] ASC)
    );
END;
GO
