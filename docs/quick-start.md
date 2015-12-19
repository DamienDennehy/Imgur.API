# Quick Start

## Issues
Report any issues at [https://github.com/DamienDennehy/Imgur.API/issues](https://github.com/DamienDennehy/Imgur.API/issues)

## Latest Release
### Version 3.2.0 (2015-12-25)
* Album endpoint added.
* Fixed case sensitivity issue for Gallery methods of the AccountEndpoint.
* Significant rewrite of Account and Image endpoints for additional unit test coverage.

## Quick Start
### Upload image anonymously with Imgur Api authentication.

    try
    {
        var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
        var endpoint = new ImageEndpoint(client);
        var file = System.IO.File.ReadAllBytes(@"D:\Image.jpg");
        var image = await endpoint.UploadImageBinaryAsync(file, "ALBUM_ID", "ALBUM_TITLE", "ALBUM_DESCRIPTION");
		Debug.Write("Image uploaded. Image Url: " + image.Link);
    }
    catch (ImgurException imgurEx)
    {
        Debug.Write("An error occurred uploading an image to Imgur.");
        Debug.Write(imgurEx.Message);
    }

### Upload image anonymously using Mashape Api authentication.

    try
    {
        var client = new MashapeClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", "YOUR_MASHAPE_KEY");
        var endpoint = new ImageEndpoint(client);
        var file = System.IO.File.ReadAllBytes(@"D:\Image.jpg");
        var image = await endpoint.UploadImageBinaryAsync(file, "ALBUM_ID", "ALBUM_TITLE", "ALBUM_DESCRIPTION");
		Debug.Write("Image uploaded. Image Url: " + image.Link);
    }
    catch (MashapeException mashapeEx)
    {
        Debug.Write("An error occurred using Mashape Authentication.");
        Debug.Write(mashapeEx.Message);
    }
    catch (ImgurException imgurEx)
    {
        Debug.Write("An error occurred uploading an image to Imgur.");
        Debug.Write(imgurEx.Message);
    }
	
### Upload image using an Imgur account with Imgur Api authentication.

    try
    {
        var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
        var oAuth2Endpoint = new OAuth2Endpoint(client);
        var token = await oAuth2Endpoint.GetTokenByRefreshTokenAsync("YOUR_REFRESH_TOKEN");
        client.SetOAuth2Token(token);
        var endpoint = new ImageEndpoint(imgurAuth);
        var file = System.IO.File.ReadAllBytes(@"D:\Image.jpg");
        var image = await endpoint.UploadImageBinaryAsync(file, "ALBUM_ID", "ALBUM_TITLE", "ALBUM_DESCRIPTION");
		Debug.Write("Image uploaded. Image Url: " + image.Link);
    }
    catch (ImgurException imgurEx)
    {
        Debug.Write("An error occurred uploading an image to Imgur.");
        Debug.Write(imgurEx.Message);
    }