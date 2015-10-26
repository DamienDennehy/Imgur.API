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
            Debug.WriteLine(account.Id);

##GetAccountAsync
Request standard user information. 

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new AccountEndpoint(client);
            var account = await endpoint.GetAccountAsync("sarah");
            Debug.WriteLine(account.Id);

##GetAccountGalleryFavoritesAsync
Return the images the user has favorited in the gallery.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new AccountEndpoint(client);
			var favourites = await endpoint.GetAccountGalleryFavoritesAsync("sarah");
            Debug.WriteLine(favourites.Count());

##GetAccountFavoritesAsync
Returns the users favorited images, only accessible if you're logged in as the user.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var oAuth2Endpoint = new OAuth2Endpoint(client);
            var token = await oAuth2Endpoint.GetTokenByRefreshTokenAsync("YOUR_REFRESH_TOKEN");
            client.SetOAuth2Token(token);
			var endpoint = new AccountEndpoint(client);
            var favourites = await endpoint.GetAccountFavoritesAsync();
            Debug.WriteLine(favourites.Count());

##GetAccountSubmissionsAsync
Return the images a user has submitted to the gallery.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new AccountEndpoint(client);
			var submissions = await endpoint.GetAccountSubmissionsAsync("sarah");
			Debug.WriteLine(submissions.Count());
