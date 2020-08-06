﻿using Imgur.API.Models;
using Imgur.API.ResponseConverters;
using Xunit;

namespace Imgur.API.Tests.ResponseConverterTests
{
    public class BasicConverterTests
    {
        [Fact]
        public void ThrowImgurExceptionIfResponseIsNull_WithNullResponse_ThrowsImgurException()
        {
            var basicConverter = new BasicResponseConverter();

            var exception = Record.Exception(() => basicConverter.ThrowImgurExceptionIfResponseIsNull(null));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
            Assert.Contains("missing", exception.Message);
        }

        [Fact]
        public void ThrowImgurExceptionIfResponseIsInvalid_WithInvalidResponse_ThrowsImgurException()
        {
            var basicConverter = new BasicResponseConverter();

            var exception = Record.Exception(() => basicConverter.ThrowImgurExceptionIfResponseIsInvalid("x"));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
            Assert.Contains("invalid", exception.Message);
        }

        [Fact]
        public void ThrowImgurExceptionIfResponseDataContainsRawError_WithRawError_ThrowsImgurException()
        {
            var basicConverter = new BasicResponseConverter();

            var exception = Record.Exception(() => basicConverter.ThrowImgurExceptionIfResponseContainsRawError(Mocks.MockErrorResponses.RawError));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
            Assert.Contains("An error occurred", exception.Message);
        }

        [Fact]
        public void ThrowImgurExceptionIfResponseDataContainsError_WithError_ThrowsImgurException()
        {
            var basicConverter = new BasicResponseConverter();

            var exception = Record.Exception(() => basicConverter.ThrowImgurExceptionIfResponseContainsError(Mocks.MockErrorResponses.Error));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
            Assert.Contains("Imgur is over capacity", exception.Message);
        }

        [Fact]
        public void ThrowImgurExceptionIfResponseDataContainsErrorMessage_WithErrorMessage_ThrowsImgurException()
        {
            var basicConverter = new BasicResponseConverter();

            var exception = Record.Exception(() => basicConverter.ThrowImgurExceptionIfResponseContainsErrorMessage(Mocks.MockErrorResponses.ErrorMessage));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
            Assert.Contains("Could not complete request", exception.Message);
        }

        [Fact]
        public void ThrowImgurExceptionIfResponseNotSuccess_WithUnsuccessfulMessage_ThrowsImgurException()
        {
            var basicConverter = new BasicResponseConverter();

            var exception = Record.Exception(() => basicConverter.ThrowImgurExceptionIfResponseNotSuccess(Mocks.MockErrorResponses.Error));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
            Assert.Contains("Imgur is over capacity", exception.Message);
        }

        [Fact]
        public void GetOAuth2Token_WithOAuth2TokenResponse_ReturnsOAuth2Token()
        {
            var basicResponseConverter = new BasicResponseConverter();
            var oauth2Token = basicResponseConverter.GetOAuth2Token<IOAuth2Token>(Mocks.MockOAuth2Responses.GetTokenResponse);
            Assert.NotNull(oauth2Token);
            Assert.IsType<OAuth2Token>(oauth2Token);
        }

        [Fact]
        public void ConvertResponse_WithImageResponse_ReturnsImage()
        {
            var basicResponseConverter = new BasicResponseConverter();
            var image = basicResponseConverter.ConvertResponse<Image>(Mocks.MockImageResponses.GetImage);
            Assert.IsType<Image>(image);
        }
    }
}
