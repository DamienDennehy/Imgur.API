using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.RequestBuilders;
using Xunit;

namespace Imgur.API.Tests.RequestBuilderTests
{
    public class ImageRequestBuilderTests
    {
        [Fact]
        public async Task UpdateImageRequest_Equal()
        {
            var apiClient = new ApiClient("123", "1234");

            var mockUrl = $"{apiClient.BaseAddress}image/1234Xyz9";
            var request = ImageRequestBuilder.UpdateImageRequest(mockUrl, "TheTitle", "TheDescription");

            Assert.NotNull(request);
            Assert.Equal("https://api.imgur.com/3/image/1234Xyz9", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);

            var expected = await request.Content.ReadAsStringAsync();

            Assert.Equal("title=TheTitle&description=TheDescription", expected);
        }

        [Fact]
        public void UpdateImageRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var exception = Record.Exception(() => ImageRequestBuilder.UpdateImageRequest(null, "1234Xyz9"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("url", argNullException.ParamName);
        }

        [Fact]
        public async Task UploadImageUrlRequest_Equal()
        {
            var apiClient = new ApiClient("123", "1234");
            var mockUrl = $"{apiClient.BaseAddress}image";

            var request = ImageRequestBuilder.UploadImageUrlRequest(mockUrl, "http://i.imgur.com/hxsPLa7.jpg",
                "TheAlbum", "TheName", "TheTitle", "TheDescription");

            Assert.NotNull(request);
            Assert.Equal("https://api.imgur.com/3/image", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);

            var expected = await request.Content.ReadAsStringAsync();

            Assert.Equal(
                "image=http%3A%2F%2Fi.imgur.com%2FhxsPLa7.jpg&type=URL&album=TheAlbum&name=TheName&title=TheTitle&description=TheDescription",
                expected);
        }

        [Fact]
        public void UploadImageUrlRequest_WithImageNull_ThrowsArgumentNullException()
        {
            var apiClient = new ApiClient("123", "1234");
            var mockUrl = $"{apiClient.BaseAddress}image";

            var exception = Record.Exception(() => ImageRequestBuilder.UploadImageUrlRequest(mockUrl, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("imageUrl", argNullException.ParamName);
        }

        [Fact]
        public void UploadImageUrlRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var exception = Record.Exception(() => ImageRequestBuilder.UploadImageUrlRequest(null, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("url", argNullException.ParamName);
        }

        [Fact]
        public async Task UploadImageStreamRequest_Equal()
        {
            var apiClient = new ApiClient("123", "1234");
            var mockUrl = $"{apiClient.BaseAddress}image";

            using var ms = new MemoryStream(new byte[9]);
            var imageLength = ms.Length;
            var request = ImageRequestBuilder.UploadImageStreamRequest(mockUrl, ms, "TheAlbum", "TheName", "TheTitle",
                "TheDescription");

            Assert.NotNull(request);
            Assert.Equal("https://api.imgur.com/3/image", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);

            var content = (MultipartFormDataContent)request.Content;
            var streamContent =
                (StreamContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "image");
            var type = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "type");
            var name = streamContent.Headers.ContentDisposition.FileName;
            var album = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "album");
            var title = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "title");
            var description =
                (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "description");

            Assert.NotNull(streamContent);
            Assert.NotNull(type);
            Assert.NotNull(name);
            Assert.NotNull(album);
            Assert.NotNull(title);
            Assert.NotNull(description);

            var image = await streamContent.ReadAsByteArrayAsync();

            Assert.Equal(imageLength, image.Length);
            Assert.Equal("file", await type.ReadAsStringAsync());
            Assert.Equal("TheName", name);
            Assert.Equal("TheAlbum", await album.ReadAsStringAsync());
            Assert.Equal("TheTitle", await title.ReadAsStringAsync());
            Assert.Equal("TheDescription", await description.ReadAsStringAsync());
        }

        [Fact]
        public async Task UploadImageProgressStreamRequest_Equal()
        {
            var apiClient = new ApiClient("123", "1234");
            var mockUrl = $"{apiClient.BaseAddress}image";

            using var ms = new MemoryStream(new byte[9]);
            var imageLength = ms.Length;
            var currentProgress = 0;
            int report(int progress) => currentProgress = progress;
            var progress = new Progress<int>(percent => report(percent));
            using var request = ImageRequestBuilder.UploadImageStreamRequest(mockUrl, ms, "TheAlbum", "TheName", "TheTitle",
                "TheDescription", progress, 9999);

            Assert.NotNull(request);
            Assert.Equal("https://api.imgur.com/3/image", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);

            var content = (MultipartFormDataContent)request.Content;
            var streamContent =
                (ProgressStreamContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "image");
            var type = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "type");
            var name = streamContent.Headers.ContentDisposition.FileName;
            var album = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "album");
            var title = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "title");
            var description =
                (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "description");

            Assert.NotNull(streamContent);
            Assert.Equal(9999, streamContent._bufferSize);
            Assert.NotNull(type);
            Assert.NotNull(name);
            Assert.NotNull(album);
            Assert.NotNull(title);
            Assert.NotNull(description);

            var image = await streamContent.ReadAsByteArrayAsync();

            Assert.Equal(imageLength, image.Length);
            Assert.Equal("file", await type.ReadAsStringAsync());
            Assert.Equal("TheName", name);
            Assert.Equal("TheAlbum", await album.ReadAsStringAsync());
            Assert.Equal("TheTitle", await title.ReadAsStringAsync());
            Assert.Equal("TheDescription", await description.ReadAsStringAsync());
        }

        [Fact]
        public void UploadImageStreamRequest_WithImageNull_ThrowsArgumentNullException()
        {
            var apiClient = new ApiClient("123", "1234");
            var mockUrl = $"{apiClient.BaseAddress}image";

            var exception = Record.Exception(() => ImageRequestBuilder.UploadImageStreamRequest(mockUrl, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("image", argNullException.ParamName);
        }

        [Fact]
        public void UploadImageStreamRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            using var ms = new MemoryStream(new byte[9]);
            var exception = Record.Exception(() => ImageRequestBuilder.UploadImageStreamRequest(null, ms));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("url", argNullException.ParamName);
        }

        [Fact]
        public async Task UploadVideoRequest_Equal()
        {
            var apiClient = new ApiClient("123", "1234");
            var mockUrl = $"{apiClient.BaseAddress}upload";

            using var ms = new MemoryStream(new byte[9]);
            var imageLength = ms.Length;
            var request = ImageRequestBuilder.UploadVideoStreamRequest(mockUrl, ms, "TheAlbum", "TheName", "TheTitle",
                "TheDescription");

            Assert.NotNull(request);
            Assert.Equal("https://api.imgur.com/3/upload", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);

            var content = (MultipartFormDataContent)request.Content;
            var streamContent =
                (StreamContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "video");
            var type = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "type");
            var name = streamContent.Headers.ContentDisposition.FileName;
            var album = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "album");
            var title = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "title");
            var description =
                (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "description");

            Assert.NotNull(streamContent);
            Assert.NotNull(type);
            Assert.NotNull(album);
            Assert.NotNull(title);
            Assert.NotNull(description);

            var image = await streamContent.ReadAsByteArrayAsync();

            Assert.Equal(imageLength, image.Length);
            Assert.Equal("file", await type.ReadAsStringAsync());
            Assert.Equal("TheName", name);
            Assert.Equal("TheAlbum", await album.ReadAsStringAsync());
            Assert.Equal("TheTitle", await title.ReadAsStringAsync());
            Assert.Equal("TheDescription", await description.ReadAsStringAsync());
        }

        [Fact]
        public async Task UploadVideoStreamProgressRequest_Equal()
        {
            var apiClient = new ApiClient("123", "1234");
            var mockUrl = $"{apiClient.BaseAddress}upload";

            using var ms = new MemoryStream(new byte[9]);
            var imageLength = ms.Length;
            var currentProgress = 0;
            int report(int progress) => currentProgress = progress;
            var progress = new Progress<int>(percent => report(percent));
            using var request = ImageRequestBuilder.UploadVideoStreamRequest(mockUrl, ms, "TheAlbum", "TheName", "TheTitle",
                "TheDescription", progress, 9999);

            Assert.NotNull(request);
            Assert.Equal("https://api.imgur.com/3/upload", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);

            var content = (MultipartFormDataContent)request.Content;
            var streamContent =
                (ProgressStreamContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "video");
            var type = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "type");
            var name = streamContent.Headers.ContentDisposition.FileName;
            var album = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "album");
            var title = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "title");
            var description =
                (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "description");

            Assert.NotNull(streamContent);
            Assert.Equal(9999, streamContent._bufferSize);
            Assert.NotNull(type);
            Assert.NotNull(name);
            Assert.NotNull(album);
            Assert.NotNull(title);
            Assert.NotNull(description);

            var image = await streamContent.ReadAsByteArrayAsync();

            Assert.Equal(imageLength, image.Length);
            Assert.Equal("file", await type.ReadAsStringAsync());
            Assert.Equal("TheName", name);
            Assert.Equal("TheAlbum", await album.ReadAsStringAsync());
            Assert.Equal("TheTitle", await title.ReadAsStringAsync());
            Assert.Equal("TheDescription", await description.ReadAsStringAsync());
        }

        [Fact]
        public void UploadVideoStreamRequest_WithVideoNull_ThrowsArgumentNullException()
        {
            var apiClient = new ApiClient("123", "1234");
            var mockUrl = $"{apiClient.BaseAddress}upload";

            var exception = Record.Exception(() => ImageRequestBuilder.UploadVideoStreamRequest(mockUrl, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("video", argNullException.ParamName);
        }

        [Fact]
        public void UploadVideoStreamRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            using var ms = new MemoryStream(new byte[9]);
            var exception = Record.Exception(() => ImageRequestBuilder.UploadVideoStreamRequest(null, ms));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("url", argNullException.ParamName);
        }
    }
}
