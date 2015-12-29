# Album Endpoint

##AddAlbumImagesAsync
Adds an array of ids to the album.
For anonymous albums, the deletehash that is returned at creation must be used.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AlbumEndpoint(client);
		var added = await endpoint.AddAlbumImagesAsync("ALBUM_ID_OR_DELETE_HASH", 
							new List<string> {"IMAGE_ID", "IMAGE_ID", "IMAGE_ID"});

##CreateAlbumAsync
Create a new album.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AlbumEndpoint(client);
		var album = await endpoint.CreateAlbumAsync();

##DeleteAlbumAsync
Delete an album with a given ID. You are required to be logged in as the user to delete the album. 
For anonymous albums, the deletehash that is returned at creation must be used.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AlbumEndpoint(client);
		var deleted = await endpoint.DeleteAlbumAsync("ALBUM_ID_OR_DELETE_HASH");

##FavoriteAlbumAsync
Favorite an album with a given ID. The user is required to be logged in to favorite the album.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
		var endpoint = new AlbumEndpoint(client);
		var favorited = await endpoint.FavoriteAlbumAsync("ALBUM_ID");

##GetAlbumAsync
Get information about a specific album.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AlbumEndpoint(client);
		var album = await endpoint.GetAlbumAsync("ALBUM_ID");

##GetAlbumImageAsync
Get information about an image in an album.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AlbumEndpoint(client);
		var image = await endpoint.GetAlbumImageAsync("IMAGE_ID", "ALBUM_ID");

##GetAlbumImagesAsync
Return all of the images in the album.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AlbumEndpoint(client);
		var images = await endpoint.GetAlbumImagesAsync("ALBUM_ID");

##RemoveAlbumImagesAsync
Removes an array of ids from the album.
For anonymous albums, the deletehash that is returned at creation must be used.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AlbumEndpoint(client);
		var added = await endpoint.RemoveAlbumImagesAsync("ALBUM_ID_OR_DELETE_HASH", 
							new List<string> {"IMAGE_ID", "IMAGE_ID", "IMAGE_ID"});

##SetAlbumImagesAsync
Sets the images for an album, removes all other images and only uses the images in this request. 
For anonymous albums, the deletehash that is returned at creation must be used.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AlbumEndpoint(client);
		var set = await endpoint.SetAlbumImagesAsync("ALBUM_ID_OR_DELETE_HASH", 
							new List<string> {"IMAGE_ID", "IMAGE_ID", "IMAGE_ID"});

##UpdateAlbumAsync
Update the information of an album.
For anonymous albums, the deletehash that is returned at creation must be used.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AlbumEndpoint(client);
		var updated = await endpoint.UpdateAlbumAsync("ALBUM_ID_OR_DELETE_HASH");