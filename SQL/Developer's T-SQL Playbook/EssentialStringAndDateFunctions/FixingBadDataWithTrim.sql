SELECT Name, LTRIM(Name) AS Fixed FROM Artist
WHERE Name LIKE '%Kiss%';

-- UPDATE Artist SET Name = LTRIM(Name);