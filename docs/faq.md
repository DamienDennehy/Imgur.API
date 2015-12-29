# FAQ

## How do I use Imgur.API with a proxy?
If your application requires the use of a proxy, then this can be set in one of two ways:

### 1. Configure proxy in the App.Config or Web.config file.
Add the following section to your config file.
  
		<system.net>
		  <defaultProxy useDefaultCredentials="true" />
		</system.net>
  
### 2. Configure proxy programmatically.
Set the DefaultWebProxy credentials before any API endpoints are used.
  
		System.Net.WebRequest.DefaultWebProxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
  
A System.Net.Http.HttpRequestException exception is thrown when a proxy is required and the current credentials are not valid.

You should catch this exception if you suspect your application will have proxy issues.

## Why isn't the Memes Metadata supported?
It was removed from the official Imgur API, but Imgur didn't remove it from their own documentation. 
[https://groups.google.com/forum/#!msg/imgur/BEyZryAhGi0/yfOFyixuPy4J](https://groups.google.com/forum/#!msg/imgur/BEyZryAhGi0/yfOFyixuPy4J)

## Why isn't the Reddit comments key supported for Reddit images?
It doesn't appear to be available currently in the official Imgur API.  
[https://groups.google.com/forum/#!topic/imgur/DWJw19wny3A](https://groups.google.com/forum/#!topic/imgur/DWJw19wny3A)
[https://market.mashape.com/imgur/imgur-9/support/48](https://market.mashape.com/imgur/imgur-9/support/48)