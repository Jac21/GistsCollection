SELECT Name, Title
FROM Artist LEFT JOIN Album
ON Album.ArtistId = Artist.ArtistId;