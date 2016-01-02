# Gallery Endpoint

##CreateGalleryItemCommentAsync
Create a comment for an item. OAuth authentication required.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new GalleryEndpoint(client);
		var comment = await endpoint.CreateGalleryItemCommentAsync("COMMENT", "GALLERY_ITEM_ID");

##CreateGalleryItemCommentReplyAsync
Reply to a comment that has been created for an item. OAuth authentication required.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new GalleryEndpoint(client);
		var comment = await endpoint.CreateGalleryItemCommentReplyAsync("COMMENT", "GALLERY_ITEM_ID", "PARENT_ID");
		
##GetGalleryAlbumAsync
Get additional information about an album in the gallery.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new GalleryEndpoint(client);
		var comment = await endpoint.GetGalleryAlbumAsync("ALBUM_ID");

##GetGalleryAsync
Returns the images in the gallery.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new GalleryEndpoint(client);
		var images = await endpoint.GetGalleryAsync();

##GetGalleryImageAsync
Get additional information about an image in the gallery.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new GalleryEndpoint(client);
		var image = await endpoint.GetGalleryImageAsync("IMAGE_ID");

##GetGalleryItemCommentAsync
Get information about a specific comment.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new GalleryEndpoint(client);
		var comment = await endpoint.GetGalleryItemCommentAsync("COMMENT_ID", "GALLERY_ITEM_ID");

##GetGalleryItemCommentCountAsync
The number of comments on an item.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new GalleryEndpoint(client);
		var count = await endpoint.GetGalleryItemCommentCountAsync("GALLERY_ITEM_ID");

##GetGalleryItemCommentIdsAsync
List all of the IDs for the comments on an item.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new GalleryEndpoint(client);
		var commentIds = await endpoint.GetGalleryItemCommentIdsAsync("GALLERY_ITEM_ID");

##GetGalleryItemCommentsAsync
Get all comments for a gallery item.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new GalleryEndpoint(client);
		var comments = await endpoint.GetGalleryItemCommentsAsync("GALLERY_ITEM_ID");

##GetGalleryItemTagsAsync
View tags for a gallery item.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new GalleryEndpoint(client);
		var tags = await endpoint.GetGalleryItemTagsAsync("GALLERY_ITEM_ID");

##GetGalleryItemVotesAsync
Get the vote information about an image.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new GalleryEndpoint(client);
		var votes = await endpoint.GetGalleryItemVotesAsync("GALLERY_ITEM_ID");

##GetGalleryTagAsync
View images for a gallery tag.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new GalleryEndpoint(client);
		var tag = await endpoint.GetGalleryTagAsync("TAG");

##GetGalleryTagImageAsync
View a single image in a gallery tag.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new GalleryEndpoint(client);
		var tag = await endpoint.GetGalleryTagImageAsync("GALLERY_ITEM_ID", "TAG");

##GetMemesSubGalleryAsync
View images for memes subgallery.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new GalleryEndpoint(client);
		var memes = await endpoint.GetMemesSubGalleryAsync();

##GetMemesSubGalleryImageAsync
View a single image in the memes gallery.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new GalleryEndpoint(client);
		var memes = await endpoint.GetMemesSubGalleryImageAsync("IMAGE_ID");

##GetRandomGalleryAsync
Returns a random set of gallery images.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new GalleryEndpoint(client);
		var memes = await endpoint.GetRandomGalleryAsync();

##GetSubredditGalleryAsync
View gallery images for a subreddit.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new GalleryEndpoint(client);
		var subreddit = await endpoint.GetSubredditGalleryAsync("SUBREDDIT");

##GetSubredditImageAsync
View a single image in the subreddit.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new GalleryEndpoint(client);
		var image = await endpoint.GetSubredditImageAsync("IMAGE_ID", "SUBREDDIT");

##PublishToGalleryAsync
Remove an image from the gallery. OAuth authentication required.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new GalleryEndpoint(client);
		var published = await endpoint.RemoveFromGalleryAsync("GALLERY_ITEM_ID");

##RemoveFromGalleryAsync
Remove an image from the gallery. OAuth authentication required.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new GalleryEndpoint(client);
		var removed = await endpoint.RemoveFromGalleryAsync("GALLERY_ITEM_ID");

##ReportGalleryItemAsync
Report an item in the gallery. OAuth authentication required.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new GalleryEndpoint(client);
		var reported = await endpoint.ReportGalleryItemAsync("GALLERY_ITEM_ID", REASON);

##SearchGalleryAsync
Search the gallery with a given query string.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new GalleryEndpoint(client);
		var images = await endpoint.SearchGalleryAsync("QUERY");

##SearchGalleryAdvancedAsync
Search the gallery with a given query string.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new GalleryEndpoint(client);
		var images = await endpoint.SearchGalleryAdvancedAsync("ALL_WORDS_QUERY", "ANY_WORDS_QUERY", "EXACT_WORDS_QUERY", "NOT_WORDS_QUERY");

##VoteGalleryItemAsync
Vote for an item. Send the same value again to undo a vote. OAuth authentication required.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new GalleryEndpoint(client);
		var voted = await VoteGalleryItemAsync("GALLERY_ITEM_ID", VOTE);

##VoteGalleryTagAsync
Vote for a tag. Send the same value again to undo a vote. OAuth authentication required.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET", OAUTH2_TOKEN);
		var endpoint = new GalleryEndpoint(client);
		var voted = await VoteGalleryTagAsync("GALLERY_ITEM_ID", "TAG", VOTE);