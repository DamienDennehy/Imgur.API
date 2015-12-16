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
        public void DeleteImageRequest_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);

            var url = $"{endpoint.GetEndpointBaseUrl()}image/1234Xyz9";
            var request = endpoint.RequestBuilder.DeleteImageRequest(url);

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
            endpoint.RequestBuilder.DeleteImageRequest(null);
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

        [TestMethod]
        public void GetImageRequest_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);

            var url = $"{endpoint.GetEndpointBaseUrl()}image/1234Xyz";
            var request = endpoint.RequestBuilder.GetImageRequest(url);

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
            endpoint.RequestBuilder.GetImageRequest(null);
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
        public async Task UploadImageBinaryRequest_AreEqual()
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
        public void UploadImageBinaryRequest_WithImageNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            var url = $"{endpoint.GetEndpointBaseUrl()}image";
            endpoint.RequestBuilder.UploadImageBinaryRequest(url, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UploadImageBinaryRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            var image = File.ReadAllBytes("banana.gif");
            endpoint.RequestBuilder.UploadImageBinaryRequest(null, image);
        }

        [TestMethod]
        public async Task UploadImageUrlRequest_AreEqual()
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
        public void UploadImageUrlRequest_WithImageNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            var url = $"{endpoint.GetEndpointBaseUrl()}image";
            endpoint.RequestBuilder.UploadImageBinaryRequest(url, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UploadImageUrlRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            endpoint.RequestBuilder.UploadImageBinaryRequest(null, null);
        }

        [TestMethod]
        public async Task UploadStreamBinaryRequest_AreEqual()
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
        public void UploadStreamBinaryRequest_WithImageNull_ThrowsArgumentNullException()
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
        [ExpectedException(typeof (ArgumentNullException))]
        public void UploadStreamBinaryRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(imgurClient);
            using (var fs = new FileStream("banana.gif", FileMode.Open))
            {
                endpoint.RequestBuilder.UploadImageStreamRequest(null, fs);
            }
        }
        
        [TestMethod]
        public void GetImageCountRequest_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);

            var url = $"{endpoint.GetEndpointBaseUrl()}account/bob/images/count";
            var request = endpoint.ImageRequestBuilder.GetImageCountRequest(url);

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/account/bob/images/count", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Get, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetImageCountRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            endpoint.ImageRequestBuilder.GetImageCountRequest(null);
        }

        [TestMethod]
        public void GetImageIdsRequest_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);

            var url = $"{endpoint.GetEndpointBaseUrl()}account/bob/images/ids/2";
            var request = endpoint.ImageRequestBuilder.GetImageIdsRequest(url);

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/account/bob/images/ids/2", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Get, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetImageIdsRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            endpoint.ImageRequestBuilder.GetImageIdsRequest(null);
        }

        [TestMethod]
        public void GetImagesRequest_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);

            var url = $"{endpoint.GetEndpointBaseUrl()}account/bob/images/2";
            var request = endpoint.ImageRequestBuilder.GetImagesRequest(url);

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/account/bob/images/2", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Get, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetImagesRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            endpoint.ImageRequestBuilder.GetImagesRequest(null);
        }
    }
}