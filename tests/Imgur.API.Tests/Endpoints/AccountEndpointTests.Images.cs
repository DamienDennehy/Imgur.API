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
    public partial class AccountEndpointTests
    {
        [TestMethod]
        public async Task DeleteImageAsync_IsNotNull()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.DeleteImageResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/sarah/image/hbzm7Ge"),
                fakeResponse);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var deleted = await endpoint.DeleteImageAsync("hbzm7Ge", "sarah");

            Assert.IsTrue(deleted);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeleteImageAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.DeleteImageAsync("hbzm7Ge", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeleteImageAsync_WithNullId_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.DeleteImageAsync(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeleteImageAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.DeleteImageAsync("hbzm7Ge", null);
        }

        [TestMethod]
        public async Task GetImageAsync_IsNotNull()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetImageResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/sarah/image/hbzm7Ge"),
                fakeResponse);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var image = await endpoint.GetImageAsync("hbzm7Ge", "sarah");

            Assert.IsNotNull(image);
            Assert.AreEqual(image.Title,
                "For three days at Camp Imgur, the Imgur flag flew proudly over our humble redwood camp, greeting Imgurians each morning.");
            Assert.AreEqual(image.Description, null);
            Assert.AreEqual(image.DateTime, new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1443651980));
            Assert.AreEqual(image.Type, "image/gif");
            Assert.AreEqual(image.Animated, true);
            Assert.AreEqual(image.Width, 406);
            Assert.AreEqual(image.Height, 720);
            Assert.AreEqual(image.Size, 23386145);
            Assert.AreEqual(image.Views, 329881);
            Assert.AreEqual(image.Bandwidth, 7714644898745);
            Assert.AreEqual(image.Vote, null);
            Assert.AreEqual(image.Favorite, false);
            Assert.AreEqual(image.Nsfw, null);
            Assert.AreEqual(image.Section, null);
            Assert.AreEqual(image.AccountUrl, null);
            Assert.AreEqual(image.AccountId, null);
            Assert.AreEqual(image.Gifv, "http://i.imgur.com/hbzm7Ge.gifv");
            Assert.AreEqual(image.Webm, "http://i.imgur.com/hbzm7Ge.webm");
            Assert.AreEqual(image.Mp4, "http://i.imgur.com/hbzm7Ge.mp4");
            Assert.AreEqual(image.Link, "http://i.imgur.com/hbzm7Geh.gif");
            Assert.AreEqual(image.Looping, true);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetImageAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetImageAsync("hbzm7Ge", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetImageAsync_WithNullId_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetImageAsync(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetImageAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetImageAsync("hbzm7Ge", null);
        }

        [TestMethod]
        public async Task GetImageCountAsync_IsNotNull()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetImageCountResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/sarah/images/count"),
                fakeResponse);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var count = await endpoint.GetImageCountAsync("sarah");

            Assert.AreEqual(count, 2);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetImageCountAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetImageCountAsync();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetImageCountAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetImageCountAsync(null);
        }

        [TestMethod]
        public async Task GetImageIdsAsync_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetImageIdsResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/sarah/images/ids/2"),
                fakeResponse);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var images = await endpoint.GetImageIdsAsync("sarah", 2);

            Assert.AreEqual(2, images.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetImageIdsAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetImageIdsAsync();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetImageIdsAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetImageIdsAsync(null);
        }

        [TestMethod]
        public async Task GetImagesAsync_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetImagesResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/sarah/images/2"), fakeResponse);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var images = await endpoint.GetImagesAsync("sarah", 2);

            Assert.AreEqual(2, images.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetImagesAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetImagesAsync();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetImagesAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetImagesAsync(null);
        }
    }
}