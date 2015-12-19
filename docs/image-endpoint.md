# Image Endpoint

##GetImageAsync
Get information about an image.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new ImageEndpoint(client);
            var image = await endpoint.GetImageAsync("IMAGE_ID");

##UploadImageBinaryAsync
Upload a new image using a binary file.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new ImageEndpoint(client);
            var localImage = System.IO.File.ReadAllBytes(@"IMAGE_LOCATION");
            var image = await endpoint.UploadImageBinaryAsync(localImage);

##UploadImageStreamAsync
Upload a new image using a stream.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new ImageEndpoint(client);
            IImage image = null;
			using (var fs = new FileStream(@"IMAGE_LOCATION", FileMode.Open))
            {
                image = await endpoint.UploadImageStreamAsync(fs);
            }

##UploadImageUrlAsync
Upload a new image using a URL.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new ImageEndpoint(client);
            var image = await endpoint.UploadImageUrlAsync("IMAGE_URL");

##DeleteImageAsync
Deletes an image. For an anonymous image, the deletehash that is returned at creation must be used.
If the image belongs to your account then passing the ID of the image is sufficient.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new ImageEndpoint(client);
            var deleted = await endpoint.DeleteImageAsync("IMAGE_ID_OR_DELETE_HASH");

##UpdateImageAsync
Updates the title or description of an image. 
For an anonymous image, the deletehash that is returned at creation must be used.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new ImageEndpoint(client);
            var updated = await endpoint.UpdateImageAsync("IMAGE_ID_OR_DELETE_HASH", "TITLE", "DESCRIPTION");

##FavoriteImageAsync
Favorite an image with the given ID. The user is required to be logged in to favorite the image.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH_TOKEN);
            var endpoint = new ImageEndpoint(client);
            var favorited = await endpoint.FavoriteImageAsync("IMAGE_ID");
