using System;
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
        public async Task GetGalleryItemVotesAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/RoAjx/votes";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.GetGalleryItemVotes)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var votes = await endpoint.GetGalleryItemVotesAsync("RoAjx").ConfigureAwait(false);

            Assert.NotNull(votes);
            Assert.Equal(751, votes.Downs);
            Assert.Equal(11347, votes.Ups);
        }

        [Fact]
        public async Task GetGalleryItemVotesAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetGalleryItemVotesAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task VoteGalleryItemAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/XoPkL/vote/down";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.VoteGalleryTag)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var voted = await endpoint.VoteGalleryItemAsync("XoPkL", VoteOption.Down).ConfigureAwait(false);

            Assert.NotNull(voted);
        }

        [Fact]
        public async Task VoteGalleryItemAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new GalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.VoteGalleryItemAsync(null, VoteOption.Down).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task VoteGalleryItemAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.VoteGalleryItemAsync("kjkjk", VoteOption.Down).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}