# Authentication

## Register an Application
In order to use the Imgur api, register an application at [http://api.imgur.com/oauth2/addclient](http://api.imgur.com/oauth2/addclient)

## Using the Imgur Api
Once you have the application registered, you can use it by declaring an instance of the ImgurClient class.

		var client = new ImgurClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");

## Using the Mashape Api
If you will be using Imgur's api for commercial purposes (uploading more than 1250 images per day), 
you will need to use Mashape's api instead of the Imgur api. Register for Mashape at [https://www.mashape.com/imgur/imgur-9](https://www.mashape.com/imgur/imgur-9)

Once you have the application registered, you can use it by declaring an instance of the MashapeClient class.

		var client = new MashapeClient("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", "YOUR_MASHAPE_KEY");

More information on Imgur's api can be found at [http://api.imgur.com/](http://api.imgur.com/)