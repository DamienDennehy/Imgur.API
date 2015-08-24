using System;
using Imgur.API.Endpoints;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class ImageEndpointTests
    {
        private const string GetImageResponse = "{\"data\":{\"id\":\"zVpyzhW\",\"title\":\"Look Mom, it's Bambi!\",\"description\":null,\"datetime\":1440259938,\"type\":\"image/gif\",\"animated\":true,\"width\":426,\"height\":240,\"size\":26270273,\"views\":1583864,\"bandwidth\":41608539674872,\"vote\":null,\"favorite\":false,\"nsfw\":false,\"section\":\"Eyebleach\",\"account_url\":\"ForAGoodTimeCall8675309\",\"account_id\":23095506,\"comment_preview\":null,\"gifv\":\"http://i.imgur.com/zVpyzhW.gifv\",\"webm\":\"http://i.imgur.com/zVpyzhW.webm\",\"mp4\":\"http://i.imgur.com/zVpyzhW.mp4\",\"link\":\"http://i.imgur.com/zVpyzhWh.gif\",\"looping\":true},\"success\":true,\"status\":200}";
        private const string UploadImageResponse = "{\"data\":{\"id\":\"kiNOcUl\",\"title\":\"Title Test\",\"description\":\"Description Test\",\"datetime\":1440373411,\"type\":\"image/gif\",\"animated\":true,\"width\":290,\"height\":189,\"size\":1038889,\"views\":0,\"bandwidth\":0,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":24234234,\"comment_preview\":null,\"deletehash\":\"nGxOKC9ML6KyTWQ\",\"name\":\"\",\"gifv\":\"http://i.imgur.com/kiNOcUl.gifv\",\"webm\":\"http://i.imgur.com/kiNOcUl.webm\",\"mp4\":\"http://i.imgur.com/kiNOcUl.mp4\",\"link\":\"http://i.imgur.com/kiNOcUl.gif\",\"looping\":true},\"success\":true,\"status\":200}";
        private const string FavoriteImageTrue = "{\"data\":\"favorited\",\"success\":true,\"status\":200}";
        private const string FavoriteImageFalse = "{\"data\":\"unfavorited\",\"success\":true,\"status\":200}";

        [TestMethod]
        public void ImageEndpoint_GetImageAsync_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IImageEndpoint>();
            endpoint.GetImageAsync("1234");
            endpoint.Received().GetImageAsync("1234");
        }

        [TestMethod]
        public void ImageEndpoint_UploadImageBinaryAsync_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IImageEndpoint>();
            var image = new byte[] { 0x20 };
            endpoint.UploadImageBinaryAsync(image, "1234", "t1234", "d1234");
            endpoint.Received().UploadImageBinaryAsync(image, "1234", "t1234", "d1234");
        }

        [TestMethod]
        public void ImageEndpoint_UploadImageBase64Async_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IImageEndpoint>();
            endpoint.UploadImageBase64Async("abcd", "1234", "t1234", "d1234");
            endpoint.Received().UploadImageBase64Async("abcd", "1234", "t1234", "d1234");
        }

        [TestMethod]
        public void ImageEndpoint_UploadImageUrlAsync_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IImageEndpoint>();
            endpoint.UploadImageUrlAsync("http://imgur.com/something", "1234", "t1234", "d1234");
            endpoint.Received().UploadImageUrlAsync("http://imgur.com/something", "1234", "t1234", "d1234");
        }

        [TestMethod]
        public void ImageEndpoint_DeleteImageAsync_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IImageEndpoint>();
            endpoint.DeleteImageAsync("12345");
            endpoint.Received().DeleteImageAsync("12345");
        }

        [TestMethod]
        public void ImageEndpoint_UpdateImageAsync_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IImageEndpoint>();
            endpoint.UpdateImageAsync("id12345", "title12345", "desc12345");
            endpoint.Received().UpdateImageAsync("id12345", "title12345", "desc12345");
        }

        [TestMethod]
        public void ImageEndpoint_FavoriteImageAsync_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IImageEndpoint>();
            endpoint.FavoriteImageAsync("id12345");
            endpoint.Received().FavoriteImageAsync("id12345");
        }

        [TestMethod]
        public void ImageEndpoint_GetImageResponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var image = endpoint.ProcessEndpointResponse<Image>(GetImageResponse);

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

        [TestMethod]
        public void ImageEndpoint_UploadImageResponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var image = endpoint.ProcessEndpointResponse<Image>(UploadImageResponse);

            Assert.AreEqual("kiNOcUl", image.Id);
            Assert.AreEqual("Title Test", image.Title);
            Assert.AreEqual("Description Test", image.Description);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1440373411), image.DateTime);
            Assert.AreEqual("image/gif", image.Type);
            Assert.AreEqual(true, image.Animated);
            Assert.AreEqual(290, image.Width);
            Assert.AreEqual(189, image.Height);
            Assert.AreEqual(1038889, image.Size);
            Assert.AreEqual(0, image.Views);
            Assert.AreEqual(0, image.Bandwidth);
            Assert.AreEqual(null, image.Vote);
            Assert.AreEqual(false, image.Favorite);
            Assert.AreEqual(null, image.Nsfw);
            Assert.AreEqual(null, image.Section);
            Assert.AreEqual(null, image.AccountUrl);
            Assert.AreEqual("24234234", image.AccountId);
            Assert.AreEqual("nGxOKC9ML6KyTWQ", image.DeleteHash);
            Assert.AreEqual("http://i.imgur.com/kiNOcUl.gifv", image.Gifv);
            Assert.AreEqual("http://i.imgur.com/kiNOcUl.webm", image.Webm);
            Assert.AreEqual("http://i.imgur.com/kiNOcUl.mp4", image.Mp4);
            Assert.AreEqual("http://i.imgur.com/kiNOcUl.gif", image.Link);
            Assert.AreEqual(true, image.Looping);
        }

        [TestMethod]
        public void ImageEndpoint_FavouriteImageTrueResponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var response = endpoint.ProcessEndpointResponse<string>(FavoriteImageTrue);
            
            Assert.AreEqual("favorited", response);
        }

        [TestMethod]
        public void ImageEndpoint_FavouriteImageFalseResponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var response = endpoint.ProcessEndpointResponse<string>(FavoriteImageFalse);

            Assert.AreEqual("unfavorited", response);
        }
    }
}