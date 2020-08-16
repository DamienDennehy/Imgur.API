using System.Collections.Generic;
using Imgur.API.Converters;
using Imgur.API.Models;
using Xunit;

namespace Imgur.API.Tests.ResponseTests
{
    public class AlbumResponseTests
    {
        [Fact]
        public void ConvertResponse_WithGetAlbumResponse_ReturnsAlbum()
        {
            var responseConverter = new ResponseConverter();
            var response = responseConverter.ConvertResponse<Album>(Mocks.MockAlbumResponses.GetAlbum);
            Assert.NotNull(response);
            Assert.IsType<Album>(response);
            Assert.Equal("nIn0Ntl", response.Id);
            Assert.Equal("O", response.Title);
            Assert.Null(response.Description);
            Assert.Equal(1597542190, response.DateTime);
            Assert.Equal("nAYq66G", response.Cover);
            Assert.Null(response.Description);
            Assert.Equal(2400, response.CoverWidth);
            Assert.Equal(2400, response.CoverHeight);
            Assert.Equal("A8XTgSW8pWrNCFwR", response.AccountUrl);
            Assert.Equal(135798223, response.AccountId);
            Assert.Equal("hidden", response.Privacy);
            Assert.Equal("blog", response.Layout);
            Assert.Equal(0, response.Views);
            Assert.Equal("https://imgur.com/a/nIn0Ntl", response.Link);
            Assert.False(response.Favorite);
            Assert.False(response.Nsfw);
            Assert.Null(response.Section);
            Assert.Equal(1, response.ImagesCount);
            Assert.False(response.InGallery);
            Assert.False(response.IsAd);
            Assert.Equal("SkWWotelbmyGqQg", response.DeleteHash);
        }

        [Fact]
        public void ConvertResponse_WithGetAlbumImagesResponse_ReturnsImages()
        {
            var responseConverter = new ResponseConverter();
            var response = responseConverter.ConvertResponse<IEnumerable<Image>>(Mocks.MockAlbumResponses.GetAlbumImages);
            Assert.NotNull(response);
            Assert.IsType<List<Image>>(response);
            Assert.Single(response);
        }

        [Fact]
        public void ConvertResponse_WithGetAlbumImagesResponse_ReturnsImage()
        {
            var responseConverter = new ResponseConverter();
            var response = responseConverter.ConvertResponse<Image>(Mocks.MockAlbumResponses.GetAlbumImage);
            Assert.NotNull(response);
            Assert.IsType<Image>(response);
            Assert.Equal("nAYq66G", response.Id);
        }
    }
}
