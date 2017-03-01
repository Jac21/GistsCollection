SELECT Name, SUBSTRING(Name, 1, 5) + '...' AS Excerpt
FROM Artist;