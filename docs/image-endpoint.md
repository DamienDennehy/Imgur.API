# Image Endpoint

##DeleteImageAsync
Deletes an image. For an anonymous image, the deletehash that is returned at creation must be used.
If the image belongs to your account then passing the ID of the image is sufficient.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new ImageEndpoint(client);
            var deleted = await endpoint.DeleteImageAsync("IMAGE_ID_OR_DELETE_HASH");

##FavoriteImageAsync
Favorite an image with the given ID. OAuth authentication required.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
            var endpoint = new ImageEndpoint(client);
            var favorited = await endpoint.FavoriteImageAsync("IMAGE_ID");

##GetImageAsync
Get information about an image.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new ImageEndpoint(client);
            var image = await endpoint.GetImageAsync("IMAGE_ID");

##UpdateImageAsync
Updates the title or description of an image. 
For an anonymous image, the deletehash that is returned at creation must be used.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new ImageEndpoint(client);
            var updated = await endpoint.UpdateImageAsync("IMAGE_ID_OR_DELETE_HASH", "TITLE", "DESCRIPTION");

##UploadImageBinaryAsync
Upload a new image using a binary file.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new ImageEndpoint(client);
            var file = System.IO.File.ReadAllBytes(@"IMAGE_LOCATION");
            var image = await endpoint.UploadImageBinaryAsync(file);

##UploadImageStreamAsync
Upload a new image using a stream.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new ImageEndpoint(client);
            IImage image;
			using (var fs = new FileStream(@"IMAGE_LOCATION", FileMode.Open))
            {
                image = await endpoint.UploadImageStreamAsync(fs);
            }

##UploadImageUrlAsync
Upload a new image using a URL.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new ImageEndpoint(client);
            var image = await endpoint.UploadImageUrlAsync("IMAGE_URL");
