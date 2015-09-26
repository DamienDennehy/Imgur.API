# Quick Start

## Issues
Report any issues at [https://github.com/DamienDennehy/Imgur.API/issues](https://github.com/DamienDennehy/Imgur.API/issues)

## Upload image anonymously with Imgur Api authentication.

    try
    {
        var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
        var imageEndpoint = new ImageEndpoint(client);
        var localImage = System.IO.File.ReadAllBytes(@"D:\Image.jpg");
        var image = await imageEndpoint.UploadImageBinaryAsync(localImage, null, "Awesome pic!", "Took me weeks to get this shot.");
		Debug.Write("Image uploaded. Image Url: " + image.Link);
    }
    catch (ImgurException imgurEx)
    {
        Debug.Write("An error occurred uploading an image to Imgur.");
        Debug.Write(imgurEx.Message);
    }

## Upload image anonymously using Mashape Api authentication.

    try
    {
        var client = new MashapeClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", "YOUR_MASHAPE_KEY");
        var imageEndpoint = new ImageEndpoint(client);
        var localImage = System.IO.File.ReadAllBytes(@"D:\Image.jpg");
        var image = await imageEndpoint.UploadImageBinaryAsync(localImage, null, "Awesome pic!", "Took me weeks to get this shot.");
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
	
## Upload image using an Imgur account with Imgur Api authentication.

    try
    {
        var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
        var oAuth2Endpoint = new OAuth2Endpoint(client);
        var token = await oAuth2Endpoint.GetTokenByRefreshTokenAsync("YOUR_REFRESH_TOKEN");
        client.SetOAuth2Token(token);
        var imageEndpoint = new ImageEndpoint(imgurAuth);
        var localImage = System.IO.File.ReadAllBytes(@"D:\Image.jpg");
        var image = await imageEndpoint.UploadImageBinaryAsync(localImage, null, "Awesome pic!", "Took me weeks to get this shot.");
		Debug.Write("Image uploaded. Image Url: " + image.Link);
    }
    catch (ImgurException imgurEx)
    {
        Debug.Write("An error occurred uploading an image to Imgur.");
        Debug.Write(imgurEx.Message);
    }