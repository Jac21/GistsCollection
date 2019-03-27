SELECT *
  FROM [sys].[indexes];

SELECT *
  FROM [sys].[index_columns];

SELECT [TableName] = [t].[name], 
       [IndexName] = [ind].[name], 
       [IndexId] = [ind].[index_id], 
       [ColumnId] = [ic].[index_column_id], 
       [ColumnName] = [col].[name], 
       [ind].*, 
       [ic].*, 
       [col].*
  FROM [sys].[indexes] AS [ind]
       INNER JOIN [sys].[index_columns] AS [ic] ON [ind].object_id = [ic].object_id AND [ind].[index_id] = [ic].[index_id]
       INNER JOIN [sys].[columns] AS [col] ON [ic].object_id = [col].object_id AND [ic].[column_id] = [col].[column_id]
       INNER JOIN [sys].[tables] AS [t] ON [ind].object_id = [t].object_id
 WHERE [ind].[is_primary_key] = 0 AND [ind].[is_unique] = 0 AND [ind].[is_unique_constraint] = 0 AND [t].[is_ms_shipped] = 0
ORDER BY [t].[name], 
         [ind].[name], 
         [ind].[index_id], 
         [ic].[index_column_id];