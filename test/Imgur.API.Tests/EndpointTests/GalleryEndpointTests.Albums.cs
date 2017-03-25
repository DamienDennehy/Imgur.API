using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.EndpointTests
{
    public partial class GalleryEndpointTests
    {
        [Fact]
        public async Task GetAlbumAsync_NotNull()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/album/dO484";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.GetGalleryAlbum)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var album = await endpoint.GetGalleryAlbumAsync("dO484").ConfigureAwait(false);

            Assert.NotNull(album);
            Assert.Equal("dO484", album.Id);
            Assert.Equal("25 Films on Netflix I'd like to recommend.", album.Title);
            Assert.Equal(null, album.Description);
            Assert.Equal(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1451445880), album.DateTime);
            Assert.Equal("xUPbs5g", album.Cover);
            Assert.Equal(700, album.CoverWidth);
            Assert.Equal(394, album.CoverHeight);
            Assert.Equal("bellsofwar3", album.AccountUrl);
            Assert.Equal(28720941, album.AccountId);
            Assert.Equal(AlbumPrivacy.Public, album.Privacy);
            Assert.Equal(AlbumLayout.Blog, album.Layout);
            Assert.Equal(13972, album.Views);
            Assert.Equal("http://imgur.com/a/dO484", album.Link);
            Assert.Equal(2024, album.Ups);
            Assert.Equal(28, album.Downs);
            Assert.Equal(1996, album.Points);
            Assert.Equal(1994, album.Score);
            Assert.Equal(null, album.Vote);
            Assert.Equal(false, album.Favorite);
            Assert.Equal(false, album.Nsfw);
            Assert.Equal("", album.Section);
            Assert.Equal(79, album.CommentCount);
            Assert.Equal("The More You Know", album.Topic);
            Assert.Equal(11, album.TopicId);
            Assert.Equal(25, album.ImagesCount);
            Assert.Equal(25, album.Images.Count());
        }

        [Fact]
        public async Task GetAlbumAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetGalleryAlbumAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}