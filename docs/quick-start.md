# Quick Start

## Issues
Report any issues at [https://github.com/DamienDennehy/Imgur.API/issues](https://github.com/DamienDennehy/Imgur.API/issues)

## Latest Release
### Version 3.4.0 (2015-12-26)
* Added Custom Gallery endpoint.
* Added Conversation endpoint.
* Added Notification endpoint.
* Updated UpdateRateLimit method to prevent OverflowExceptions.
* Updated comments on all exceptions thrown.
* Moved exceptions to base namespace.

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