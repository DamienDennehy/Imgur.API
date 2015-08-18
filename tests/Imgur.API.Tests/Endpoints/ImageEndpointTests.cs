using Imgur.API.Endpoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class ImageEndpointTests
    {
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
    }
}
