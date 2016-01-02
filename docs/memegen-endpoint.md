# MemeGen Endpoint

##GetDefaultMemesAsync
Get the list of default memes.

		var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
		var endpoint = new MemeGenEndpoint(client);
		var memes = await endpoint.GetDefaultMemesAsync();

