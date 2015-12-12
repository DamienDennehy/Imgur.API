using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Tests.FakeResponses;
using Imgur.API.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class ImageEndpointTests
    {
        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageAsync_WithNullId_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            await endpoint.GetImageAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task UploadImageBinaryAsync_WithNullImage_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            await endpoint.UploadImageBinaryAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task UploadStreamBinaryAsync_WithNullImage_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            await endpoint.UploadImageStreamAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task UploadImageUrlAsync_WithNullUrl_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            await endpoint.UploadImageUrlAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteImageAsync_WithNullId_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            await endpoint.DeleteImageAsync(null);
        }

        [TestMethod]
        public async Task DeleteImageAsync_AreEqual()
        {
            //Create a fake message handler
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ImageEndpointResponses.Imgur.DeleteImageResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/image/123xyj"), fakeResponse);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var deleted = await endpoint.DeleteImageAsync("123xyj");

            Assert.AreEqual(true, deleted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task FavoriteImageAsync_WithNullId_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            await endpoint.FavoriteImageAsync(null);
        }

        [TestMethod]
        public async Task GetImageResponse_AreEqual()
        {
            //Create a fake message handler
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ImageEndpointResponses.Imgur.GetImageResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/image/zVpyzhW"), fakeResponse);
            
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
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
            Assert.AreEqual(null, image.Vote);
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
    }
}