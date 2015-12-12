using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.RequestBuilders
{
    [TestClass]
    public class ImageRequestBuilderTests
    {
        [TestMethod]
        public void GetImageRequest_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);

            var url = $"{endpoint.GetEndpointBaseUrl()}image/1234Xyz";
            var request = endpoint.RequestBuilder.GetImageRequest(url, "1234Xyz");

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/image/1234Xyz", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Get, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetImageRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            endpoint.RequestBuilder.GetImageRequest(null, "1234Xyz");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetImageRequest_WithIdNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            var url = $"{endpoint.GetEndpointBaseUrl()}/image/1234Xyz";
            endpoint.RequestBuilder.GetImageRequest(url, null);
        }

        [TestMethod]
        public async Task UploadImageBinary_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            var url = $"{endpoint.GetEndpointBaseUrl()}image";

            var image = File.ReadAllBytes("banana.gif");
            var request = endpoint.RequestBuilder.UploadImageBinaryRequest(url, image, "TheAlbum", "TheTitle",
                "TheDescription");

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/image", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);

            var content = (MultipartFormDataContent) request.Content;
            var imageContent =
                (ByteArrayContent) content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "image");
            var album = (StringContent) content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "album");
            var type = (StringContent) content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "type");
            var title = (StringContent) content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "title");
            var description =
                (StringContent) content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "description");

            Assert.IsNotNull(imageContent);
            Assert.IsNotNull(type);
            Assert.IsNotNull(album);
            Assert.IsNotNull(title);
            Assert.IsNotNull(description);

            Assert.AreEqual(image.Length, imageContent.Headers.ContentLength);
            Assert.AreEqual("file", await type.ReadAsStringAsync());
            Assert.AreEqual("TheAlbum", await album.ReadAsStringAsync());
            Assert.AreEqual("TheTitle", await title.ReadAsStringAsync());
            Assert.AreEqual("TheDescription", await description.ReadAsStringAsync());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UploadImageBinary_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            var image = File.ReadAllBytes("banana.gif");
            endpoint.RequestBuilder.UploadImageBinaryRequest(null, image);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UploadImageBinary_WithImageNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            var url = $"{endpoint.GetEndpointBaseUrl()}image";
            endpoint.RequestBuilder.UploadImageBinaryRequest(url, null);
        }

        [TestMethod]
        public async Task UploadStreamBinary_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            var url = $"{endpoint.GetEndpointBaseUrl()}image";

            using (var fs = new FileStream("banana.gif", FileMode.Open))
            {
                var imageLength = fs.Length;
                var request = endpoint.RequestBuilder.UploadImageStreamRequest(url, fs, "TheAlbum", "TheTitle",
                    "TheDescription");

                Assert.IsNotNull(request);
                Assert.AreEqual("https://api.imgur.com/3/image", request.RequestUri.ToString());
                Assert.AreEqual(HttpMethod.Post, request.Method);

                var content = (MultipartFormDataContent) request.Content;
                var imageContent =
                    (StreamContent) content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "image");
                var album = (StringContent) content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "album");
                var type = (StringContent) content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "type");
                var title = (StringContent) content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "title");
                var description =
                    (StringContent) content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "description");

                Assert.IsNotNull(imageContent);
                Assert.IsNotNull(type);
                Assert.IsNotNull(album);
                Assert.IsNotNull(title);
                Assert.IsNotNull(description);

                var image = await imageContent.ReadAsByteArrayAsync();

                Assert.AreEqual(imageLength, image.Length);
                Assert.AreEqual("file", await type.ReadAsStringAsync());
                Assert.AreEqual("TheAlbum", await album.ReadAsStringAsync());
                Assert.AreEqual("TheTitle", await title.ReadAsStringAsync());
                Assert.AreEqual("TheDescription", await description.ReadAsStringAsync());
            }
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UploadStreamBinary_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            using (var fs = new FileStream("banana.gif", FileMode.Open))
            {
                endpoint.RequestBuilder.UploadImageStreamRequest(null, fs);
            }
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UploadStreamBinary_WithImageNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            var url = $"{endpoint.GetEndpointBaseUrl()}image";
            using (var fs = new FileStream("banana.gif", FileMode.Open))
            {
                endpoint.RequestBuilder.UploadImageStreamRequest(url, null);
            }
        }

        [TestMethod]
        public async Task UploadImageUrl_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            var url = $"{endpoint.GetEndpointBaseUrl()}image";

            var request = endpoint.RequestBuilder.UploadImageUrlRequest(url, "http://i.imgur.com/hxsPLa7.jpg",
                "TheAlbum", "TheTitle", "TheDescription");

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/image", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);

            var expected = await request.Content.ReadAsStringAsync();

            Assert.AreEqual(
                "type=URL&image=http%3A%2F%2Fi.imgur.com%2FhxsPLa7.jpg&album=TheAlbum&title=TheTitle&description=TheDescription",
                expected);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UploadImageUrl_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            endpoint.RequestBuilder.UploadImageBinaryRequest(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UploadImageUrl_WithImageNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            var url = $"{endpoint.GetEndpointBaseUrl()}image";
            endpoint.RequestBuilder.UploadImageBinaryRequest(url, null);
        }

        [TestMethod]
        public void DeleteImageRequest_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);

            var url = $"{endpoint.GetEndpointBaseUrl()}image/1234Xyz9";
            var request = endpoint.RequestBuilder.DeleteImageRequest(url, "1234Xyz9");

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/image/1234Xyz9", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Delete, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void DeleteImageRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            endpoint.RequestBuilder.DeleteImageRequest(null, "1234Xyz9");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void DeleteImageRequest_WithIdNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            var url = $"{endpoint.GetEndpointBaseUrl()}/image/1234Xyz";
            endpoint.RequestBuilder.DeleteImageRequest(url, null);
        }

        [TestMethod]
        public async Task UpdateImageRequest_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);

            var url = $"{endpoint.GetEndpointBaseUrl()}image/1234Xyz9";
            var request = endpoint.RequestBuilder.UpdateImageRequest(url, "TheTitle", "TheDescription");

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/image/1234Xyz9", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);

            var expected = await request.Content.ReadAsStringAsync();

            Assert.AreEqual("title=TheTitle&description=TheDescription", expected);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UpdateImageRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            endpoint.RequestBuilder.UpdateImageRequest(null, "1234Xyz9");
        }

        [TestMethod]
        public void FavoriteImageRequest_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);

            var url = $"{endpoint.GetEndpointBaseUrl()}image/1234Xyz9/favorite";
            var request = endpoint.RequestBuilder.FavoriteImageRequest(url);

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/image/1234Xyz9/favorite", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void FavoriteImageRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            endpoint.RequestBuilder.FavoriteImageRequest(null);
        }
    }
}