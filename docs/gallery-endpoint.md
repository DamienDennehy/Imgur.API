# Gallery Endpoint

##CreateGalleryItemCommentAsync
Create a comment for an item. OAuth authentication required.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
		var endpoint = new GalleryEndpoint(client);
		var comment = await endpoint.CreateGalleryItemCommentAsync("YOUR_COMMENT", "GALLERY_ITEM_ID");

##CreateGalleryItemCommentReplyAsync
Reply to a comment that has been created for an item. OAuth authentication required.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
		var endpoint = new GalleryEndpoint(client);
		var comment = await endpoint.CreateGalleryItemCommentReplyAsync("YOUR_COMMENT", "GALLERY_ITEM_ID", "PARENT_ID");
		
##GetGalleryAlbumAsync
Get additional information about an album in the gallery.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
		var endpoint = new GalleryEndpoint(client);
		var comment = await endpoint.GetGalleryAlbumAsync("GALLERY_ALBUM_ID");