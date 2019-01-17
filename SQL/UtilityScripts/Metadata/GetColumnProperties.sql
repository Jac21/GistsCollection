SELECT

    --col.TABLE_CATALOG AS [Database],
    --col.TABLE_SCHEMA AS Owner,
    --col.TABLE_NAME AS [Object],
    [col].[COLUMN_NAME] AS [Column],
    [col].[ORDINAL_POSITION] AS [Position],
    [col].[DATA_TYPE] AS [Data Type],
    CASE [col].[CHARACTER_MAXIMUM_LENGTH] WHEN ISNULL([col].[CHARACTER_MAXIMUM_LENGTH], 0)
     THEN CAST([col].[CHARACTER_MAXIMUM_LENGTH] AS NVARCHAR)
     ELSE ''
                     END AS [Max Length],
    CASE [pk].[is_primary_key] WHEN CAST(ISNULL([pk].[is_primary_key], 0) AS BIT)
     THEN 'True'
     ELSE ''
                            END AS [Is Primary Key],
    CASE(COLUMNPROPERTY(OBJECT_ID('['+[col].[TABLE_SCHEMA]+'].['+[col].[TABLE_NAME]+']'), [col].[COLUMN_NAME], 'AllowsNull')) WHEN 1
     THEN 'True'
     ELSE ''
                                   END AS [Nulls Permitted],
    -- Copy statement above to list other column properties, adjust c.COLUNN_NAME, 'Desired Value' and label 'AS [Whatever]'
    -- COLUMNPROPERTY (Transact-SQL) - https://msdn.microsoft.com/en-us/library/ms174968.aspx
    CASE [col].[COLUMN_DEFAULT] WHEN CAST(ISNULL([col].[COLUMN_DEFAULT], 0) AS NVARCHAR)
     THEN [col].[COLUMN_DEFAULT]
     ELSE ''
                                          END AS [Default Setting],
    CASE [col].[DATETIME_PRECISION] WHEN ISNULL([col].[DATETIME_PRECISION], 0)
     THEN CAST([col].[DATETIME_PRECISION] AS NVARCHAR)
     ELSE ''
                                                 END AS [Date Time Precision]
FROM [INFORMATION_SCHEMA].[COLUMNS] AS [col]
    LEFT JOIN (SELECT SCHEMA_NAME([o].schema_id) AS [TABLE_SCHEMA],
        [o].[name] AS [TABLE_NAME],
        [c].[name] AS [COLUMN_NAME],
        [i].[is_primary_key]
    FROM [sys].[indexes] AS [i]
        JOIN [sys].[index_columns] AS [ic] ON [i].object_id = [ic].object_id AND [i].[index_id] = [ic].[index_id]
        JOIN [sys].[objects] AS [o] ON [i].object_id = [o].object_id
        LEFT JOIN [sys].[columns] AS [c] ON [ic].object_id = [c].object_id AND [c].[column_id] = [ic].[column_id]
    WHERE [i].[is_primary_key] = 1
                 ) AS [pk] ON ([col].[TABLE_NAME] = [pk].[TABLE_NAME]) AND ([col].[TABLE_SCHEMA] = [pk].[TABLE_SCHEMA]) AND ([col].[COLUMN_NAME] = [pk].[COLUMN_NAME])
WHERE [col].[TABLE_NAME] = 'Table'
-- Table or View 
ORDER BY [Position];