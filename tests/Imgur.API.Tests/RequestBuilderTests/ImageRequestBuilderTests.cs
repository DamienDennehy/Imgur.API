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

            var expected = await request.Content.ReadAsStringAsync().ConfigureAwait(false);

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

            var expected = await request.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal(
                "type=URL&image=http%3A%2F%2Fi.imgur.com%2FhxsPLa7.jpg&album=TheAlbum&name=TheName&title=TheTitle&description=TheDescription",
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
        public async Task UploadStreamRequest_Equal()
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
            var imageContent =
                (StreamContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "image");
            var album = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "album");
            var type = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "type");
            var name = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "name");
            var title = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "title");
            var description =
                (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "description");

            Assert.NotNull(imageContent);
            Assert.NotNull(type);
            Assert.NotNull(album);
            Assert.NotNull(name);
            Assert.NotNull(title);
            Assert.NotNull(description);

            var image = await imageContent.ReadAsByteArrayAsync().ConfigureAwait(false);

            Assert.Equal(imageLength, image.Length);
            Assert.Equal("file", await type.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("TheAlbum", await album.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("TheName", await name.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("TheTitle", await title.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("TheDescription", await description.ReadAsStringAsync().ConfigureAwait(false));
        }

        [Fact]
        public async Task UploadProgressStreamRequest_Equal()
        {
            var apiClient = new ApiClient("123", "1234");
            var mockUrl = $"{apiClient.BaseAddress}image";

            using var ms = new MemoryStream(new byte[9]);
            var imageLength = ms.Length;
            var currentProgress = 0;
            int report(int progress) => currentProgress = progress;
            var byteProgress = new Progress<int>(percent => report(percent));
            using var request = ImageRequestBuilder.UploadImageStreamRequest(mockUrl, ms, "TheAlbum", "TheName", "TheTitle",
                "TheDescription", byteProgress);

            Assert.NotNull(request);
            Assert.Equal("https://api.imgur.com/3/image", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);

            var content = (MultipartFormDataContent)request.Content;
            var imageContent =
                (ProgressStreamContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "image");
            var album = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "album");
            var type = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "type");
            var name = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "name");
            var title = (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "title");
            var description =
                (StringContent)content.FirstOrDefault(x => x.Headers.ContentDisposition.Name == "description");

            Assert.NotNull(imageContent);
            Assert.NotNull(type);
            Assert.NotNull(album);
            Assert.NotNull(name);
            Assert.NotNull(title);
            Assert.NotNull(description);

            var image = await imageContent.ReadAsByteArrayAsync().ConfigureAwait(false);

            Assert.Equal(imageLength, image.Length);
            Assert.Equal("file", await type.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("TheAlbum", await album.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("TheName", await name.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("TheTitle", await title.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("TheDescription", await description.ReadAsStringAsync().ConfigureAwait(false));
        }

        [Fact]
        public void UploadStreamRequest_WithImageNull_ThrowsArgumentNullException()
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
        public void UploadStreamRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            using var ms = new MemoryStream(new byte[9]);
            var exception = Record.Exception(() => ImageRequestBuilder.UploadImageStreamRequest(null, ms));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("url", argNullException.ParamName);
        }
    }
}
