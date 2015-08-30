### Upload image anonymously using Imgur authentication.

    
    try
    {
        var auth = new ImgurAuthentication("YOUR_CLIENT", "YOUR_SECRET");
        var imageEndpoint = new ImageEndpoint(auth);
        var localImage = System.IO.File.ReadAllBytes(@"D:\Image.jpg");
        var image = await imageEndpoint.UploadImageBinaryAsync(localImage, null, "Awesome pic!", "Took me weeks to get this shot.");
    }
    catch (ImgurException imgurEx)
    {
        Console.WriteLine($"An error occurred uploading an image to Imgur. Error: ({imgurEx.Message})");
        Debug.Write(imgurEx.Message);
        throw;
    }
    catch (Exception ex)
    {
        Console.WriteLine("An unknown error occurred uploading an image to Imgur.");
        Debug.Write(ex.Message);
        throw;
    }

### Upload image anonymously using Mashape authentication.

    
    try
    {
        var auth = new MashapeAuthentication("YOUR_CLIENT", "YOUR_SECRET", "YOUR_MASHAPE_KEY");
        var imageEndpoint = new ImageEndpoint(auth);
        var localImage = System.IO.File.ReadAllBytes(@"D:\Image.jpg");
        var image = await imageEndpoint.UploadImageBinaryAsync(localImage, null, "Awesome pic!", "Took me weeks to get this shot.");
    }
    catch (MashapeException mashapeEx)
    {
        Console.WriteLine($"An error occurred using Mashape Authentication. Error: ({mashapeEx.Message})");
        Debug.Write(mashapeEx.Message);
        throw;
    }
    catch (ImgurException imgurEx)
    {
        Console.WriteLine($"An error occurred uploading an image to Imgur. Error: ({imgurEx.Message})");
        Debug.Write(imgurEx.Message);
        throw;
    }
    catch (Exception ex)
    {
        Console.WriteLine("An unknown error occurred uploading an image to Imgur.");
        Debug.Write(ex.Message);
        throw;
    }