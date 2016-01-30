# Quick Start

## Issues
Report any issues at [https://github.com/DamienDennehy/Imgur.API/issues](https://github.com/DamienDennehy/Imgur.API/issues)

## Get Image 

		public async Task GetImage()
		{
			try
			{
				var client = new ImgurClient("CLIENT_ID", "CLIENT_SECRET");
				var endpoint = new ImageEndpoint(client);
				var image = await endpoint.GetImageAsync("IMAGE_ID");
				Debug.Write("Image retrieved. Image Url: " + image.Link);
			}
			catch (ImgurException imgurEx)
			{
				Debug.Write("An error occurred getting an image from Imgur.");
				Debug.Write(imgurEx.Message);
			}
		}
		
## Upload Image

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
