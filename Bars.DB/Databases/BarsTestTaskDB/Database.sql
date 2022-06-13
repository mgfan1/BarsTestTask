DECLARE @databaseName sysname = 'BarsTestTaskDB';

IF NOT EXISTS 
(
	SELECT 1 
	FROM sys.databases 
	WHERE [name] = @databaseName
)
BEGIN
    EXEC ('CREATE DATABASE [' + @databaseName + ']');
    PRINT 'Database [' + @databaseName + '] has been created';
END;
GO

USE BarsTestTaskDB;
