using Imgur.API.Models;
using Imgur.API.ResponseConverters;
using Xunit;

namespace Imgur.API.Tests.ResponseConverterTests
{
    public class ResponseConverterTests
    {
        [Fact]
        public void ThrowImgurExceptionIfResponseIsNull_WithNullResponse_ThrowsImgurException()
        {
            var responseConverter = new ResponseConverter();

            var exception = Record.Exception(() =>
            {
                responseConverter.ThrowImgurExceptionIfResponseIsNull(null);
            });
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
            Assert.Contains("missing", exception.Message);
        }

        [Fact]
        public void ThrowImgurExceptionIfResponseIsInvalid_WithInvalidResponse_ThrowsImgurException()
        {
            var responseConverter = new ResponseConverter();

            var exception = Record.Exception(() =>
            {
                responseConverter.ThrowImgurExceptionIfResponseIsInvalid("x");
            });
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
            Assert.Contains("invalid", exception.Message);
        }

        [Fact]
        public void ThrowImgurExceptionIfResponseDataContainsRawError_WithRawError_ThrowsImgurException()
        {
            var responseConverter = new ResponseConverter();

            var exception = Record.Exception(() =>
            {
                responseConverter.ThrowImgurExceptionIfResponseContainsRawError(Mocks.MockResponses.RawError);
            });
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
            Assert.Contains("An error occurred", exception.Message);
        }

        [Fact]
        public void ThrowImgurExceptionIfResponseDataContainsError_WithError_ThrowsImgurException()
        {
            var responseConverter = new ResponseConverter();

            var exception = Record.Exception(() =>
            {
                responseConverter.ThrowImgurExceptionIfResponseContainsError(Mocks.MockResponses.Error);
            });
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
            Assert.Contains("Imgur is over capacity", exception.Message);
        }

        [Fact]
        public void ThrowImgurExceptionIfResponseDataContainsErrorMessage_WithErrorMessage_ThrowsImgurException()
        {
            var responseConverter = new ResponseConverter();

            var exception = Record.Exception(() =>
            {
                responseConverter.ThrowImgurExceptionIfResponseContainsErrorMessage(Mocks.MockResponses.ErrorMessage);
            });
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
            Assert.Contains("Could not complete request", exception.Message);
        }

        [Fact]
        public void ThrowImgurExceptionIfResponseNotSuccess_WithUnsuccessfulMessage_ThrowsImgurException()
        {
            var responseConverter = new ResponseConverter();

            var exception = Record.Exception(() =>
            {
                responseConverter.ThrowImgurExceptionIfResponseNotSuccess(Mocks.MockResponses.Error);
            });
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
            Assert.Contains("Imgur is over capacity", exception.Message);
        }

        [Fact]
        public void ConvertOAuth2TokenResponse_WithOAuth2TokenResponse_ReturnsOAuth2Token()
        {
            var responseConverter = new ResponseConverter();
            var oauth2Token = responseConverter.ConvertOAuth2TokenResponse(Mocks.MockOAuth2Responses.GetTokenResponse);
            Assert.NotNull(oauth2Token);
            Assert.IsType<OAuth2Token>(oauth2Token);
        }
    }
}
