using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Models;
using Imgur.API.Tests.FakeResponses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class ImageEndpointTests : TestBase
    {
        [TestMethod]
        public async Task DeleteImageAsync_AreEqual()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ImageEndpointResponses.Imgur.DeleteImageResponse)
            };

            FakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/image/123xyj"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            var deleted = await endpoint.DeleteImageAsync("123xyj");

            Assert.AreEqual(true, deleted);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);
            await endpoint.DeleteImageAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task FavoriteImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);
            await endpoint.FavoriteImageAsync(null);
        }

        [TestMethod]
        public async Task FavoriteImageAsync_WithImgurClient_IsFalse()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ImageEndpointResponses.Imgur.FavoriteImageResponseFalse)
            };

            FakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/image/zVpyzhW/favorite"),
                fakeResponse);

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ImageEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            var favorited = await endpoint.FavoriteImageAsync("zVpyzhW");

            Assert.IsFalse(favorited);
        }

        [TestMethod]
        public async Task FavoriteImageAsync_WithImgurClient_IsTrue()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ImageEndpointResponses.Imgur.FavoriteImageResponseTrue)
            };

            FakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/image/zVpyzhW/favorite"),
                fakeResponse);

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new ImageEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            var favorited = await endpoint.FavoriteImageAsync("zVpyzhW");

            Assert.IsTrue(favorited);
        }

        [TestMethod]
        public async Task FavoriteImageAsync_WithMashapeClient_IsFalse()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ImageEndpointResponses.Mashape.FavoriteImageResponseFalse)
            };

            FakeHttpMessageHandler.AddFakeResponse(
                new Uri("https://imgur-apiv3.p.mashape.com/3/image/zVpyzhW/favorite"), fakeResponse);

            var client = new MashapeClient("123", "1234", "xyz", FakeOAuth2Token);
            var endpoint = new ImageEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            var favorited = await endpoint.FavoriteImageAsync("zVpyzhW");

            Assert.IsFalse(favorited);
        }

        [TestMethod]
        public async Task FavoriteImageAsync_WithMashapeClient_IsTrue()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ImageEndpointResponses.Mashape.FavoriteImageResponseTrue)
            };

            FakeHttpMessageHandler.AddFakeResponse(
                new Uri("https://imgur-apiv3.p.mashape.com/3/image/zVpyzhW/favorite"), fakeResponse);

            var client = new MashapeClient("123", "1234", "xyz", FakeOAuth2Token);
            var endpoint = new ImageEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            var favorited = await endpoint.FavoriteImageAsync("zVpyzhW");

            Assert.IsTrue(favorited);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task FavoriteImageAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);
            await endpoint.FavoriteImageAsync("zVpyzhW");
        }

        [TestMethod]
        public async Task GetImageAsync_AreEqual()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ImageEndpointResponses.Imgur.GetImageResponse)
            };

            FakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/image/zVpyzhW"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            var image = await endpoint.GetImageAsync("zVpyzhW");

            Assert.IsNotNull(image);
            Assert.AreEqual("zVpyzhW", image.Id);
            Assert.AreEqual("Look Mom, it's Bambi!", image.Title);
            Assert.AreEqual(null, image.Description);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1440259938), image.DateTime);
            Assert.AreEqual("image/gif", image.Type);
            Assert.AreEqual(true, image.Animated);
            Assert.AreEqual(426, image.Width);
            Assert.AreEqual(240, image.Height);
            Assert.AreEqual(26270273, image.Size);
            Assert.AreEqual(1583864, image.Views);
            Assert.AreEqual(41608539674872, image.Bandwidth);
            Assert.AreEqual(Vote.Up, image.Vote);
            Assert.AreEqual(false, image.Favorite);
            Assert.AreEqual(false, image.Nsfw);
            Assert.AreEqual("Eyebleach", image.Section);
            Assert.AreEqual("ForAGoodTimeCall8675309", image.AccountUrl);
            Assert.AreEqual("23095506", image.AccountId);
            Assert.AreEqual("http://i.imgur.com/zVpyzhW.gifv", image.Gifv);
            Assert.AreEqual("http://i.imgur.com/zVpyzhW.webm", image.Webm);
            Assert.AreEqual("http://i.imgur.com/zVpyzhW.mp4", image.Mp4);
            Assert.AreEqual("http://i.imgur.com/zVpyzhWh.gif", image.Link);
            Assert.AreEqual(true, image.Looping);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);
            await endpoint.GetImageAsync(null);
        }

        [TestMethod]
        public async Task UpdateImageAsync_AreEqual()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ImageEndpointResponses.Imgur.UpdateImageResponse)
            };

            FakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/image/123xyj"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            var updated = await endpoint.UpdateImageAsync("123xyj");

            Assert.AreEqual(true, updated);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task UpdateImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);
            await endpoint.UpdateImageAsync(null);
        }

        [TestMethod]
        public async Task UploadImageBinaryAsync_AreEqual()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ImageEndpointResponses.Imgur.UploadImageResponse)
            };

            FakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/image"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            var image = await endpoint.UploadImageBinaryAsync(File.ReadAllBytes("banana.gif"));

            Assert.IsNotNull(image);
            Assert.AreEqual("24234234", image.AccountId);
            Assert.AreEqual(null, image.AccountUrl);
            Assert.AreEqual(true, image.Animated);
            Assert.AreEqual(0, image.Bandwidth);
            Assert.AreEqual(new DateTimeOffset(new DateTime(2015, 8, 23, 23, 43, 31, DateTimeKind.Utc)), image.DateTime);
            Assert.AreEqual("nGxOKC9ML6KyTWQ", image.DeleteHash);
            Assert.AreEqual("Description Test", image.Description);
            Assert.AreEqual(false, image.Favorite);
            Assert.AreEqual("http://i.imgur.com/kiNOcUl.gifv", image.Gifv);
            Assert.AreEqual(189, image.Height);
            Assert.AreEqual("kiNOcUl", image.Id);
            Assert.AreEqual("http://i.imgur.com/kiNOcUl.gif", image.Link);
            Assert.AreEqual(true, image.Looping);
            Assert.AreEqual("http://i.imgur.com/kiNOcUl.mp4", image.Mp4);
            Assert.AreEqual("", image.Name);
            Assert.AreEqual(null, image.Nsfw);
            Assert.AreEqual(null, image.Section);
            Assert.AreEqual(1038889, image.Size);
            Assert.AreEqual("Title Test", image.Title);
            Assert.AreEqual("image/gif", image.Type);
            Assert.AreEqual(0, image.Views);
            Assert.AreEqual(null, image.Vote);
            Assert.AreEqual("http://i.imgur.com/kiNOcUl.webm", image.Webm);
            Assert.AreEqual(290, image.Width);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task UploadImageBinaryAsync_WithImageNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);
            await endpoint.UploadImageBinaryAsync(null);
        }

        [TestMethod]
        public async Task UploadImageStreamAsync_AreEqual()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ImageEndpointResponses.Imgur.UploadImageResponse)
            };

            FakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/image"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            IImage image;

            using (var fs = new FileStream("banana.gif", FileMode.Open))
            {
                image = await endpoint.UploadImageStreamAsync(fs);
            }

            Assert.IsNotNull(image);
            Assert.AreEqual("24234234", image.AccountId);
            Assert.AreEqual(null, image.AccountUrl);
            Assert.AreEqual(true, image.Animated);
            Assert.AreEqual(0, image.Bandwidth);
            Assert.AreEqual(new DateTimeOffset(new DateTime(2015, 8, 23, 23, 43, 31, DateTimeKind.Utc)), image.DateTime);
            Assert.AreEqual("nGxOKC9ML6KyTWQ", image.DeleteHash);
            Assert.AreEqual("Description Test", image.Description);
            Assert.AreEqual(false, image.Favorite);
            Assert.AreEqual("http://i.imgur.com/kiNOcUl.gifv", image.Gifv);
            Assert.AreEqual(189, image.Height);
            Assert.AreEqual("kiNOcUl", image.Id);
            Assert.AreEqual("http://i.imgur.com/kiNOcUl.gif", image.Link);
            Assert.AreEqual(true, image.Looping);
            Assert.AreEqual("http://i.imgur.com/kiNOcUl.mp4", image.Mp4);
            Assert.AreEqual("", image.Name);
            Assert.AreEqual(null, image.Nsfw);
            Assert.AreEqual(null, image.Section);
            Assert.AreEqual(1038889, image.Size);
            Assert.AreEqual("Title Test", image.Title);
            Assert.AreEqual("image/gif", image.Type);
            Assert.AreEqual(0, image.Views);
            Assert.AreEqual(null, image.Vote);
            Assert.AreEqual("http://i.imgur.com/kiNOcUl.webm", image.Webm);
            Assert.AreEqual(290, image.Width);
        }

        [TestMethod]
        public async Task UploadImageUrlAsync_AreEqual()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ImageEndpointResponses.Imgur.UploadImageResponse)
            };

            FakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/image"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            var image = await endpoint.UploadImageUrlAsync("http://i.imgur.com/kiNOcUl.gif");

            Assert.IsNotNull(image);
            Assert.AreEqual("24234234", image.AccountId);
            Assert.AreEqual(null, image.AccountUrl);
            Assert.AreEqual(true, image.Animated);
            Assert.AreEqual(0, image.Bandwidth);
            Assert.AreEqual(new DateTimeOffset(new DateTime(2015, 8, 23, 23, 43, 31, DateTimeKind.Utc)), image.DateTime);
            Assert.AreEqual("nGxOKC9ML6KyTWQ", image.DeleteHash);
            Assert.AreEqual("Description Test", image.Description);
            Assert.AreEqual(false, image.Favorite);
            Assert.AreEqual("http://i.imgur.com/kiNOcUl.gifv", image.Gifv);
            Assert.AreEqual(189, image.Height);
            Assert.AreEqual("kiNOcUl", image.Id);
            Assert.AreEqual("http://i.imgur.com/kiNOcUl.gif", image.Link);
            Assert.AreEqual(true, image.Looping);
            Assert.AreEqual("http://i.imgur.com/kiNOcUl.mp4", image.Mp4);
            Assert.AreEqual("", image.Name);
            Assert.AreEqual(null, image.Nsfw);
            Assert.AreEqual(null, image.Section);
            Assert.AreEqual(1038889, image.Size);
            Assert.AreEqual("Title Test", image.Title);
            Assert.AreEqual("image/gif", image.Type);
            Assert.AreEqual(0, image.Views);
            Assert.AreEqual(null, image.Vote);
            Assert.AreEqual("http://i.imgur.com/kiNOcUl.webm", image.Webm);
            Assert.AreEqual(290, image.Width);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task UploadImageUrlAsync_WithUrlNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);
            await endpoint.UploadImageUrlAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task UploadStreamBinaryAsync_WithImageNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);
            await endpoint.UploadImageStreamAsync(null);
        }
    }
}