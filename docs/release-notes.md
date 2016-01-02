# Release Notes

## Version 3.6.0 (2016-01-03)
* Added Gallery endpoint. Now at feature parity with the official API.
* Updated several enums to prevent name clashes with models.
* Updated all async methods to use ConfigureAwait(false) as per recommended async standards for libraries.
* Updated package to use Json.NET 8.
* Fix for incorrect type on Topic model - Ephemeral is now boolean not string.
* Fix for GetCommentIdsAsync method on AlbumEndpoint - return type is now IEnumerable[int] not IEnumerable[string].
* Fix for CreateComment methods on Comment and GalleryEndpoint - return type is now int not IComment.

## Version 3.5.0 (2015-12-29)
* Added MemeGen endpoint.
* Added Topic endpoint.
* Refactored inheritance on Image > GalleryImage and Album > GalleryAlbum.
* Removed AccountId and AccountUrl from Image model as no longer supported by official Imgur API.
* Removed MemeMetaData model as no longer supported by official Imgur API.

## Version 3.4.0 (2015-12-26)
* Added Custom Gallery endpoint.
* Added Conversation endpoint.
* Added Notification endpoint.
* Updated UpdateRateLimit method to prevent OverflowExceptions.
* Updated comments on all exceptions thrown.
* Moved exceptions to base namespace.

## Version 3.3.0 (2015-12-23)
* Added CommentEndpoint.
* Added AccountUsername to OAuth2Token.
* Added Veto option to Vote enum.
* Replaced Vote string with Vote enum on Image, GalleryImage and GalleryAlbum models.
* Replaced Layout string with AlbumLayout enum on GalleryAlbum.
* Fix for ImgurException not being thrown when Imgur doesn't return a response.

## Version 3.2.0 (2015-12-21)
* Album endpoint added.
* Fixed case sensitivity issue for Gallery methods of the AccountEndpoint.
* Significant rewrite of Account and Image endpoints for additional unit test coverage.

## Version 3.1.0 (2015-11-09)
* Account endpoint added.
* Stream support for uploading images added.
* Made certain fields optional for uploading images.

## Version 3.0.1 (2015-09-20)
* Imgur api support.
* Mashape api support.
* OAuth2 support.
* Image endpoint added.