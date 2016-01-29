using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.EndpointTests
{
    public partial class AccountEndpointTests
    {
        [TestMethod]
        public async Task DeleteImageAsync_IsNotNull()
        {
            var mockUrl = "https://api.imgur.com/3/account/sarah/image/hbzm7Ge";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.DeleteImage)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var deleted = await endpoint.DeleteImageAsync("hbzm7Ge", "sarah").ConfigureAwait(false);

            Assert.IsTrue(deleted);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteImageAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteImageAsync("hbzm7Ge", null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteImageAsync(null, null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteImageAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteImageAsync("yMgB7", "sarah").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteImageAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteImageAsync("hbzm7Ge", null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetImageAsync_IsNotNull()
        {
            var mockUrl = "https://api.imgur.com/3/account/sarah/image/hbzm7Ge";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetImage)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var image = await endpoint.GetImageAsync("hbzm7Ge", "sarah").ConfigureAwait(false);

            Assert.IsNotNull(image);
            Assert.AreEqual(
                "For three days at Camp Imgur, the Imgur flag flew proudly over our humble redwood camp, greeting Imgurians each morning.",
                image.Title);
            Assert.AreEqual(null, image.Description);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1443651980), image.DateTime);
            Assert.AreEqual("image/gif", image.Type);
            Assert.AreEqual(true, image.Animated);
            Assert.AreEqual(406, image.Width);
            Assert.AreEqual(720, image.Height);
            Assert.AreEqual(23386145, image.Size);
            Assert.AreEqual(329881, image.Views);
            Assert.AreEqual(7714644898745, image.Bandwidth);
            Assert.AreEqual(null, image.Vote);
            Assert.AreEqual(false, image.Favorite);
            Assert.AreEqual(null, image.Nsfw);
            Assert.AreEqual(null, image.Section);
            Assert.AreEqual("http://i.imgur.com/hbzm7Ge.gifv", image.Gifv);
            Assert.AreEqual("http://i.imgur.com/hbzm7Ge.webm", image.Webm);
            Assert.AreEqual("http://i.imgur.com/hbzm7Ge.mp4", image.Mp4);
            Assert.AreEqual("http://i.imgur.com/hbzm7Geh.gif", image.Link);
            Assert.AreEqual(true, image.Looping);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetImageAsync("hbzm7Ge", null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetImageAsync(null, null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetImageAsync("hbzm7Ge", null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetImageCountAsync_IsNotNull()
        {
            var mockUrl = "https://api.imgur.com/3/account/sarah/images/count";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetImageCount)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var count = await endpoint.GetImageCountAsync("sarah").ConfigureAwait(false);

            Assert.AreEqual(count, 2);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageCountAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetImageCountAsync().ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageCountAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetImageCountAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetImageIdsAsync_AreEqual()
        {
            var mockUrl = "https://api.imgur.com/3/account/sarah/images/ids/2";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetImageIds)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var images = await endpoint.GetImageIdsAsync("sarah", 2).ConfigureAwait(false);

            Assert.AreEqual(2, images.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageIdsAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetImageIdsAsync().ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageIdsAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetImageIdsAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetImagesAsync_AreEqual()
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetImages)
            };

            var mockUrl = "https://api.imgur.com/3/account/sarah/images/2";

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var images = await endpoint.GetImagesAsync("sarah", 2).ConfigureAwait(false);

            Assert.AreEqual(2, images.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImagesAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetImagesAsync().ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImagesAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetImagesAsync(null).ConfigureAwait(false);
        }
    }
}