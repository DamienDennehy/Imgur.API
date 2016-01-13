using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Tests.FakeResponses;
using Imgur.API.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class TopicEndpointTests : TestBase
    {
        [TestMethod]
        public async Task GetDefaultTopicsAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/topics/defaults";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(TopicEndpointResponses.GetDefaultTopicsAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new TopicEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var topics = await endpoint.GetDefaultTopicsAsync().ConfigureAwait(false);
            var topic = topics.FirstOrDefault();

            Assert.IsNotNull(topic);
            Assert.IsNotNull(topic.TopPost);
        }

        [TestMethod]
        public async Task GetGalleryTopicItemAsync_IsNotNull()
        {
            var fakeUrl = "https://api.imgur.com/3/topics/Current_Events/xyZ";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(TopicEndpointResponses.GetGalleryTopicItemAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new TopicEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var item = await endpoint.GetGalleryTopicItemAsync("xyZ", "Current Events").ConfigureAwait(false);

            Assert.IsNotNull(item);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryTopicItemAsync_WithGalleryItemIdNull_ThrowsNewArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new TopicEndpoint(client);
            await endpoint.GetGalleryTopicItemAsync(null, "Current Events").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryTopicItemAsync_WithTopicIdNull_ThrowsNewArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new TopicEndpoint(client);
            await endpoint.GetGalleryTopicItemAsync("XyZ", null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetGalleryTopicItemsAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/topics/Current_Events/top/day/3";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(TopicEndpointResponses.GetGalleryTopicItemsAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new TopicEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var items =
                await
                    endpoint.GetGalleryTopicItemsAsync("Current Events", CustomGallerySortOrder.Top, TimeWindow.Day, 3)
                        .ConfigureAwait(false);

            Assert.IsTrue(items.Any());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryTopicItemsAsync_WithTopicIdNull_ThrowsNewArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new TopicEndpoint(client);
            await endpoint.GetGalleryTopicItemsAsync(null).ConfigureAwait(false);
        }
    }
}