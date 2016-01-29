using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.EndpointTests
{
    public partial class GalleryEndpointTests
    {
        [TestMethod]
        public async Task GetGalleryItemTagsAsync_DefaultParameters_Any()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/xTYm/tags";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.GetGalleryItemTags)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var tags = await endpoint.GetGalleryItemTagsAsync("xTYm").ConfigureAwait(false);

            Assert.IsTrue(tags.Tags.Any());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryItemTagsAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetGalleryItemTagsAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetGalleryTagAsync_DefaultParameters_AreEqual()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/t/cats/viral/week/";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.GetGalleryTag)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var tag = await endpoint.GetGalleryTagAsync("cats").ConfigureAwait(false);

            Assert.IsNotNull(tag);
            Assert.AreEqual(196, tag.Followers);
            Assert.AreEqual(false, tag.Following);
            Assert.AreEqual(60, tag.Items.Count());
            Assert.AreEqual("cats", tag.Name);
            Assert.AreEqual(10920, tag.TotalItems);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryTagAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetGalleryTagAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetGalleryTagImageAsync_DefaultParameters_AreEqual()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/t/cats/XoPkL";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.GetGalleryTagImage)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var image = await endpoint.GetGalleryTagImageAsync("XoPkL", "cats").ConfigureAwait(false);

            Assert.IsNotNull(image);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryTagImageAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetGalleryTagImageAsync(null, "xiui").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryTagImageAsync_WithTagNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetGalleryTagImageAsync("kjkjk", null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task VoteGalleryTagAsync_IsTrue()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/XoPkL/vote/tag/cats/down";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.VoteGalleryTag)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var voted = await endpoint.VoteGalleryTagAsync("XoPkL", "cats", VoteOption.Down).ConfigureAwait(false);

            Assert.IsNotNull(voted);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task VoteGalleryTagAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new GalleryEndpoint(client);
            await endpoint.VoteGalleryTagAsync(null, "xiui", VoteOption.Down).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task VoteGalleryTagAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.VoteGalleryTagAsync("kjkjk", "hjhj", VoteOption.Down).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task VoteGalleryTagAsync_WithTagNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new GalleryEndpoint(client);
            await endpoint.VoteGalleryTagAsync("kjkjk", null, VoteOption.Down).ConfigureAwait(false);
        }
    }
}