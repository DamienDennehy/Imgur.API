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

namespace Imgur.API.Tests.Endpoints
{
    public partial class GalleryEndpointTests
    {
        [TestMethod]
        public async Task GetGalleryItemTagsAsync_DefaultParameters_Any()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/xTYm/tags";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.GetGalleryItemTagsAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var tags = await endpoint.GetGalleryItemTagsAsync("xTYm");

            Assert.IsTrue(tags.Tags.Any());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryItemTagsAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetGalleryItemTagsAsync(null);
        }

        [TestMethod]
        public async Task GetGalleryTagAsync_DefaultParameters_AreEqual()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/t/cats/viral/week/";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.GetGalleryTagAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var tag = await endpoint.GetGalleryTagAsync("cats");

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
            await endpoint.GetGalleryTagAsync(null);
        }

        [TestMethod]
        public async Task GetGalleryTagImageAsync_DefaultParameters_AreEqual()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/t/cats/XoPkL";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.GetGalleryTagImageAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var image = await endpoint.GetGalleryTagImageAsync("XoPkL", "cats");

            Assert.IsNotNull(image);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryTagImageAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetGalleryTagImageAsync(null, "xiui");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryTagImageAsync_WithTagNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetGalleryTagImageAsync("kjkjk", null);
        }

        [TestMethod]
        public async Task VoteGalleryTagAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/XoPkL/vote/tag/cats/down";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.VoteGalleryTagAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var voted = await endpoint.VoteGalleryTagAsync("XoPkL", "cats", VoteOption.Down);

            Assert.IsNotNull(voted);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task VoteGalleryTagAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new GalleryEndpoint(client);
            await endpoint.VoteGalleryTagAsync(null, "xiui", VoteOption.Down);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task VoteGalleryTagAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.VoteGalleryTagAsync("kjkjk", "hjhj", VoteOption.Down);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task VoteGalleryTagAsync_WithTagNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new GalleryEndpoint(client);
            await endpoint.VoteGalleryTagAsync("kjkjk", null, VoteOption.Down);
        }
    }
}