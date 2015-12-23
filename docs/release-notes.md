# Release Notes

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