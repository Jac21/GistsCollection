-- Find what SQL Server thinks are 'missing indexes'
-- These should almost never just be blindly applied. You need to look at the info in the context of already existing indexes (sometimes the right thing to do is 
-- tweak an existing index with an INCLUDE column). Sometimes the thing it wants to index is too big (like a full name) or something that changes frequently 
-- (so killing the performance of updates).

SELECT [migs].[avg_total_user_cost] * ([migs].[avg_user_impact] / 100.0) * ([migs].[user_seeks] + [migs].[user_scans]) AS [improvement_measure], 
       ([migs].[avg_total_user_cost] * [migs].[avg_user_impact] * ([migs].[user_seeks] + [migs].[user_scans]) ) AS [cumulative_impact], 
       OBJECT_NAME(OBJECT_ID) AS [TableName], 
       'CREATE INDEX [missing_index_'+CONVERT(VARCHAR, [mig].[index_group_handle])+'_'+CONVERT(VARCHAR, [mid].[index_handle])+'_'+LEFT(PARSENAME([mid].statement, 1), 32)+']'+' ON '+[mid].statement+' ('+ISNULL([mid].[equality_columns], '')+CASE WHEN [mid].[equality_columns] IS NOT NULL AND [mid].[inequality_columns] IS NOT NULL
                                                                                                                                                                                                                                                    THEN ','
                                                                                                                                                                                                                                                    ELSE ''
                                                                                                                                                                                                                                               END+ISNULL([mid].[inequality_columns], '')+')'+ISNULL(' INCLUDE ('+[mid].[included_columns]+')', '') AS [create_index_statement], 
       [migs].*, 
       [mid].[database_id], 
       [mid].[object_id]
  FROM [sys].[dm_db_missing_index_groups] AS [mig]
       INNER JOIN [sys].[dm_db_missing_index_group_stats] AS [migs] ON [migs].[group_handle] = [mig].[index_group_handle]
       INNER JOIN [sys].[dm_db_missing_index_details] AS [mid] ON [mig].[index_handle] = [mid].[index_handle]
 WHERE [migs].[avg_total_user_cost] * ([migs].[avg_user_impact] / 100.0) * ([migs].[user_seeks] + [migs].[user_scans]) > 10 AND [database_id] = DB_ID()
ORDER BY [migs].[avg_total_user_cost] * [migs].[avg_user_impact] * ([migs].[user_seeks] + [migs].[user_scans]) DESC;