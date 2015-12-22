# Rate Limit Endpoint

##GetRateLimitAsync
Gets remaining credits for the application.

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new RateLimitEndpoint(client);
			var rateLimit = await endpoint.GetRateLimitAsync();

