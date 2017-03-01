SELECT *, REPLACE (Title, 'Blood', 'B***d') AS CleanTitle
FROM Album WHERE Title LIKE '%Blood%';