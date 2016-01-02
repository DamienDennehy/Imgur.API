# Custom Gallery Endpoint

##AddCustomGalleryTagsAsync
Add tags to a user's custom gallery. OAuth authentication required.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new CustomGalleryEndpoint(client);
		var added = await endpoint.AddCustomGalleryTagsAsync(new List<string> { "TAG", "TAG" });
			
##AddFilteredOutGalleryTagAsync
Add tags to filter out. OAuth authentication required.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new CustomGalleryEndpoint(client);
		var removed = await endpoint.AddFilteredOutGalleryTagAsync("A_TAG");
			
##GetCustomGalleryAsync
View images for current user's custom gallery. OAuth authentication required.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new CustomGalleryEndpoint(client);
		var gallery = await endpoint.GetCustomGalleryAsync();
			
##GetCustomGalleryItemAsync
View a single item in a user's custom gallery. OAuth authentication required.
 
		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new CustomGalleryEndpoint(client);
		var item = await endpoint.GetCustomGalleryItemAsync("GALLERY_ITEM_ID");
			
##GetFilteredOutGalleryAsync
Retrieve user's filtered out gallery. OAuth authentication required.
 
		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new CustomGalleryEndpoint(client);
		var gallery = await endpoint.GetFilteredOutGalleryAsync();
			
##RemoveCustomGalleryTagsAsync
Remove tags from a custom gallery. OAuth authentication required.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new CustomGalleryEndpoint(client);
		var removed = await endpoint.RemoveCustomGalleryTagsAsync(new List<string> { "TAG", "TAG" });
			
##RemoveFilteredOutGalleryTagAsync
Remove a filtered out tag. OAuth authentication required.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new CustomGalleryEndpoint(client);
		var removed = await endpoint.RemoveFilteredOutGalleryTagAsync("TAG");