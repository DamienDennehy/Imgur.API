using Imgur.API.Models;
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

            var exception = Record.Exception(() => basicConverter.ThrowImgurExceptionIfResponseContainsRawError(Mocks.MockErrorResponses.ImgurRawError));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
            Assert.Contains("An error occurred", exception.Message);
        }

        [Fact]
        public void ThrowImgurExceptionIfResponseDataContainsError_WithError_ThrowsImgurException()
        {
            var basicConverter = new BasicResponseConverter();

            var exception = Record.Exception(() => basicConverter.ThrowImgurExceptionIfResponseContainsError(Mocks.MockErrorResponses.ImgurError));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
            Assert.Contains("Imgur is over capacity", exception.Message);
        }

        [Fact]
        public void ThrowImgurExceptionIfResponseDataContainsErrorMessage_WithErrorMessage_ThrowsImgurException()
        {
            var basicConverter = new BasicResponseConverter();

            var exception = Record.Exception(() => basicConverter.ThrowImgurExceptionIfResponseContainsErrorMessage(Mocks.MockErrorResponses.ImgurErrorMessage));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
            Assert.Contains("Could not complete request", exception.Message);
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
