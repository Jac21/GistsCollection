SELECT *,
(SELECT COUNT(1) FROM 
	Album WHERE Album.ArtistId = Artist.ArtistId
) as AlbumCount
FROM Artist
ORDER BY AlbumCount;