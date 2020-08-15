# Imgur.API
Imgur.API is a .NET implementation of Imgur's API. 

![Build](https://github.com/DamienDennehy/Imgur.API/workflows/Build/badge.svg)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=ImgurAPI&metric=alert_status)](https://sonarcloud.io/dashboard?id=ImgurAPI)

[![NuGet](https://img.shields.io/nuget/vpre/Imgur.API.svg)](https://www.nuget.org/packages/Imgur.API/)

## Getting Started
### Register Client
Register your App at https://api.imgur.com/oauth2/addclient

### Creating an ApiClient
~~~
var apiClient = new ApiClient("YOUR_CLIENT_KEY");
~~~

## Using OAuth
### Getting an Authorization Url
The Authorization Url should be loaded in a browser, allowing the user to login to Imgur.

~~~
var apiClient = new ApiClient("YOUR_CLIENT_KEY", "YOUR_CLIENT_SECRET");
var httpClient = new HttpClient();

var oAuth2Endpoint = new OAuth2Endpoint(apiClient, httpClient);
var authUrl = oAuth2Endpoint.GetAuthorizationUrl();
~~~

Once user has logged in, they are redirected to your previously set Url.
Once the token information is available and parsed create a token.

~~~
var token = new OAuth2Token
{
    AccessToken = "YOUR_TOKEN",
    RefreshToken = "YOUR_REFRESH_TOKEN",
    AccountId = YOUR_ACCOUNT_ID,
    AccountUsername = "YOUR_ACCOUNT_PASSWORD",
    ExpiresIn = YOUR_EXPIRATION,
    TokenType = "YOUR_TOKEN"
};
~~~

Then set the token on the ApiClient.

~~~
apiClient.SetOAuth2Token(token);
~~~

Continue to use the rest of the Endpoints.

~~~
var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
~~~

## Uploading Images & Video
### Uploading Image

~~~
var apiClient = new ApiClient("YOUR_CLIENT_KEY");
var httpClient = new HttpClient();

var filePath = "PATH_TO_YOUR_IMAGE";
using var fileStream = File.OpenRead(filePath);

var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
var imageUpload = await imageEndpoint.UploadImageAsync(fileStream);
~~~       

### Uploading Video

~~~
var apiClient = new ApiClient("YOUR_CLIENT_KEY");
var httpClient = new HttpClient();

var filePath = "PATH_TO_YOUR_IMAGE";
using var fileStream = File.OpenRead(filePath);

var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
var imageUpload = await imageEndpoint.UploadVideoAsync(fileStream);
~~~ 

### Uploading Video with Progress

~~~
var apiClient = new ApiClient("YOUR_CLIENT_KEY");
var httpClient = new HttpClient();

var uploadProgress = new Progress<int>(report);

var filePath = "PATH_TO_YOUR_IMAGE";
using var fileStream = File.OpenRead(filePath);

var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
var imageUpload = await imageEndpoint.UploadVideoAsync(fileStream, progress: uploadProgress);

void report(int byteProgress)
{
    //Do something with the progress here. 
}
~~~

## API Definition
Several Endpoints are available.
The methods on the Endpoints match what is available at the official Imgur API at https://apidocs.imgur.com/