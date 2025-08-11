-- The use of ctid in this example is PostgreSQL system column which provides a unique identifier for each row. 
-- By selecting ctid values in the subquery, you can limit the number of rows affected in each iteration. 
-- This approach is more efficient than using LIMIT directly in the main query, as it avoids the need to re-scan the table for each batch.

DELETE FROM films
WHERE ctid IN (
    SELECT ctid FROM films
    WHERE kind <> 'Musical'
    LIMIT 250
);
