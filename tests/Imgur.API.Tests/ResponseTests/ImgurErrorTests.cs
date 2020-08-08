using System.Linq;
using System.Text.Json;
using Imgur.API.Models;
using Imgur.API.ResponseConverters;
using Xunit;

namespace Imgur.API.Tests.ResponseTests
{
    public class ImgurErrorTests
    {
        [Fact]
        public void ConvertResponse_WithErrorResponse_ReturnsImgurError()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var errorResponse = JsonSerializer.Deserialize<Basic<ImgurError>>(Mocks.MockResponses.Error, options);
            Assert.NotNull(errorResponse);
            Assert.IsType<Basic<ImgurError>>(errorResponse);
            Assert.Equal(1203, errorResponse.Status);
        }
    }
}
