SELECT table_name [Table Name], column_name [Column Name]
FROM information_schema.columns
WHERE data_type = 'NTEXT'