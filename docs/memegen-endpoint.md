# MemeGen Endpoint

##GetDefaultMemesAsync
Get the list of default memes.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
		var endpoint = new MemeGenEndpoint(client);
		var memes = await endpoint.GetDefaultMemesAsync();

