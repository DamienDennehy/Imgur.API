# FAQ

## How do I use the Imgur.API with a proxy?
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