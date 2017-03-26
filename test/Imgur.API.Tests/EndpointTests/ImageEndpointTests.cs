using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Models;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.EndpointTests
{
    public class ImageEndpointTests : TestBase
    {
        [Fact]
        public async Task DeleteImageAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/image/123xyj";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageEndpointResponses.Imgur.DeleteImage)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var deleted = await endpoint.DeleteImageAsync("123xyj").ConfigureAwait(false);

            Assert.Equal(true, deleted);
        }

        [Fact]
        public async Task DeleteImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.DeleteImageAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task FavoriteImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.FavoriteImageAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task FavoriteImageAsync_WithImgurClient_IsFalse()
        {
            var mockUrl = "https://api.imgur.com/3/image/zVpyzhW/favorite";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageEndpointResponses.Imgur.FavoriteImageFalse)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new ImageEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var favorited = await endpoint.FavoriteImageAsync("zVpyzhW").ConfigureAwait(false);

            Assert.False(favorited);
        }

        [Fact]
        public async Task FavoriteImageAsync_WithImgurClient_True()
        {
            var mockUrl = "https://api.imgur.com/3/image/zVpyzhW/favorite";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageEndpointResponses.Imgur.FavoriteImageTrue)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new ImageEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var favorited = await endpoint.FavoriteImageAsync("zVpyzhW").ConfigureAwait(false);

            Assert.True(favorited);
        }

        [Fact]
        public async Task FavoriteImageAsync_WithMashapeClient_IsFalse()
        {
            var mockUrl = "https://imgur-apiv3.p.mashape.com/3/image/zVpyzhW/favorite";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageEndpointResponses.Imgur.FavoriteImageFalse)
            };

            var client = new MashapeClient("123", "1234", "xyz", MockOAuth2Token);
            var endpoint = new ImageEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var favorited = await endpoint.FavoriteImageAsync("zVpyzhW").ConfigureAwait(false);

            Assert.False(favorited);
        }

        [Fact]
        public async Task FavoriteImageAsync_WithMashapeClient_True()
        {
            var mockUrl = "https://imgur-apiv3.p.mashape.com/3/image/zVpyzhW/favorite";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageEndpointResponses.Imgur.FavoriteImageTrue)
            };

            var client = new MashapeClient("123", "1234", "xyz", MockOAuth2Token);
            var endpoint = new ImageEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var favorited = await endpoint.FavoriteImageAsync("zVpyzhW").ConfigureAwait(false);

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
        public async Task GetImageAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/image/zVpyzhW";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageEndpointResponses.Imgur.GetImage)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var image = await endpoint.GetImageAsync("zVpyzhW").ConfigureAwait(false);

            Assert.NotNull(image);
            Assert.Equal("zVpyzhW", image.Id);
            Assert.Equal("Look Mom, it's Bambi!", image.Title);
            Assert.Equal(null, image.Description);
            Assert.Equal(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1440259938), image.DateTime);
            Assert.Equal("image/gif", image.Type);
            Assert.Equal(true, image.Animated);
            Assert.Equal(426, image.Width);
            Assert.Equal(240, image.Height);
            Assert.Equal(26270273, image.Size);
            Assert.Equal(3185896, image.Views);
            Assert.InRange(image.Bandwidth, 1, long.MaxValue);
            Assert.Equal(VoteOption.Up, image.Vote);
            Assert.Equal(false, image.Favorite);
            Assert.Equal(false, image.Nsfw);
            Assert.Equal("Eyebleach", image.Section);
            Assert.Equal("http://i.imgur.com/zVpyzhW.gifv", image.Gifv);
            Assert.Equal("http://i.imgur.com/zVpyzhW.mp4", image.Mp4);
            Assert.Equal("http://i.imgur.com/zVpyzhWh.gif", image.Link);
            Assert.Equal(true, image.Looping);
            Assert.Equal(true, image.InGallery);
            Assert.Equal(595876, image.Mp4Size);
        }

        [Fact]
        public async Task GetImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetImageAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task UpdateImageAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/image/123xyj";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageEndpointResponses.Imgur.UpdateImage)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var updated = await endpoint.UpdateImageAsync("123xyj").ConfigureAwait(false);

            Assert.Equal(true, updated);
        }

        [Fact]
        public async Task UpdateImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.UpdateImageAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task UploadImageBinaryAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/image";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageEndpointResponses.Imgur.UploadImage)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var image = await endpoint.UploadImageBinaryAsync(new byte[9]).ConfigureAwait(false);

            Assert.NotNull(image);
            Assert.Equal(true, image.Animated);
            Assert.Equal(0, image.Bandwidth);
            Assert.Equal(new DateTimeOffset(new DateTime(2015, 8, 23, 23, 43, 31, DateTimeKind.Utc)), image.DateTime);
            Assert.Equal("nGxOKC9ML6KyTWQ", image.DeleteHash);
            Assert.Equal("Description Test", image.Description);
            Assert.Equal(false, image.Favorite);
            Assert.Equal("http://i.imgur.com/kiNOcUl.gifv", image.Gifv);
            Assert.Equal(189, image.Height);
            Assert.Equal("kiNOcUl", image.Id);
            Assert.Equal("http://i.imgur.com/kiNOcUl.gif", image.Link);
            Assert.Equal(true, image.Looping);
            Assert.Equal("http://i.imgur.com/kiNOcUl.mp4", image.Mp4);
            Assert.Equal("", image.Name);
            Assert.Equal(null, image.Nsfw);
            Assert.Equal(null, image.Section);
            Assert.Equal(1038889, image.Size);
            Assert.Equal("Title Test", image.Title);
            Assert.Equal("image/gif", image.Type);
            Assert.Equal(0, image.Views);
            Assert.Equal(null, image.Vote);
            Assert.Equal(290, image.Width);
        }

        [Fact]
        public async Task UploadImageBinaryAsync_WithImageNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.UploadImageBinaryAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task UploadImageStreamAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/image";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageEndpointResponses.Imgur.UploadImage)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            IImage image;

            using (var ms = new MemoryStream(new byte[9]))
            {
                image = await endpoint.UploadImageStreamAsync(ms).ConfigureAwait(false);
            }

            Assert.NotNull(image);
            Assert.Equal(true, image.Animated);
            Assert.Equal(0, image.Bandwidth);
            Assert.Equal(new DateTimeOffset(new DateTime(2015, 8, 23, 23, 43, 31, DateTimeKind.Utc)), image.DateTime);
            Assert.Equal("nGxOKC9ML6KyTWQ", image.DeleteHash);
            Assert.Equal("Description Test", image.Description);
            Assert.Equal(false, image.Favorite);
            Assert.Equal("http://i.imgur.com/kiNOcUl.gifv", image.Gifv);
            Assert.Equal(189, image.Height);
            Assert.Equal("kiNOcUl", image.Id);
            Assert.Equal("http://i.imgur.com/kiNOcUl.gif", image.Link);
            Assert.Equal(true, image.Looping);
            Assert.Equal("http://i.imgur.com/kiNOcUl.mp4", image.Mp4);
            Assert.Equal("", image.Name);
            Assert.Equal(null, image.Nsfw);
            Assert.Equal(null, image.Section);
            Assert.Equal(1038889, image.Size);
            Assert.Equal("Title Test", image.Title);
            Assert.Equal("image/gif", image.Type);
            Assert.Equal(0, image.Views);
            Assert.Equal(null, image.Vote);
            Assert.Equal(290, image.Width);
        }

        [Fact]
        public async Task UploadImageUrlAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/image";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageEndpointResponses.Imgur.UploadImage)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var image = await endpoint.UploadImageUrlAsync("http://i.imgur.com/kiNOcUl.gif").ConfigureAwait(false);

            Assert.NotNull(image);
            Assert.Equal(true, image.Animated);
            Assert.Equal(0, image.Bandwidth);
            Assert.Equal(new DateTimeOffset(new DateTime(2015, 8, 23, 23, 43, 31, DateTimeKind.Utc)), image.DateTime);
            Assert.Equal("nGxOKC9ML6KyTWQ", image.DeleteHash);
            Assert.Equal("Description Test", image.Description);
            Assert.Equal(false, image.Favorite);
            Assert.Equal("http://i.imgur.com/kiNOcUl.gifv", image.Gifv);
            Assert.Equal(189, image.Height);
            Assert.Equal("kiNOcUl", image.Id);
            Assert.Equal("http://i.imgur.com/kiNOcUl.gif", image.Link);
            Assert.Equal(true, image.Looping);
            Assert.Equal("http://i.imgur.com/kiNOcUl.mp4", image.Mp4);
            Assert.Equal("", image.Name);
            Assert.Equal(null, image.Nsfw);
            Assert.Equal(null, image.Section);
            Assert.Equal(1038889, image.Size);
            Assert.Equal("Title Test", image.Title);
            Assert.Equal("image/gif", image.Type);
            Assert.Equal(0, image.Views);
            Assert.Equal(null, image.Vote);
            Assert.Equal(290, image.Width);
        }

        [Fact]
        public async Task UploadImageUrlAsync_WithUrlNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.UploadImageUrlAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task UploadStreamBinaryAsync_WithImageNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.UploadImageStreamAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}