# Image Endpoint

##GetImageAsync
Get information about an image.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var imageEndpoint = new ImageEndpoint(client);
            var image = await imageEndpoint.GetImageAsync("qvM6Xho");

##UploadImageBinaryAsync
Upload a new image using a binary file.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var imageEndpoint = new ImageEndpoint(client);
            var localImage = System.IO.File.ReadAllBytes(@"D:\Image.jpg");
            var image = await imageEndpoint.UploadImageBinaryAsync(localImage,
                null, "Awesome pic!", "Took me weeks to get this shot.");

##UploadImageUrlAsync
Upload a new image using a URL.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var imageEndpoint = new ImageEndpoint(client);
            var image = await imageEndpoint.UploadImageUrlAsync("http://i.imgur.com/kLq629A.jpg",
                null, "Awesome pic!", "Took me weeks to get this shot.");

##DeleteImageAsync
Deletes an image. For an anonymous image, {id} must be the image's deletehash.
If the image belongs to your account then passing the ID of the image is sufficient.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var imageEndpoint = new ImageEndpoint(client);
            var deleted = await imageEndpoint.DeleteImageAsync("IMAGE_ID_OR_HASH");

##UpdateImageAsync
Updates the title or description of an image.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var imageEndpoint = new ImageEndpoint(client);
            var updated = await imageEndpoint.UpdateImageAsync("IMAGE_ID_OR_HASH", "TITLE", "DESCRIPTION");

##FavoriteImageAsync
Favorite an image with the given ID. The user is required to be logged in to favorite the image.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var oAuth2Endpoint = new OAuth2Endpoint(client);
            var token = await oAuth2Endpoint.GetTokenByRefreshTokenAsync("YOUR_REFRESH_TOKEN");
            client.SetOAuth2Token(token);
            var imageEndpoint = new ImageEndpoint(client);
            var favorited = await imageEndpoint.FavoriteImageAsync("IMAGE_ID");

More information on the image endpoint can be found at [http://api.imgur.com/endpoints/image](http://api.imgur.com/endpoints/image)
