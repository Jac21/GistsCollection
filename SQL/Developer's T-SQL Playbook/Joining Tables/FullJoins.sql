SELECT Name, Title
FROM Artist FULL JOIN Album
ON Album.ArtistId = Artist.ArtistId;