using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.RequestBuilders;
using Xunit;

namespace Imgur.API.Tests.RequestBuilderTests
{
    public class CustomGalleryRequestBuilderTests
    {
        [Fact]
        public async Task AddCustomGalleryTagsRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CustomGalleryRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}g/custom/add_tags";
            var tags = new List<string> {"Cats", "Dogs", "Seals"};

            var request = CustomGalleryRequestBuilder.AddCustomGalleryTagsRequest(mockUrl, tags);
            var expected = "tags=Cats%2CDogs%2CSeals";

            Assert.NotNull(request);
            Assert.Equal(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("https://api.imgur.com/3/g/custom/add_tags", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Put, request.Method);
        }

        [Fact]
        public void AddCustomGalleryTagsRequest_WithTagsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CustomGalleryRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}g/custom/add_tags";

            var exception = Record.Exception(() => CustomGalleryRequestBuilder.AddCustomGalleryTagsRequest(mockUrl, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "tags");
        }

        [Fact]
        public void AddCustomGalleryTagsRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new CustomGalleryRequestBuilder();

            var exception = Record.Exception(() => CustomGalleryRequestBuilder.AddCustomGalleryTagsRequest(null, new List<string>()));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }

        [Fact]
        public async Task AddFilteredOutGalleryTagRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CustomGalleryRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}g/block_tag";
            var tag = "Cats";

            var request = CustomGalleryRequestBuilder.AddFilteredOutGalleryTagRequest(mockUrl, tag);
            var expected = "tag=Cats";

            Assert.NotNull(request);
            Assert.Equal(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("https://api.imgur.com/3/g/block_tag", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
        }

        [Fact]
        public void AddFilteredOutGalleryTagRequest_WithTagNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CustomGalleryRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}g/block_tag";

            var exception = Record.Exception(() => CustomGalleryRequestBuilder.AddFilteredOutGalleryTagRequest(mockUrl, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "tag");
        }

        [Fact]
        public void AddFilteredOutGalleryTagRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new CustomGalleryRequestBuilder();

            var exception = Record.Exception(() => CustomGalleryRequestBuilder.AddFilteredOutGalleryTagRequest(null, "test"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }

        [Fact]
        public void RemoveCustomGalleryTagsRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CustomGalleryRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}g/custom/remove_tags";
            var tags = new List<string> {"Cats", "Dogs", "Seals"};

            var request = CustomGalleryRequestBuilder.RemoveCustomGalleryTagsRequest(mockUrl, tags);
            var expected = "https://api.imgur.com/3/g/custom/remove_tags?tags=Cats%2CDogs%2CSeals";

            Assert.NotNull(request);
            Assert.Equal(expected, request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Delete, request.Method);
        }

        [Fact]
        public void RemoveCustomGalleryTagsRequest_WithTagsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CustomGalleryRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}g/custom/remove_tags";

            var exception = Record.Exception(() => CustomGalleryRequestBuilder.RemoveCustomGalleryTagsRequest(mockUrl, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "tags");
        }

        [Fact]
        public void RemoveCustomGalleryTagsRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new CustomGalleryRequestBuilder();

            var exception =
                Record.Exception(() => CustomGalleryRequestBuilder.RemoveCustomGalleryTagsRequest(null, new List<string>()));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }

        [Fact]
        public void RemoveFilteredOutGalleryTagRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CustomGalleryRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}g/unblock_tag";
            var tag = "Cats";

            var request = CustomGalleryRequestBuilder.RemoveFilteredOutGalleryTagRequest(mockUrl, tag);
            var expected = "https://api.imgur.com/3/g/unblock_tag";

            Assert.NotNull(request);
            Assert.Equal(expected, request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
        }

        [Fact]
        public void RemoveFilteredOutGalleryTagRequest_WithTagNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CustomGalleryRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}g/unblock_tag";

            var exception = Record.Exception(() => CustomGalleryRequestBuilder.RemoveFilteredOutGalleryTagRequest(mockUrl, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "tag");
        }

        [Fact]
        public void RemoveFilteredOutGalleryTagRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new CustomGalleryRequestBuilder();

            var exception = Record.Exception(() => CustomGalleryRequestBuilder.RemoveFilteredOutGalleryTagRequest(null, "test"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }
    }
}