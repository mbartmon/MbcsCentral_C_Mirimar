set nocount on
declare @columnName nvarchar(500)
declare @tableName nvarchar(500)
declare @select nvarchar(500)
declare @sql nvarchar(500)
-- check if the Temp table already exists
if OBJECT_ID('tempdb..#LocalTempTable') is  null
	Begin
		CREATE TABLE #LocalTempTable(
			TableName varchar(150),
			ColumnName varchar(150))
		end
else 
	begin 
		truncate table #LocalTempTable; 
	end
-- Build a select for each of the columns in the database.  That checks for nulls
DECLARE check_cursor CURSOR FOR 
select column_name, table_name, concat(' Select ''',column_name,''',''',table_name,''' from ',table_name,' where [',COLUMN_NAME,'] is null')
from INFORMATION_SCHEMA.COLUMNS

OPEN check_cursor

FETCH NEXT FROM check_cursor 
INTO @columnName, @tableName,@select

WHILE @@FETCH_STATUS = 0
BEGIN
-- Insert it if there if it exists.
set @sql = 'insert into #LocalTempTable (ColumnName, TableName)' + @select
print @sql

-- Run the statment
exec( @sql)

FETCH NEXT FROM check_cursor 
INTO @columnName, @tableName,@select
end
CLOSE check_cursor;
DEALLOCATE check_cursor;


select distinct ColumnName, TableName from #LocalTempTable 