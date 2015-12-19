# Account Endpoint

##GetAccountAsync
Request standard user information. 

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new AccountEndpoint(client);
            var account = await endpoint.GetAccountAsync("USERNAME");

##GetAccountGalleryFavoritesAsync
Return the images the user has favorited in the gallery.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new AccountEndpoint(client);
			var favourites = await endpoint.GetAccountGalleryFavoritesAsync("USERNAME");

##GetAccountFavoritesAsync
Returns the users favorited images, only accessible if you're logged in as the user.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH_TOKEN);
			var endpoint = new AccountEndpoint(client);
            var favourites = await endpoint.GetAccountFavoritesAsync();

##GetAccountSubmissionsAsync
Return the images a user has submitted to the gallery.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new AccountEndpoint(client);
			var submissions = await endpoint.GetAccountSubmissionsAsync("USERNAME");
			
##UpdateAccountSettingsAsync
Updates the account settings for a given user, the user must be logged in.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH_TOKEN);
			var endpoint = new AccountEndpoint(client);
            var updated = await endpoint.UpdateAccountSettingsAsync("BIO");			
			
##GetGalleryProfileAsync
Returns the totals for the gallery profile.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new AccountEndpoint(client);
			var profile = await endpoint.GetGalleryProfileAsync("USERNAME");
			
##VerifyEmailAsync
Checks to see if user has verified their email address.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH_TOKEN);
			var endpoint = new AccountEndpoint(client);
            var verified = await endpoint.VerifyEmailAsync();	
			
##SendVerificationEmailAsync
Sends an email to the user to verify that their email is valid to upload to gallery. Must be logged in as the user to send.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH_TOKEN);
			var endpoint = new AccountEndpoint(client);
            var sent = await endpoint.SendVerificationEmailAsync();				
			
##GetAlbumsAsync	
Get all the albums associated with the account. Must be logged in as the user to see secret and hidden albums.			
			
            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
			var endpoint = new AccountEndpoint(client);
			var albums = await endpoint.GetAlbumsAsync("USERNAME", 2);

##GetAlbumAsync	
Get additional information about an album.		
			
            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
			var endpoint = new AccountEndpoint(client);
			var album = await endpoint.GetAlbumAsync("ALBUM_ID", "USERNAME");
						
##GetAlbumIdsAsync
Return an array of all of the album IDs.	
			
            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
			var endpoint = new AccountEndpoint(client);
			var albumIds = await endpoint.GetAlbumIdsAsync("USERNAME", 2);

##GetAlbumCountAsync
Return the total number of albums associated with the account.	
			
            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
			var endpoint = new AccountEndpoint(client);
			var count = await endpoint.GetAlbumCountAsync("USERNAME", 2);

##DeleteAlbumAsync
Delete an Album with a given id.
			
            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH_TOKEN);
			var endpoint = new AccountEndpoint(client);
			var deleted = await endpoint.DeleteAlbumAsync("ALBUM_ID", "USERNAME");

##GetCommentsAsync
Return the comments the user has created.
			
            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
			var endpoint = new AccountEndpoint(client);
			var comments = await endpoint.GetCommentsAsync("USERNAME");

##GetCommentAsync
Return information about a specific comment. 

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
			var endpoint = new AccountEndpoint(client);
			var comment = await endpoint.GetCommentAsync("COMMENT_ID", "USERNAME");

##GetCommentIdsAsync
Return an array of all of the comment IDs.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
			var endpoint = new AccountEndpoint(client);
			var commentIds = await endpoint.GetCommentIdsAsync("USERNAME");

##GetCommentCountAsync
Return a count of all of the comments associated with the account.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
			var endpoint = new AccountEndpoint(client);
			var count = await endpoint.GetCommentCountAsync("USERNAME");

##DeleteCommentAsync
Delete a comment. You are required to be logged in as the user whom created the comment.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH_TOKEN);
			var endpoint = new AccountEndpoint(client);
			var deleted = await endpoint.DeleteCommentAsync("COMMENT_ID", "USERNAME");

##GetImagesAsync
Return all of the images associated with the account. You can page through the images by setting the page, this defaults to 0.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH_TOKEN);
			var endpoint = new AccountEndpoint(client);
			var images = await endpoint.GetImagesAsync();

##GetImageAsync
Return information about a specific image.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
			var endpoint = new AccountEndpoint(client);
			var image = await endpoint.GetImageAsync("IMAGE_ID", "USERNAME");

##GetImageIdsAsync
Returns an array of Image IDs that are associated with the account.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH_TOKEN);
			var endpoint = new AccountEndpoint(client);
			var imageIds = await endpoint.GetImageIdsAsync();

##GetImageCountAsync
Returns the total number of images associated with the account.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH_TOKEN);
			var endpoint = new AccountEndpoint(client);
			var count = await endpoint.GetImageCountAsync();

##DeleteImageAsync
Deletes an Image. This requires a delete hash rather than an ID.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH_TOKEN);
			var endpoint = new AccountEndpoint(client);
			var deleted = await endpoint.DeleteImageAsync("DELETE_HASH", "USERNAME");

##GetNotificationsAsync
Returns all of the reply notifications for the user. Required to be logged in as that user.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH_TOKEN);
			var endpoint = new AccountEndpoint(client);
			var notifications = await endpoint.GetNotificationsAsync(false);