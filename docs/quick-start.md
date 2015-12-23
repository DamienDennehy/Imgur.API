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
### Upload image anonymously

    try
    {
        var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");
        var endpoint = new ImageEndpoint(client);
		IImage image;
        using (var fs = new FileStream(@"IMAGE_LOCATION", FileMode.Open))
        {
            image = await endpoint.UploadImageStreamAsync(fs);
        }
		Debug.Write("Image uploaded. Image Url: " + image.Link);
    }
    catch (ImgurException imgurEx)
    {
        Debug.Write("An error occurred uploading an image to Imgur.");
        Debug.Write(imgurEx.Message);
    }