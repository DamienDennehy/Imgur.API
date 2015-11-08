# Account Endpoint

Source code samples below do not include exception handling for brevity.

The following methods are available:

##GetAccountAsync
Request your user information. 

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
			var oAuth2Endpoint = new OAuth2Endpoint(client);
			var token = await oAuth2Endpoint.GetTokenByRefreshTokenAsync("YOUR_REFRESH_TOKEN");
			client.SetOAuth2Token(token);
            var endpoint = new AccountEndpoint(client);
            var account = await endpoint.GetAccountAsync();

##GetAccountAsync
Request standard user information. 

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new AccountEndpoint(client);
            var account = await endpoint.GetAccountAsync("sarah");

##GetAccountGalleryFavoritesAsync
Return the images the user has favorited in the gallery.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new AccountEndpoint(client);
			var favourites = await endpoint.GetAccountGalleryFavoritesAsync("sarah");

##GetAccountFavoritesAsync
Returns the users favorited images, only accessible if you're logged in as the user.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var oAuth2Endpoint = new OAuth2Endpoint(client);
            var token = await oAuth2Endpoint.GetTokenByRefreshTokenAsync("YOUR_REFRESH_TOKEN");
            client.SetOAuth2Token(token);
			var endpoint = new AccountEndpoint(client);
            var favourites = await endpoint.GetAccountFavoritesAsync();

##GetAccountSubmissionsAsync
Return the images a user has submitted to the gallery.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new AccountEndpoint(client);
			var submissions = await endpoint.GetAccountSubmissionsAsync("sarah");
			
##UpdateAccountSettingsAsync
Updates the account settings for a given user, the user must be logged in.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var oAuth2Endpoint = new OAuth2Endpoint(client);
            var token = await oAuth2Endpoint.GetTokenByRefreshTokenAsync("YOUR_REFRESH_TOKEN");
            client.SetOAuth2Token(token);
			var endpoint = new AccountEndpoint(client);
            var updated = await endpoint.UpdateAccountSettingsAsync("Hello World!");			
			
##GetGalleryProfileAsync
Returns the totals for the gallery profile.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new AccountEndpoint(client);
			var profile = await endpoint.GetGalleryProfileAsync("sarah");
			
##VerifyEmailAsync
Checks to see if user has verified their email address.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var oAuth2Endpoint = new OAuth2Endpoint(client);
            var token = await oAuth2Endpoint.GetTokenByRefreshTokenAsync("YOUR_REFRESH_TOKEN");
            client.SetOAuth2Token(token);
			var endpoint = new AccountEndpoint(client);
            var verified = await endpoint.VerifyEmailAsync();	
			
##SendVerificationEmailAsync
Sends an email to the user to verify that their email is valid to upload to gallery. Must be logged in as the user to send.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var oAuth2Endpoint = new OAuth2Endpoint(client);
            var token = await oAuth2Endpoint.GetTokenByRefreshTokenAsync("YOUR_REFRESH_TOKEN");
            client.SetOAuth2Token(token);
			var endpoint = new AccountEndpoint(client);
            var sent = await endpoint.SendVerificationEmailAsync();				
			
##GetAlbumsAsync	
Get all the albums associated with the account. Must be logged in as the user to see secret and hidden albums.			
			
            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
			var endpoint = new AccountEndpoint(client);
			var albums = await endpoint.GetAlbumsAsync("sarah", 2);

##GetAlbumAsync	
Get additional information about an album.		
			
            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
			var endpoint = new AccountEndpoint(client);
			var album = await endpoint.GetAlbumAsync("HmszY", "sarah");
						
##GetAlbumIdsAsync
Return an array of all of the album IDs.	
			
            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
			var endpoint = new AccountEndpoint(client);
			var albumIds = await endpoint.GetAlbumIdsAsync("sarah", 2);

##GetAlbumCountAsync
Return the total number of albums associated with the account.	
			
            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
			var endpoint = new AccountEndpoint(client);
			var count = await endpoint.GetAlbumCountAsync("sarah", 2);

##GetAlbumCountAsync
Return the total number of albums associated with the account.	
			
            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
			var endpoint = new AccountEndpoint(client);
			var count = await endpoint.GetAlbumCountAsync("sarah", 2);

##DeleteAlbumAsync
Delete an Album with a given id.
			
            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var oAuth2Endpoint = new OAuth2Endpoint(client);
            var token = await oAuth2Endpoint.GetTokenByRefreshTokenAsync("YOUR_REFRESH_TOKEN");
            client.SetOAuth2Token(token);
			var endpoint = new AccountEndpoint(client);
			var deleted = await endpoint.DeleteAlbumAsync("3pqWgc");