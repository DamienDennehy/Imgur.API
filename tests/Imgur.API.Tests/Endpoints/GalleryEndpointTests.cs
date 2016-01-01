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
    public partial class GalleryEndpointTests : TestBase
    {
        [TestMethod]
        public async Task GetGalleryAsync_DefaultParameters_Any()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/hot/viral/day/?showViral=true";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.GetGalleryAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var gallery = await endpoint.GetGalleryAsync().ConfigureAwait(false);

            Assert.IsTrue(gallery.Any());
        }

        [TestMethod]
        public async Task GetGalleryAsync_WithUserRisingMonth2ShowViralFalse_Any()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/user/rising/month/2?showViral=false";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.GetGalleryAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var gallery = await endpoint.GetGalleryAsync(GallerySection.User,
                GallerySortOrder.Rising,
                TimeWindow.Month, 2, false).ConfigureAwait(false);

            Assert.IsTrue(gallery.Any());
        }

        [TestMethod]
        public async Task GetRandomGalleryAsync_DefaultParameters_Any()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/random/random/";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.GetRandomGalleryAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var gallery = await endpoint.GetRandomGalleryAsync().ConfigureAwait(false);

            Assert.IsTrue(gallery.Any());
        }

        [TestMethod]
        public async Task GetRandomGalleryAsync_WithPage_Any()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/random/random/8";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.GetRandomGalleryAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var gallery = await endpoint.GetRandomGalleryAsync(8).ConfigureAwait(false);

            Assert.IsTrue(gallery.Any());
        }

        [TestMethod]
        public async Task PublishToGalleryAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/xyZ";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.PublishToGalleryAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var published = await endpoint.PublishToGalleryAsync("xyZ", "Test Title").ConfigureAwait(false);

            Assert.IsTrue(published);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task PublishToGalleryAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new GalleryEndpoint(client);
            await endpoint.PublishToGalleryAsync(null, "Xyz", "ahj", true, true).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task PublishToGalleryAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.PublishToGalleryAsync("x48989", "Xyz", "ahj", true, true).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task PublishToGalleryAsync_WithTopicNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.PublishToGalleryAsync("x48989", null, "ahj", true, true).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task RemoveFromGalleryAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/xyZ";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.RemoveFromGalleryAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var removed = await endpoint.RemoveFromGalleryAsync("xyZ").ConfigureAwait(false);

            Assert.IsTrue(removed);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task RemoveFromGalleryAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new GalleryEndpoint(client);
            await endpoint.RemoveFromGalleryAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task RemoveFromGalleryAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.RemoveFromGalleryAsync("x48989").ConfigureAwait(false);
        }

        [TestMethod]
        public async Task SearchGalleryAdvancedAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/search/top/week/2";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.SearchGalleryAdvancedAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var galleries =
                await
                    endpoint.SearchGalleryAdvancedAsync("star wars", "luke han leia", "Obi-Wan", "Kirk",
                        ImageFileType.Anigif, ImageSize.Lrg, GallerySortOrder.Top, TimeWindow.Week, 2).ConfigureAwait(false);

            Assert.IsTrue(galleries.Any());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task SearchGalleryAdvancedAsync_WithQueriesNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.SearchGalleryAdvancedAsync().ConfigureAwait(false);
        }

        [TestMethod]
        public async Task SearchGalleryAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/search/top/week/2";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.SearchGalleryAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var galleries = await endpoint.SearchGalleryAsync("star wars", GallerySortOrder.Top, TimeWindow.Week, 2).ConfigureAwait(false);

            Assert.IsTrue(galleries.Any());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task SearchGalleryAsync_WithQueryNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.SearchGalleryAsync(null).ConfigureAwait(false);
        }
    }
}