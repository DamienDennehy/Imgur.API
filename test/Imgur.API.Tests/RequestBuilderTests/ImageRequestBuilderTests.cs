using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.RequestBuilders;
using Xunit;
using System.Diagnostics;

namespace Imgur.API.Tests.RequestBuilderTests
{
    public class ImageRequestBuilderTests
    {
        [Fact]
        public async Task UpdateImageRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new ImageRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}image/1234Xyz9";
            var request = ImageRequestBuilder.UpdateImageRequest(mockUrl, "TheTitle", "TheDescription");

            Assert.NotNull(request);
            Assert.Equal("https://api.imgur.com/3/image/1234Xyz9", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);

            var expected = await request.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal("title=TheTitle&description=TheDescription", expected);
        }

        [Fact]
        public void UpdateImageRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new ImageRequestBuilder();

            var exception = Record.Exception(() => ImageRequestBuilder.UpdateImageRequest(null, "1234Xyz9"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal(argNullException.ParamName, "url");
        }

        [Fact]
        public async Task UploadImageBinaryRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new ImageRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}image";

            var image = new byte[9];
            var request = ImageRequestBuilder.UploadImageBinaryRequest(mockUrl, image, "TheAlbum", "TheTitle",
                "TheDescription");

            Assert.NotNull(request);
            Assert.Equal("https://api.imgur.com/3/image", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);

            var content = (MultipartFormDataContent)request.Content;
            var imageContent =
                (ByteArrayContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "image");
            var album = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "album");
            var type = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "type");
            var title = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "title");
            var description =
                (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "description");

            Assert.NotNull(imageContent);
            Assert.NotNull(type);
            Assert.NotNull(album);
            Assert.NotNull(title);
            Assert.NotNull(description);

            Assert.Equal(image.Length, imageContent.Headers.ContentLength);
            Assert.Equal("file", await type.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("TheAlbum", await album.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("TheTitle", await title.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("TheDescription", await description.ReadAsStringAsync().ConfigureAwait(false));
        }

        [Fact]
        public void UploadImageBinaryRequest_WithImageNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new ImageRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}image";

            var exception = Record.Exception(() => ImageRequestBuilder.UploadImageBinaryRequest(mockUrl, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal(argNullException.ParamName, "image");
        }

        [Fact]
        public void UploadImageBinaryRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new ImageRequestBuilder();
            var image = new byte[9];

            var exception = Record.Exception(() => ImageRequestBuilder.UploadImageBinaryRequest(null, image));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal(argNullException.ParamName, "url");
        }

        [Fact]
        public async Task UploadImageUrlRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new ImageRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}image";

            var request = ImageRequestBuilder.UploadImageUrlRequest(mockUrl, "http://i.imgur.com/hxsPLa7.jpg",
                "TheAlbum", "TheTitle", "TheDescription");

            Assert.NotNull(request);
            Assert.Equal("https://api.imgur.com/3/image", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);

            var expected = await request.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal(
                "type=URL&image=http%3A%2F%2Fi.imgur.com%2FhxsPLa7.jpg&album=TheAlbum&title=TheTitle&description=TheDescription",
                expected);
        }

        [Fact]
        public void UploadImageUrlRequest_WithImageNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new ImageRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}image";

            var exception = Record.Exception(() => ImageRequestBuilder.UploadImageBinaryRequest(mockUrl, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal(argNullException.ParamName, "image");
        }

        [Fact]
        public void UploadImageUrlRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new ImageRequestBuilder();

            var exception = Record.Exception(() => ImageRequestBuilder.UploadImageBinaryRequest(null, new byte[9]));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal(argNullException.ParamName, "url");
        }

        [Fact]
        public async Task UploadStreamBinaryRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new ImageRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}image";

            using (var ms = new MemoryStream(new byte[9]))
            {
                var imageLength = ms.Length;
                var request = ImageRequestBuilder.UploadImageStreamRequest(mockUrl, ms, "TheAlbum", "TheTitle",
                    "TheDescription");

                Assert.NotNull(request);
                Assert.Equal("https://api.imgur.com/3/image", request.RequestUri.ToString());
                Assert.Equal(HttpMethod.Post, request.Method);

                var content = (MultipartFormDataContent)request.Content;
                var imageContent =
                    (StreamContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "image");
                var album = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "album");
                var type = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "type");
                var title = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "title");
                var description =
                    (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "description");

                Assert.NotNull(imageContent);
                Assert.NotNull(type);
                Assert.NotNull(album);
                Assert.NotNull(title);
                Assert.NotNull(description);

                var image = await imageContent.ReadAsByteArrayAsync().ConfigureAwait(false);

                Assert.Equal(imageLength, image.Length);
                Assert.Equal("file", await type.ReadAsStringAsync().ConfigureAwait(false));
                Assert.Equal("TheAlbum", await album.ReadAsStringAsync().ConfigureAwait(false));
                Assert.Equal("TheTitle", await title.ReadAsStringAsync().ConfigureAwait(false));
                Assert.Equal("TheDescription", await description.ReadAsStringAsync().ConfigureAwait(false));
            }
        }

        [Fact]
        public void UploadStreamBinaryRequest_WithImageNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new ImageRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}image";

            var exception = Record.Exception(() => ImageRequestBuilder.UploadImageStreamRequest(mockUrl, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal(argNullException.ParamName, "image");
        }

        [Fact]
        public void UploadStreamBinaryRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new ImageRequestBuilder();
            using (var ms = new MemoryStream(new byte[9]))
            {
                var exception = Record.Exception(() => ImageRequestBuilder.UploadImageStreamRequest(null, ms));
                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);

                var argNullException = (ArgumentNullException)exception;
                Assert.Equal(argNullException.ParamName, "url");
            }
        }
    }
}