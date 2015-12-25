# Custom Gallery Endpoint

##AddCustomGalleryTagsAsync
Add tags to a user's custom gallery. OAuth authentication required.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
            var endpoint = new CustomGalleryEndpoint(client);
			var added = await endpoint.AddCustomGalleryTagsAsync(new List<string> { "A_TAG", "ANOTHER_TAG" });
			
##AddFilteredOutGalleryTagAsync
Add tags to filter out. OAuth authentication required.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
            var endpoint = new CustomGalleryEndpoint(client);
			var removed = await endpoint.RemoveCustomGalleryTagsAsync(new List<string> { "A_TAG", "ANOTHER_TAG" });
			
##GetCustomGalleryAsync
View images for current user's custom gallery. OAuth authentication required.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
            var endpoint = new CustomGalleryEndpoint(client);
			var gallery = await endpoint.GetCustomGalleryAsync();
			
##GetCustomGalleryItemAsync
View a single item in a user's custom gallery. OAuth authentication required.
 
            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
            var endpoint = new CustomGalleryEndpoint(client);
			var item = await endpoint.GetCustomGalleryItemAsync("ITEM_ID");
			
##GetFilteredOutGalleryAsync
Retrieve user's filtered out gallery. OAuth authentication required.
 
            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
            var endpoint = new CustomGalleryEndpoint(client);
			var gallery = await endpoint.GetFilteredOutGalleryAsync();
			
##RemoveCustomGalleryTagsAsync
Remove tags from a custom gallery. OAuth authentication required.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
            var endpoint = new CustomGalleryEndpoint(client);
			var removed = await endpoint.RemoveCustomGalleryTagsAsync(new List<string> { "A_TAG", "ANOTHER_TAG" });
			
##RemoveFilteredOutGalleryTagAsync
Remove a filtered out tag. OAuth authentication required.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
            var endpoint = new CustomGalleryEndpoint(client);
			var removed = await endpoint.RemoveFilteredOutGalleryTagAsync("A_TAG");