using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Enums;
using Imgur.API.RequestBuilders;
using Xunit;

namespace Imgur.API.Tests.RequestBuilderTests
{
    public class GalleryRequestBuilderTests : TestBase
    {
        [Fact]
        public async Task PublishToGalleryRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new GalleryRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}gallery/XysioD";
            var request = GalleryRequestBuilder.PublishToGalleryRequest(mockUrl, "Hello World!", "Funny", true, true);

            Assert.NotNull(request);
            var expected = "title=Hello+World%21&topic=Funny&terms=true&mature=true";

            Assert.Equal(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("https://api.imgur.com/3/gallery/XysioD", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
        }

        [Fact]
        public void PublishToGalleryRequest_WithTitleNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new GalleryRequestBuilder();

            var exception = Record.Exception(() => GalleryRequestBuilder.PublishToGalleryRequest("url", null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "title");
        }

        [Fact]
        public async Task PublishToGalleryRequest_WithTitleOnly_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new GalleryRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}gallery/XysioD";
            var request = GalleryRequestBuilder.PublishToGalleryRequest(mockUrl, "Hello World!");

            Assert.NotNull(request);
            var expected = "title=Hello+World%21";

            Assert.Equal(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("https://api.imgur.com/3/gallery/XysioD", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
        }

        [Fact]
        public void PublishToGalleryRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new GalleryRequestBuilder();

            var exception = Record.Exception(() => GalleryRequestBuilder.PublishToGalleryRequest(null, "test"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }

        [Fact]
        public void SearchGalleryAdvancedRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new GalleryRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}gallery/search";
            var actual = GalleryRequestBuilder.SearchGalleryAdvancedRequest(mockUrl, "star wars", "luke han leia", "Obi-Wan",
                "Kirk", ImageFileType.Anigif, ImageSize.Lrg);

            var expected =
                "https://api.imgur.com/3/gallery/search?q_all=star+wars&q_any=luke+han+leia&q_exactly=Obi-Wan&q_not=Kirk&q_type=anigif&q_size_px=lrg";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SearchGalleryAdvancedRequest_WithDefaults_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new GalleryRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}gallery/search";
            var actual = GalleryRequestBuilder.SearchGalleryAdvancedRequest(mockUrl, "star wars");

            var expected =
                "https://api.imgur.com/3/gallery/search?q_all=star+wars";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SearchGalleryAdvancedRequest_WithQueryNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new GalleryRequestBuilder();

            var exception = Record.Exception(() => GalleryRequestBuilder.SearchGalleryAdvancedRequest("Xio"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            Assert.True(
                exception.Message.Contains("At least one search parameter must be provided (All | Any | Exactly | Not)"));
        }

        [Fact]
        public void SearchGalleryAdvancedRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new GalleryRequestBuilder();

            var exception = Record.Exception(() => GalleryRequestBuilder.SearchGalleryAdvancedRequest(null, "dfdfd"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }

        [Fact]
        public void SearchGalleryRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new GalleryRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}gallery/search";
            var actual = GalleryRequestBuilder.SearchGalleryRequest(mockUrl, "star wars");

            var expected = "https://api.imgur.com/3/gallery/search?q=star+wars";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SearchGalleryRequest_WithQueryNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new GalleryRequestBuilder();

            var exception = Record.Exception(() => GalleryRequestBuilder.SearchGalleryRequest("Xio", null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "query");
        }

        [Fact]
        public void SearchGalleryRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new GalleryRequestBuilder();

            var exception = Record.Exception(() => GalleryRequestBuilder.SearchGalleryRequest(null, "dfdfd"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }
    }
}