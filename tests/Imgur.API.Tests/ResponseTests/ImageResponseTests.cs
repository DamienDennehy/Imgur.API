using System.Linq;
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
            Assert.NotNull(image);
            Assert.IsType<Image>(image);
            Assert.Equal("mvWNMH4", image.Id);
            Assert.Equal("Epic Fail", image.Title);
            Assert.Equal("That's got to hurt", image.Description);
            Assert.Equal(1596483255, image.DateTime);
            Assert.Equal("video/mp4", image.Type);
            Assert.True(image.Animated);
            Assert.Equal(854, image.Width);
            Assert.Equal(482, image.Height);
            Assert.Equal(7701069, image.Size);
            Assert.Equal(915460, image.Views);
            Assert.Equal(7050020626740, image.Bandwidth);
            Assert.Equal("ups", image.Vote);
            Assert.True(image.Favorite);
            Assert.True(image.Nsfw);
            Assert.Equal("viral", image.Section);
            Assert.Equal("https://imgur.com/user/Sarah", image.AccountUrl);
            Assert.Equal(12345679, image.AccountId);
            Assert.True(image.IsAd);
            Assert.True(image.InMostViral);
            Assert.Contains(image.Tags, e => e == "funny");
            Assert.Equal(1, image.AdType);
            Assert.Equal("http://imgur.com", image.AdUrl);
            Assert.True(image.InGallery);
            Assert.Equal("https://i.imgur.com/mvWNMH4.mp4", image.Link);
            Assert.Equal("ABCDEFGH1234", image.DeleteHash);
            Assert.Equal("ouch.mp4", image.Name);
        }
    }
}
