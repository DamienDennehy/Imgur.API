# Running Integration Tests

## App.config settings
Integration Tests require several settings to be set in the App.config file.

These variables contain the Imgur and Mashape authentication settings.

**ClientId**: The Imgur App ClientId.

**ClientSecret**: The Imgur App ClientSecret.

**MashapeKey**: The Mashape Key for commercial applications.

**RefreshToken**: An OAuth2 refresh token for testing Imgur API methods that require user authentication.

## Preventing App.config commits
As the App.config file shouldn't be committed using your ClientId and Secret, after checking out the 
Imgur.API repository open a Git Shell and run the following command:

git update-index --assume-unchanged .\tests\Imgur.API.Tests.Integration\App.config

This will prevent changes to the App.config file being commited as long as the repository exists on your machine.
