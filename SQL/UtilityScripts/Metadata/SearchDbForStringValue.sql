DECLARE @tableName sysname
DECLARE @columnName sysname
DECLARE @value varchar(100)
DECLARE @sql varchar(2000)
DECLARE @sqlPreamble varchar(100)
DECLARE @minLength int;

SET @value = 'String'
-- *** Set this to the value you're searching for *** --
SET @minLength = LEN(REPLACE(@value, '%', ''));

SET @sqlPreamble = 'IF EXISTS (SELECT 1 FROM '

DECLARE theTableCursor CURSOR FAST_FORWARD FOR 
    SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA = 'dbo' AND TABLE_TYPE = 'BASE TABLE'
    AND TABLE_NAME != 'dtproperties' AND TABLE_NAME != 'sysdiagrams'
ORDER BY TABLE_NAME

OPEN theTableCursor
FETCH NEXT FROM theTableCursor INTO @tableName

WHILE @@FETCH_STATUS = 0 -- spin through Table entries
BEGIN
    DECLARE theColumnCursor CURSOR FAST_FORWARD FOR
        SELECT COLUMN_NAME
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = @tableName
        AND (DATA_TYPE = 'nvarchar' OR DATA_TYPE = 'varchar')
        AND (CHARACTER_MAXIMUM_LENGTH >= @minlength OR CHARACTER_MAXIMUM_LENGTH = -1)
    ORDER BY ORDINAL_POSITION

    OPEN theColumnCursor
    FETCH NEXT FROM theColumnCursor INTO @columnName

    WHILE @@FETCH_STATUS = 0 -- spin through Column entries
    BEGIN
        SET @sql = @tableName + ' (nolock) WHERE ' + @columnName + ' LIKE ''' + @value + 
                   ''') PRINT ''Value found in Table: ' + @tableName + ', Column: ' +  @columnName + ''''
        EXEC (@sqlPreamble + @sql)
        FETCH NEXT FROM theColumnCursor INTO @columnName
    END
    CLOSE theColumnCursor
    DEALLOCATE theColumnCursor

    FETCH NEXT FROM theTableCursor INTO @tableName
END
CLOSE theTableCursor
DEALLOCATE theTableCursor