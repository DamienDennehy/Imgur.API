using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
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

            var fakeOAuth2TokenHandler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var deleted = await endpoint.DeleteImageAsync("hbzm7Ge", "sarah");

            Assert.IsTrue(deleted);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteImageAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var fakeOAuth2TokenHandler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteImageAsync("hbzm7Ge", null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var fakeOAuth2TokenHandler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteImageAsync(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteImageAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteImageAsync("yMgB7", "sarah");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteImageAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var fakeOAuth2TokenHandler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(client);
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

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var image = await endpoint.GetImageAsync("hbzm7Ge", "sarah");

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
            Assert.AreEqual(null, image.AccountUrl);
            Assert.AreEqual(null, image.AccountId);
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
            await endpoint.GetImageAsync("hbzm7Ge", null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetImageAsync(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
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

            var tokenHandler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", tokenHandler.GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var count = await endpoint.GetImageCountAsync("sarah");

            Assert.AreEqual(count, 2);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageCountAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetImageCountAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageCountAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
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

            var tokenHandler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", tokenHandler.GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var images = await endpoint.GetImageIdsAsync("sarah", 2);

            var images2 = images as ICollection;

            Assert.AreEqual(2, images.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageIdsAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetImageIdsAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageIdsAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
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

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/sarah/images/2"),
                fakeResponse);

            var tokenHandler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", tokenHandler.GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var images = await endpoint.GetImagesAsync("sarah", 2);

            Assert.AreEqual(2, images.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImagesAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetImagesAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImagesAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetImagesAsync(null);
        }
    }
}