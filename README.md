# Imgur.API
Imgur.API is a .NET implementation of Imgur's API. 

![Build](https://github.com/DamienDennehy/Imgur.API/workflows/Build/badge.svg)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=ImgurAPI&metric=alert_status)](https://sonarcloud.io/dashboard?id=ImgurAPI)

[![NuGet](https://img.shields.io/nuget/vpre/Imgur.API.svg)](https://www.nuget.org/packages/Imgur.API/)

## Getting Started

### Register Client
Register your App at https://api.imgur.com/oauth2/addclient

### Creating an ApiClient
~~~
var apiClient = new ApiClient("YOUR_CLIENT_KEY");
~~~

## Using OAuth

## Uploading Images & Video
### Uploading Image
~~~
var apiClient = new ApiClient("YOUR_CLIENT_KEY");
var httpClient = new HttpClient();

var filePath = "PATH_TO_YOUR_IMAGE";
using var fileStream = File.OpenRead(filePath);

var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
var imageUpload = await imageEndpoint.UploadImageAsync(fileStream);
~~~        
### Uploading Video
~~~
var apiClient = new ApiClient("YOUR_CLIENT_KEY");
var httpClient = new HttpClient();

var filePath = "PATH_TO_YOUR_IMAGE";
using var fileStream = File.OpenRead(filePath);

var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
var imageUpload = await imageEndpoint.UploadVideoAsync(fileStream);
~~~       
### Uploading Video with Progress
~~~
var apiClient = new ApiClient("YOUR_CLIENT_KEY");
var httpClient = new HttpClient();

var uploadProgress = new Progress<int>(report);

var filePath = "PATH_TO_YOUR_IMAGE";
using var fileStream = File.OpenRead(filePath);

var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
var imageUpload = await imageEndpoint.UploadVideoAsync(fileStream, progress: uploadProgress);

void report(int byteProgress)
{
    //Do something with the progress here. 
}
~~~   

## API Definition
As of August 2020 two Endpoints are available:
* OAuthEndpoint
* ImageEndpoint

The methods on the endpoints match what is available at the official Imgur API at https://apidocs.imgur.com/