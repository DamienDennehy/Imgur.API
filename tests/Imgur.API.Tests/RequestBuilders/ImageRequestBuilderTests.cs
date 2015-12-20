﻿using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.RequestBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.RequestBuilders
{
    [TestClass]
    public class ImageRequestBuilderTests
    {
        [TestMethod]
        public async Task UpdateImageRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new ImageRequestBuilder();

            var url = $"{client.EndpointUrl}image/1234Xyz9";
            var request = requestBuilder.UpdateImageRequest(url, "TheTitle", "TheDescription");

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
            var requestBuilder = new ImageRequestBuilder();
            requestBuilder.UpdateImageRequest(null, "1234Xyz9");
        }

        [TestMethod]
        public async Task UploadImageBinaryRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new ImageRequestBuilder();
            var url = $"{client.EndpointUrl}image";

            var image = File.ReadAllBytes("banana.gif");
            var request = requestBuilder.UploadImageBinaryRequest(url, image, "TheAlbum", "TheTitle",
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
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new ImageRequestBuilder();
            var url = $"{client.EndpointUrl}image";
            requestBuilder.UploadImageBinaryRequest(url, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UploadImageBinaryRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new ImageRequestBuilder();
            var image = File.ReadAllBytes("banana.gif");
            requestBuilder.UploadImageBinaryRequest(null, image);
        }

        [TestMethod]
        public async Task UploadImageUrlRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new ImageRequestBuilder();
            var url = $"{client.EndpointUrl}image";

            var request = requestBuilder.UploadImageUrlRequest(url, "http://i.imgur.com/hxsPLa7.jpg",
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
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new ImageRequestBuilder();
            var url = $"{client.EndpointUrl}image";
            requestBuilder.UploadImageBinaryRequest(url, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UploadImageUrlRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new ImageRequestBuilder();
            requestBuilder.UploadImageBinaryRequest(null, null);
        }

        [TestMethod]
        public async Task UploadStreamBinaryRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new ImageRequestBuilder();
            var url = $"{client.EndpointUrl}image";

            using (var fs = new FileStream("banana.gif", FileMode.Open))
            {
                var imageLength = fs.Length;
                var request = requestBuilder.UploadImageStreamRequest(url, fs, "TheAlbum", "TheTitle",
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
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new ImageRequestBuilder();
            var url = $"{client.EndpointUrl}image";
            using (var fs = new FileStream("banana.gif", FileMode.Open))
            {
                requestBuilder.UploadImageStreamRequest(url, null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UploadStreamBinaryRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new ImageRequestBuilder();
            using (var fs = new FileStream("banana.gif", FileMode.Open))
            {
                requestBuilder.UploadImageStreamRequest(null, fs);
            }
        }
    }
}