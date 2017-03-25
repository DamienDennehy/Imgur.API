using System;
using System.Collections.Generic;
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
    public class AlbumEndpointTests : TestBase
    {
        [Fact]
        public async Task AddAlbumImagesAsync_True()
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAlbumEndpointResponses.Imgur.AddAlbumImages)
            };

            var mockUrl = "https://api.imgur.com/3/album/12x5454/add";
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));

            var updated =
                await
                    endpoint.AddAlbumImagesAsync("12x5454", new List<string> {"AbcDef", "IrcDef"}).ConfigureAwait(false);

            Assert.True(updated);
        }

        [Fact]
        public async Task AddAlbumImagesAsync_WithAlbumNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.AddAlbumImagesAsync(null, null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task AddAlbumImagesAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.AddAlbumImagesAsync("12x5454", null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task CreateAlbumAsync_Equal()
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAlbumEndpointResponses.Imgur.CreateAlbum)
            };

            var mockUrl = "https://api.imgur.com/3/album";
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var album = await endpoint.CreateAlbumAsync().ConfigureAwait(false);

            Assert.NotNull(album);
            Assert.Equal("3gfxo", album.Id);
            Assert.Equal("iIFJnFpVbYOvzvv", album.DeleteHash);
        }

        [Fact]
        public async Task DeleteAlbumAsync_True()
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAlbumEndpointResponses.Imgur.DeleteAlbum)
            };

            var mockUrl = "https://api.imgur.com/3/album/12x5454";
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var deleted = await endpoint.DeleteAlbumAsync("12x5454").ConfigureAwait(false);

            Assert.True(deleted);
        }

        [Fact]
        public async Task DeleteAlbumAsync_WithAlbumNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.DeleteAlbumAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task FavoriteAlbumAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AlbumEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.FavoriteAlbumAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task FavoriteAlbumAsync_WithImgurClient_IsFalse()
        {
            var mockUrl = "https://api.imgur.com/3/album/zVpyzhW/favorite";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAlbumEndpointResponses.Imgur.FavoriteAlbumFalse)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AlbumEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var favorited = await endpoint.FavoriteAlbumAsync("zVpyzhW").ConfigureAwait(false);

            Assert.False(favorited);
        }

        [Fact]
        public async Task FavoriteAlbumAsync_WithImgurClient_True()
        {
            var mockUrl = "https://api.imgur.com/3/album/zVpyzhW/favorite";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAlbumEndpointResponses.Imgur.FavoriteAlbumTrue)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AlbumEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var favorited = await endpoint.FavoriteAlbumAsync("zVpyzhW").ConfigureAwait(false);

            Assert.True(favorited);
        }

        [Fact]
        public async Task FavoriteAlbumAsync_WithMashapeClient_IsFalse()
        {
            var mockUrl = "https://imgur-apiv3.p.mashape.com/3/album/zVpyzhW/favorite";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAlbumEndpointResponses.Imgur.FavoriteAlbumFalse)
            };

            var client = new MashapeClient("123", "1234", "xyz", MockOAuth2Token);
            var endpoint = new AlbumEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var favorited = await endpoint.FavoriteAlbumAsync("zVpyzhW").ConfigureAwait(false);

            Assert.False(favorited);
        }

        [Fact]
        public async Task FavoriteAlbumAsync_WithMashapeClient_True()
        {
            var mockUrl = "https://imgur-apiv3.p.mashape.com/3/album/zVpyzhW/favorite";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAlbumEndpointResponses.Imgur.FavoriteAlbumTrue)
            };

            var client = new MashapeClient("123", "1234", "xyz", MockOAuth2Token);
            var endpoint = new AlbumEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var favorited = await endpoint.FavoriteAlbumAsync("zVpyzhW").ConfigureAwait(false);

            Assert.True(favorited);
        }

        [Fact]
        public async Task FavoriteImageAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.FavoriteImageAsync("zVpyzhW").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetAlbumAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/album/5F5Cy";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAlbumEndpointResponses.Imgur.GetAlbum)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var album = await endpoint.GetAlbumAsync("5F5Cy").ConfigureAwait(false);

            Assert.NotNull(album);
            Assert.Equal("5F5Cy", album.Id);
            Assert.Equal(null, album.Title);
            Assert.Equal(null, album.Description);
            Assert.Equal(album.DateTime, new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1446591779));
            Assert.Equal("79MH23L", album.Cover);
            Assert.Equal(609, album.CoverWidth);
            Assert.Equal(738, album.CoverHeight);
            Assert.Equal("sarah", album.AccountUrl);
            Assert.Equal(9571, album.AccountId);
            Assert.Equal(AlbumPrivacy.Public, album.Privacy);
            Assert.Equal(AlbumLayout.Blog, album.Layout);
            Assert.Equal(19, album.Views);
            Assert.Equal("http://imgur.com/a/5F5Cy", album.Link);
            Assert.Equal(false, album.Favorite);
            Assert.Equal(null, album.Nsfw);
            Assert.Equal(null, album.Section);
            Assert.Equal(3, album.ImagesCount);
            Assert.Equal(3, album.Images.Count());
            Assert.Equal(false, album.InGallery);
        }

        [Fact]
        public async Task GetAlbumAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetAlbumAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetAlbumImageAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/album/5F5Cy/image/79MH23L";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAlbumEndpointResponses.Imgur.GetAlbumImage)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var image = await endpoint.GetAlbumImageAsync("79MH23L", "5F5Cy").ConfigureAwait(false);

            Assert.NotNull(image);

            Assert.Equal("79MH23L", image.Id);
            Assert.Equal(null, image.Title);
            Assert.Equal(null, image.Description);
            Assert.Equal(image.DateTime, new DateTimeOffset(new DateTime(2015, 11, 3, 23, 03, 03, DateTimeKind.Utc)));
            Assert.Equal("image/png", image.Type);
            Assert.Equal(false, image.Animated);
            Assert.Equal(609, image.Width);
            Assert.Equal(738, image.Height);
            Assert.Equal(451530, image.Size);
            Assert.Equal(2849, image.Views);
            Assert.Equal(1286408970, image.Bandwidth);
            Assert.Equal(null, image.Vote);
            Assert.Equal(false, image.Favorite);
            Assert.Equal(null, image.Nsfw);
            Assert.Equal(null, image.Section);
            Assert.Equal("http://i.imgur.com/79MH23L.png", image.Link);
        }

        [Fact]
        public async Task GetAlbumImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetAlbumImageAsync(null, "xyuOi").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetAlbumImageAsync_WithImageNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetAlbumImageAsync("PioAxs8", null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetAlbumImagesAsync_ImageCountTrue()
        {
            var mockUrl = "https://api.imgur.com/3/album/5F5Cy/images";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAlbumEndpointResponses.Imgur.GetAlbumImages)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var images = await endpoint.GetAlbumImagesAsync("5F5Cy").ConfigureAwait(false);

            Assert.True(images.Count() == 3);
        }

        [Fact]
        public async Task GetAlbumImagesAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetAlbumImagesAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task RemoveAlbumImagesAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/album/12x5454/remove_images?ids=AbcDef%2CIrcDef";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAlbumEndpointResponses.Imgur.RemoveAlbumImages)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var updated =
                await
                    endpoint.RemoveAlbumImagesAsync("12x5454", new List<string> {"AbcDef", "IrcDef"})
                        .ConfigureAwait(false);

            Assert.True(updated);
        }

        [Fact]
        public async Task RemoveAlbumImagesAsync_WithAlbumNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.RemoveAlbumImagesAsync(null, null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task RemoveAlbumImagesAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.RemoveAlbumImagesAsync("12x5454", null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task SetAlbumImagesAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/album/12x5454";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAlbumEndpointResponses.Imgur.SetAlbumImages)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var updated =
                await
                    endpoint.SetAlbumImagesAsync("12x5454", new List<string> {"AbcDef", "IrcDef"}).ConfigureAwait(false);

            Assert.True(updated);
        }

        [Fact]
        public async Task SetAlbumImagesAsync_WithAlbumNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.SetAlbumImagesAsync(null, null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task SetAlbumImagesAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.SetAlbumImagesAsync("12x5454", null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task UpdateAlbumAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/album/12x5454";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAlbumEndpointResponses.Imgur.UpdateAlbum)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var updated = await endpoint.UpdateAlbumAsync("12x5454").ConfigureAwait(false);

            Assert.True(updated);
        }

        [Fact]
        public async Task UpdateAlbumAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.UpdateAlbumAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}