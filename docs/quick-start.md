# Quick Start

## Issues
Report any issues at [https://github.com/DamienDennehy/Imgur.API/issues](https://github.com/DamienDennehy/Imgur.API/issues)

## Latest Release
### Version 3.5.0 (2015-12-29)
* Added MemeGen endpoint.
* Added Topic endpoint.
* Refactored inheritance on Image > GalleryImage and Album > GalleryAlbum.
* Removed AccountId and AccountUrl from Image model as no longer supported by official Imgur API.
* Removed MemeMetaData model as no longer supported by official Imgur API.

## Quick Start
### Upload image anonymously

		public async Task UploadImage()
		{
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
		}