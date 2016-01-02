# OAuth2

## Authorization
To access a user's account, the user must first authorize your application so that you can get an access token. 
The simplest way to do this is to redirect to Imgur's authorization url.

		var client = new ImgurClient("CLIENT", "SECRET");
		var endpoint = new OAuth2Endpoint(client);
		var redirectUrl = endpoint.GetAuthorizationUrl(OAuth2ResponseType.Token);

Once the user authorizes the application, Imgur will then redirect back to your application's Redirect URL.
The Redirect URL will contain several values that should be parsed and stored by your application.

*   access_token - The user's access token for this session.
*   refresh_token - The user's refresh token which should be used to refresh the access_token when it expires.
*   token_type - The type of token that should be used for authorization.
*   account_id - The user's account id.
*   account_username - The user's account username.
*   expires_in - The time in seconds when the user's access token expires. Default is one month - 2419200.

## Creating an OAuth2 token from the Redirect URL.
Using the Redirect URL values, an OAuth2 Token can be created.

		var token = new OAuth2Token("ACCESS_TOKEN", "REFRESH_TOKEN", "TOKEN_TYPE", 
									"ACCOUNT_ID", "ACCOUNT_USERNAME", EXPIRES_IN);
	
The token should be stored by your application. 
This will save your application from constructing a new token on each endpoint request.

## Getting an OAuth2 token from the Refresh Token.
If the access token has expired but you still have the refresh token, you can request a new OAuth2 token.

		var token = endpoint.GetTokenByRefreshTokenAsync("REFRESH_TOKEN");

## Using the OAuth2 token.
Using the OAuth2 token can be done in two ways. 

You may use it in the client's constructor:

		var client = new ImgurClient("CLIENT", "SECRET", token);
	
You may also switch or set the token explicitly using the client's SetOAuth2Token method:

		var client = new ImgurClient("CLIENT", "SECRET");
		client.SetOAuth2Token(token);

More information on Imgur's OAuth2 implementation can be found at [https://api.imgur.com/oauth2](https://api.imgur.com/oauth2)