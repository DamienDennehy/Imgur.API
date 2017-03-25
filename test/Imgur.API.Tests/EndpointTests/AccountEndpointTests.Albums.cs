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
    public partial class AccountEndpointTests
    {
        [Fact]
        public async Task DeleteAlbumAsync_NotNull()
        {
            var mockUrl = "https://api.imgur.com/3/account/sarah/album/yMgB7";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.DeleteAlbum)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var deleted = await endpoint.DeleteAlbumAsync("yMgB7", "sarah").ConfigureAwait(false);

            Assert.True(deleted);
        }

        [Fact]
        public async Task DeleteAlbumAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.DeleteAlbumAsync("yMgB7", null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task DeleteAlbumAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.DeleteAlbumAsync(null, null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task DeleteAlbumAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.DeleteAlbumAsync("yMgB7", "sarah").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task DeleteAlbumAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.DeleteAlbumAsync("yMgB7", null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetAlbumAsync_NotNull()
        {
            var mockUrl = "https://api.imgur.com/3/account/sarah/album/yMgB7";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetAlbum)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var album = await endpoint.GetAlbumAsync("yMgB7", "sarah").ConfigureAwait(false);

            Assert.NotNull(album);
            Assert.Equal("yMgB7", album.Id);
            Assert.Equal("Day 2 at Camp Imgur", album.Title);
            Assert.Equal(null, album.Description);
            Assert.Equal(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1439066984), album.DateTime);
            Assert.Equal("BOdd9Qd", album.Cover);
            Assert.Equal(5184, album.CoverWidth);
            Assert.Equal(3456, album.CoverHeight);
            Assert.Equal("sarah", album.AccountUrl);
            Assert.Equal(9571, album.AccountId);
            Assert.Equal(AlbumPrivacy.Public, album.Privacy);
            Assert.Equal(AlbumLayout.Blog, album.Layout);
            Assert.Equal(413310, album.Views);
            Assert.Equal("http://imgur.com/a/yMgB7", album.Link);
            Assert.Equal(false, album.Favorite);
            Assert.Equal(false, album.Nsfw);
            Assert.Equal("pics", album.Section);
            Assert.Equal(6, album.ImagesCount);
        }

        [Fact]
        public async Task GetAlbumAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetAlbumAsync("yMgB7", null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetAlbumAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetAlbumAsync(null, null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetAlbumAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetAlbumAsync("yMgB7", null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetAlbumCountAsync_NotNull()
        {
            var mockUrl = "https://api.imgur.com/3/account/sarah/albums/count";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetAlbumCount)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var count = await endpoint.GetAlbumCountAsync("sarah").ConfigureAwait(false);

            Assert.Equal(105, count);
        }

        [Fact]
        public async Task GetAlbumCountAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetAlbumCountAsync().ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetAlbumCountAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetAlbumCountAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetAlbumIdsAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/account/bob/albums/ids/2";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetAlbumIds)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var albums = await endpoint.GetAlbumIdsAsync("bob", 2).ConfigureAwait(false);

            Assert.Equal(50, albums.Count());
        }

        [Fact]
        public async Task GetAlbumIdsAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetAlbumIdsAsync().ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetAlbumIdsAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetAlbumIdsAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetAlbumsAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/account/bob/albums/2";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetAlbums)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var albums = await endpoint.GetAlbumsAsync("bob", 2).ConfigureAwait(false);

            Assert.Equal(50, albums.Count());
        }

        [Fact]
        public async Task GetAlbumsAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetAlbumsAsync().ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetAlbumsAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetAlbumsAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}