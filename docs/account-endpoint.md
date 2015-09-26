# Account Endpoint

Source code samples below do not include exception handling for brevity.

The following methods are available:

##GetAccountAsync
Request standard user information. 

            var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
            var endpoint = new AccountEndpoint(client);
            var account = await endpoint.GetAccountAsync("sarah");
            Debug.WriteLine(account.Url);