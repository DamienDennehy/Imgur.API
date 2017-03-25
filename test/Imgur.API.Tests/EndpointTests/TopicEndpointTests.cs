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
    public class TopicEndpointTests : TestBase
    {
        [Fact]
        public async Task GetDefaultTopicsAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/topics/defaults";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockTopicEndpointResponses.GetDefaultTopics)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new TopicEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var topics = await endpoint.GetDefaultTopicsAsync().ConfigureAwait(false);
            var topic = topics.FirstOrDefault();

            Assert.NotNull(topic);
            Assert.NotNull(topic.TopPost);
            Assert.NotNull(topic.HeroImage);
        }

        [Fact]
        public async Task GetGalleryTopicItemAsync_NotNull()
        {
            var mockUrl = "https://api.imgur.com/3/topics/Current_Events/xyZ";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockTopicEndpointResponses.GetGalleryTopicItem)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new TopicEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var item = await endpoint.GetGalleryTopicItemAsync("xyZ", "Current Events").ConfigureAwait(false);

            Assert.NotNull(item);
        }

        [Fact]
        public async Task GetGalleryTopicItemAsync_WithGalleryItemIdNull_ThrowsNewArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new TopicEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () =>
                            await endpoint.GetGalleryTopicItemAsync(null, "Current Events").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetGalleryTopicItemAsync_WithTopicIdNull_ThrowsNewArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new TopicEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetGalleryTopicItemAsync("XyZ", null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetGalleryTopicItemsAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/topics/Current_Events/top/day/3";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockTopicEndpointResponses.GetGalleryTopicItems)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new TopicEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var items =
                await
                    endpoint.GetGalleryTopicItemsAsync("Current Events", CustomGallerySortOrder.Top, TimeWindow.Day, 3)
                        .ConfigureAwait(false);

            Assert.True(items.Any());
        }

        [Fact]
        public async Task GetGalleryTopicItemsAsync_WithTopicIdNull_ThrowsNewArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new TopicEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetGalleryTopicItemsAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}