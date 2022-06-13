DECLARE @schema sysname = 'dbo'
,@GetContractProcName sysname = 'GetListContracts'

IF NOT EXISTS
(
	SELECT 1 
	FROM sys.objects 
	WHERE OBJECT_ID = OBJECT_ID(N'[' + @schema + '].[' + @GetContractProcName + ']')
)
BEGIN
	EXEC('CREATE PROCEDURE [' + @schema + '].[' + @GetContractProcName + '] AS RAISERROR(''Not implemented.'', 16, 3);')
END;
GO

ALTER PROCEDURE [dbo].[GetListContracts]
AS BEGIN
SET NOCOUNT ON
	SELECT c.[Number]
			,c.[Date]
			,c.[LastModifiedDate]
			,CAST(IIF(DATEDIFF(DAY, c.LastModifiedDate, GETDATE()) <= 60, 1, 0) AS BIT) [IsActual]
	FROM [dbo].Contracts c
END
GO
