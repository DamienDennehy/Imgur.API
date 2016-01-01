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
    public partial class GalleryEndpointTests
    {
        [TestMethod]
        public async Task GetSubredditGalleryAsync_DefaultParameters_Any()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/r/pics/time/week/";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.GetSubredditGalleryAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var gallery = await endpoint.GetSubredditGalleryAsync("pics").ConfigureAwait(false);

            Assert.IsTrue(gallery.Any());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetSubredditGalleryAsync_WithSubRedditNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetSubredditGalleryAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetSubredditGalleryAsync_WithTopYearPage7_Any()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/r/pics/time/week/7";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.GetMemesSubGalleryAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var gallery =
                await
                    endpoint.GetSubredditGalleryAsync("pics", SubredditGallerySortOrder.Time, TimeWindow.Week, 7)
                        .ConfigureAwait(false);

            Assert.IsTrue(gallery.Any());
        }

        [TestMethod]
        public async Task GetSubredditImageAsync_IsNotNull()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/r/pics/xyP";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.GetSubredditImageAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var image = await endpoint.GetSubredditImageAsync("xyP", "pics").ConfigureAwait(false);

            Assert.IsNotNull(image);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetSubredditImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetSubredditImageAsync(null, "test").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetSubredditImageAsync_WithSubRedditNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetSubredditImageAsync("hhjkh", null).ConfigureAwait(false);
        }
    }
}