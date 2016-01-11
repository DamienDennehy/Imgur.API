# Imgur.API 
==============

Imgur.API is a .NET implementation of Imgur's v3 API. 
It supports Imgur's free and Mashape's commercial API endpoints.

Visit the [Imgur.API website](http://imgurapi.readthedocs.org/en/latest/) for docs and sample code.

## Quick Start  - Upload image anonymously

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