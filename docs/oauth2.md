# OAuth2

## Authorization
To access a user's account, the user must first authorize your application so that you can get an access token. 
The simplest way to do this is to redirect to Imgur's authorization url.

    var client = new ImgurClient("YOUR_CLIENT", "YOUR_SECRET");
    var endpoint = new OAuth2Endpoint(client);
    var redirectUrl = endpoint.GetAuthorizationUrl(OAuth2ResponseType.Token, null);

Once the user authorizes the application, the Imgur OAuth2 endpoint will redirect back to your application Redirect URL.
The Redirect URL will contain several values that should be saved.

*   access_token - The user's access token for this session.
*   refresh_token - The user's refresh token which should be used to refresh the access_token when it expires.
*   expires_in - The time in seconds when the user's access token expires. Default is 3600.
*   token_type - The type of token that should be used for authorization.
*   account_username - The account username that is now authorized.
*   account_id - The account id that is now authorized.

## Creating an OAuth2 token from the Redirect URL.
Using the Redirect URL values, an OAuth2 Token can be created.

    var token = new OAuth2Token("ACCESS_TOKEN", "REFRESH_TOKEN", "TOKEN_TYPE", "ACCOUNT_ID", 3600);

## Getting an OAuth2 token from the Refresh Token.
If the access token has expired but you still have the refresh token, you can request a new OAuth2 token.

    var token = endpoint.GetTokenByRefreshTokenAsync("YOUR_REFRESH_TOKEN");

## Setting the OAuth2 token.
Setting the OAuth2 token can be done in two ways. 

You may set it in the client's constructor:

    var client = new ImgurClient("YOUR_CLIENT", "YOUR_SECRET", token);
	
You may also set it explicitly using the client:

	var client = new ImgurClient("YOUR_CLIENT", "YOUR_SECRET");
    client.SetOAuth2Token(token);

More information on Imgur's OAuth2 implementation can be found at [https://api.imgur.com/oauth2](https://api.imgur.com/oauth2)