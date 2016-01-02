# Topic Endpoint

##GetDefaultTopicsAsync
Get the list of default topics.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new TopicEndpoint(client);
		var topics = await endpoint.GetDefaultTopicsAsync();

##GetGalleryTopicItemAsync
View a single item in a gallery topic.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new TopicEndpoint(client);
		var topics = await endpoint.GetGalleryTopicItemAsync("GALLERY_ITEM_ID", "TOPIC_ID");

##GetGalleryTopicItemsAsync
View gallery items for a topic.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new TopicEndpoint(client);
		var topics = await endpoint.GetGalleryTopicItemsAsync("TOPIC_ID");