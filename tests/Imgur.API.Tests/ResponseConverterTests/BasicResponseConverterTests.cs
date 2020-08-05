using Imgur.API.Models;
using Imgur.API.ResponseConverters;
using Xunit;

namespace Imgur.API.Tests.ResponseConverterTests
{
    public class BasicConverterTests
    {
        [Fact]
        public void ProcessResponse_WithNullResponse_ThrowsImgurException()
        {
            var basicConverter = new BasicResponseConverter();

            var exception = Record.Exception(() => basicConverter.ConvertResponse<Image>(null));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
        }

        [Fact]
        public void ConvertResponse_WithImageResponse_ReturnsImage()
        {
            var basicResponseConverter = new BasicResponseConverter();
            var image = basicResponseConverter.ConvertResponse<Image>(EndpointResponses.ImageEndpointResponses.GetImage);
            Assert.IsType<Image>(image);
        }
    }
}
