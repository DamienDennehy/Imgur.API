# Account Endpoint

##DeleteAlbumAsync
Delete an Album with a given id. OAuth authentication required.
			
		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
		var endpoint = new AccountEndpoint(client);
		var deleted = await endpoint.DeleteAlbumAsync("ALBUM_ID", "USERNAME");
	
##DeleteCommentAsync
Delete a comment. OAuth authentication required.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
		var endpoint = new AccountEndpoint(client);
		var deleted = await endpoint.DeleteCommentAsync("COMMENT_ID", "USERNAME");

##DeleteImageAsync
Deletes an Image. OAuth authentication required.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
		var endpoint = new AccountEndpoint(client);
		var deleted = await endpoint.DeleteImageAsync("DELETE_HASH", "USERNAME");

##GetAccountAsync
Request standard user information. 

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AccountEndpoint(client);
		var account = await endpoint.GetAccountAsync("USERNAME");

##GetAccountFavoritesAsync
Returns the users favorited images. OAuth authentication required.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
		var endpoint = new AccountEndpoint(client);
		var favourites = await endpoint.GetAccountFavoritesAsync();

##GetAccountGalleryFavoritesAsync
Return the images the user has favorited in the gallery.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AccountEndpoint(client);
		var favourites = await endpoint.GetAccountGalleryFavoritesAsync("USERNAME");

##GetAccountSettingsAsync
Returns the account settings. 
OAuth authentication required.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
		var endpoint = new AccountEndpoint(client);
		var submissions = await endpoint.GetAccountSettingsAsync();

##GetAccountSubmissionsAsync
Return the images a user has submitted to the gallery.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AccountEndpoint(client);
		var submissions = await endpoint.GetAccountSubmissionsAsync("USERNAME");
						
##GetAlbumAsync	
Get additional information about an album, this works the same as the Album Endpoint.		
			
		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AccountEndpoint(client);
		var album = await endpoint.GetAlbumAsync("ALBUM_ID", "USERNAME");

##GetAlbumCountAsync
Return the total number of albums associated with the account.	
			
		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AccountEndpoint(client);
		var count = await endpoint.GetAlbumCountAsync("USERNAME");	
								
##GetAlbumIdsAsync
Return an array of all of the album IDs.	
			
		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AccountEndpoint(client);
		var albumIds = await endpoint.GetAlbumIdsAsync("USERNAME", PAGE);

##GetAlbumsAsync	
Get all the albums associated with the account. Must be logged in as the user to see secret and hidden albums.			
			
		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AccountEndpoint(client);
		var albums = await endpoint.GetAlbumsAsync("USERNAME", PAGE);

##GetCommentAsync
Return information about a specific comment. 

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AccountEndpoint(client);
		var comment = await endpoint.GetCommentAsync("COMMENT_ID", "USERNAME");

##GetCommentCountAsync
Return a count of all of the comments associated with the account.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AccountEndpoint(client);
		var count = await endpoint.GetCommentCountAsync("USERNAME");

##GetCommentIdsAsync
Return an array of all of the comment IDs.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AccountEndpoint(client);
		var commentIds = await endpoint.GetCommentIdsAsync("USERNAME");

##GetCommentsAsync
Return the comments the user has created.
			
		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AccountEndpoint(client);
		var comments = await endpoint.GetCommentsAsync("USERNAME");

##GetGalleryProfileAsync
Returns the totals for the gallery profile.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AccountEndpoint(client);
		var profile = await endpoint.GetGalleryProfileAsync("USERNAME");

##GetImageAsync
Return information about a specific image.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new AccountEndpoint(client);
		var image = await endpoint.GetImageAsync("IMAGE_ID", "USERNAME");

##GetImageCountAsync
Returns the total number of images associated with the account. OAuth authentication required.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
		var endpoint = new AccountEndpoint(client);
		var count = await endpoint.GetImageCountAsync();

##GetImageIdsAsync
Returns an array of Image IDs that are associated with the account. OAuth authentication required.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
		var endpoint = new AccountEndpoint(client);
		var imageIds = await endpoint.GetImageIdsAsync();
			
##GetImagesAsync
Return all of the images associated with the account. OAuth authentication required.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
		var endpoint = new AccountEndpoint(client);
		var images = await endpoint.GetImagesAsync();

##GetNotificationsAsync
Returns all of the notifications for the user. OAuth authentication required.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
		var endpoint = new AccountEndpoint(client);
		var notifications = await endpoint.GetNotificationsAsync(false);

##SendVerificationEmailAsync
Sends an email to the user to verify that their email is valid to upload to gallery. OAuth authentication required.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
		var endpoint = new AccountEndpoint(client);
		var sent = await endpoint.SendVerificationEmailAsync();

##UpdateAccountSettingsAsync
Updates the account settings for a given user. OAuth authentication required.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
		var endpoint = new AccountEndpoint(client);
		var updated = await endpoint.UpdateAccountSettingsAsync();

##VerifyEmailAsync
Checks to see if user has verified their email address. OAuth authentication required.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
		var endpoint = new AccountEndpoint(client);
		var verified = await endpoint.VerifyEmailAsync();