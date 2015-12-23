# Quick Start

## Issues
Report any issues at [https://github.com/DamienDennehy/Imgur.API/issues](https://github.com/DamienDennehy/Imgur.API/issues)

## Latest Release
### Version 3.3.0 (2015-12-23)
* Added CommentEndpoint.
* Added AccountUsername to OAuth2Token.
* Added Veto option to Vote enum.
* Replaced Vote string with Vote enum on Image, GalleryImage and GalleryAlbum models.
* Replaced Layout string with AlbumLayout enum on GalleryAlbum.
* Fix for ImgurException not being thrown when Imgur doesn't return a response.

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
        var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", YOUR_OAUTH2_TOKEN);
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