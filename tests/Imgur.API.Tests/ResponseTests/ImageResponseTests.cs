using System.Linq;
using Imgur.API.Models;
using Imgur.API.ResponseConverters;
using Xunit;

namespace Imgur.API.Tests.ResponseTests
{
    public class ImageResponseTests
    {
        [Fact]
        public void ConvertResponse_WithGetImageResponse_ReturnsImage()
        {
            var basicResponseConverter = new ResponseConverter();
            var response = basicResponseConverter.ConvertResponse<Image>(Mocks.MockImageResponses.GetImage);
            Assert.NotNull(response);
            Assert.IsType<Image>(response);
            Assert.Equal("mvWNMH4", response.Id);
            Assert.Equal("Epic Fail", response.Title);
            Assert.Equal("That's got to hurt", response.Description);
            Assert.Equal(1596483255, response.DateTime);
            Assert.Equal("video/mp4", response.Type);
            Assert.True(response.Animated);
            Assert.Equal(854, response.Width);
            Assert.Equal(482, response.Height);
            Assert.Equal(7701069, response.Size);
            Assert.Equal(915460, response.Views);
            Assert.Equal(7050020626740, response.Bandwidth);
            Assert.Equal("ups", response.Vote);
            Assert.True(response.Favorite);
            Assert.True(response.Nsfw);
            Assert.Equal("viral", response.Section);
            Assert.Equal("https://imgur.com/user/Sarah", response.AccountUrl);
            Assert.Equal(12345679, response.AccountId);
            Assert.True(response.IsAd);
            Assert.True(response.InMostViral);
            Assert.Contains(response.Tags, e => e == "funny");
            Assert.Equal(1, response.AdType);
            Assert.Equal("http://imgur.com", response.AdUrl);
            Assert.True(response.InGallery);
            Assert.Equal("https://i.imgur.com/mvWNMH4.mp4", response.Link);
            Assert.Equal("ABCDEFGH1234", response.DeleteHash);
            Assert.Equal("ouch.mp4", response.Name);
        }

        [Fact]
        public void ConvertResponse_WithUploadImageResponse_ReturnsImage()
        {
            var basicResponseConverter = new ResponseConverter();
            var response = basicResponseConverter.ConvertResponse<Image>(Mocks.MockImageResponses.UploadImage);
            Assert.NotNull(response);
            Assert.IsType<Image>(response);
            Assert.Equal("mvWNMH4", response.Id);
            Assert.Equal("Epic Fail", response.Title);
            Assert.Equal("That's got to hurt", response.Description);
            Assert.Equal(1596483255, response.DateTime);
            Assert.Equal("video/mp4", response.Type);
            Assert.True(response.Animated);
            Assert.Equal(854, response.Width);
            Assert.Equal(482, response.Height);
            Assert.Equal(7701069, response.Size);
            Assert.Equal(915460, response.Views);
            Assert.Equal(7050020626740, response.Bandwidth);
            Assert.Equal("ups", response.Vote);
            Assert.True(response.Favorite);
            Assert.True(response.Nsfw);
            Assert.Equal("viral", response.Section);
            Assert.Equal("https://imgur.com/user/Sarah", response.AccountUrl);
            Assert.Equal(12345679, response.AccountId);
            Assert.True(response.IsAd);
            Assert.True(response.InMostViral);
            Assert.Contains(response.Tags, e => e == "funny");
            Assert.Equal(1, response.AdType);
            Assert.Equal("http://imgur.com", response.AdUrl);
            Assert.True(response.InGallery);
            Assert.Equal("https://i.imgur.com/mvWNMH4.mp4", response.Link);
            Assert.Equal("ABCDEFGH1234", response.DeleteHash);
            Assert.Equal("ouch.mp4", response.Name);
        }

        [Fact]
        public void ConvertResponse_WithFavoriteImageResponse_ReturnsString()
        {
            var basicResponseConverter = new ResponseConverter();
            var response = basicResponseConverter.ConvertResponse<string>(Mocks.MockImageResponses.FavoriteImage);
            Assert.NotNull(response);
            Assert.IsType<string>(response);
            Assert.Equal("favorited", response);
        }

        [Fact]
        public void ConvertResponse_WithDeleteImageResponse_ReturnsBoolean()
        {
            var basicResponseConverter = new ResponseConverter();
            var response = basicResponseConverter.ConvertResponse<bool>(Mocks.MockImageResponses.DeleteImage);
            Assert.IsType<bool>(response);
            Assert.True(response);
        }
    }
}
