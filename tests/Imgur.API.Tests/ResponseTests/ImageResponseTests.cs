using System;
using System.Collections.Generic;
using System.Text;
using Imgur.API.Models;
using Imgur.API.ResponseConverters;
using Xunit;

namespace Imgur.API.Tests.ResponseTests
{
    public class ImageResponseTests
    {
        [Fact]
        public void ConvertResponse_WithImageResponse_ReturnsImage()
        {
            var basicResponseConverter = new ResponseConverter();
            var image = basicResponseConverter.ConvertResponse<Image>(Mocks.MockImageResponses.GetImage);
            Assert.IsType<Image>(image);
            Assert.Equal("mvWNMH4", image.Id);
            Assert.Equal("Epic Fail", image.Title);
            Assert.Equal("That's got to hurt", image.Description);
        }
    }
}
