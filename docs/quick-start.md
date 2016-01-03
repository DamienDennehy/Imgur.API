# Quick Start

## Issues
Report any issues at [https://github.com/DamienDennehy/Imgur.API/issues](https://github.com/DamienDennehy/Imgur.API/issues)

## Latest Release
### Version 3.6.0 (2016-01-03)
* Added Gallery endpoint. All endpoints now completed and at feature parity with the official API.
* Added Notoriety property to Account model, calculated from the Reputation property.
* Updated several enums to prevent name clashes with models.
* Updated library to use Json.NET 8.
* Updated library to use PCL Profile111 from PCL Profile7.
* Fix for incorrect type on Topic model - Ephemeral is now boolean not string.
* Fix for GetCommentIdsAsync method on AlbumEndpoint - return type is now IEnumerable int not IEnumerable string.
* Fix for CreateComment methods on Comment and GalleryEndpoint - return type is now int not IComment.
* Fix for GetComment methods on Comment and GalleryEndpoint - commentId is now int not string.

## Quick Start
### Upload image anonymously

		public async Task UploadImage()
		{
			try
			{
				var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
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