using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.EndpointTests
{
    public partial class GalleryEndpointTests
    {
        [Fact]
        public async Task GetImageAsync_NotNull()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/image/rNdMhHm";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.GetGalleryImage)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var image = await endpoint.GetGalleryImageAsync("rNdMhHm").ConfigureAwait(false);

            Assert.NotNull(image);
            Assert.Equal("rNdMhHm", image.Id);
            Assert.Equal("wanna make money quickly? follow the instruction below", image.Title);
            Assert.Equal("i am not lying. FP edit: may the money cat bless you all with wealth ^^", image.Description);
            Assert.Equal(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1451524552), image.DateTime);
            Assert.Equal("image/png", image.Type);
            Assert.Equal(false, image.Animated);
            Assert.Equal(610, image.Width);
            Assert.Equal(558, image.Height);
            Assert.Equal(564133, image.Size);
            Assert.Equal(923523, image.Views);
            Assert.Equal(520989800559, image.Bandwidth);
            Assert.Equal(null, image.Vote);
            Assert.Equal(false, image.Favorite);
            Assert.Equal(false, image.Nsfw);
            Assert.Equal("pics", image.Section);
            Assert.Equal("Calasin", image.AccountUrl);
            Assert.Equal(22349254, image.AccountId);
            Assert.Equal(619, image.CommentCount);
            Assert.Equal("No Topic", image.Topic);
            Assert.Equal(29, image.TopicId);
            Assert.Equal("http://i.imgur.com/rNdMhHm.png", image.Link);
            Assert.Equal(619, image.CommentCount);
            Assert.Equal(28057, image.Ups);
            Assert.Equal(5924, image.Downs);
            Assert.Equal(22133, image.Points);
            Assert.Equal(22594, image.Score);
            Assert.Null(image.Mp4Size);
        }

        [Fact]
        public async Task GetImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetGalleryImageAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}