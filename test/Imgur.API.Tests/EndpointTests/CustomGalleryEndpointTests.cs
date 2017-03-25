using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Models;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.EndpointTests
{
    public class CustomGalleryEndpointTests : TestBase
    {
        [Fact]
        public async Task AddCustomGalleryTagsAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/g/custom/add_tags";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCustomGalleryEndpointResponses.AddCustomGalleryTags)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var added =
                await endpoint.AddCustomGalleryTagsAsync(new List<string> {"Cats", "Dogs"}).ConfigureAwait(false);

            Assert.True(added);
        }

        [Fact]
        public async Task AddCustomGalleryTagsAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () =>
                            await
                                endpoint.AddCustomGalleryTagsAsync(new List<string> {"Cats", "Dogs"})
                                    .ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task AddCustomGalleryTagsAsync_WithTagsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.AddCustomGalleryTagsAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task AddFilteredOutGalleryTagAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/g/block_tag";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCustomGalleryEndpointResponses.AddFilteredOutGalleryTag)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var added = await endpoint.AddFilteredOutGalleryTagAsync("Cat").ConfigureAwait(false);

            Assert.True(added);
        }

        [Fact]
        public async Task AddFilteredOutGalleryTagAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.AddFilteredOutGalleryTagAsync("Cat").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task AddFilteredOutGalleryTagAsync_WithTagsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.AddFilteredOutGalleryTagAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetCustomGalleryAsync_DefaultParameters_NotNull()
        {
            var mockUrl = "https://api.imgur.com/3/g/custom/viral/week/";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCustomGalleryEndpointResponses.GetCustomGallery)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var gallery = await endpoint.GetCustomGalleryAsync().ConfigureAwait(false);

            Assert.NotNull(gallery);
            Assert.Equal("imgurapidotnet", gallery.AccountUrl);
            Assert.Equal(60, gallery.ItemCount);
            Assert.Equal(60, gallery.Items.Count());
            Assert.Equal("http://imgur.com/custom", gallery.Link);
            Assert.Equal(2, gallery.Tags.Count());
        }

        [Fact]
        public async Task GetCustomGalleryAsync_TopMonth_NotNull()
        {
            var mockUrl = "https://api.imgur.com/3/g/custom/top/month/";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCustomGalleryEndpointResponses.GetCustomGallery)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var gallery =
                await endpoint.GetCustomGalleryAsync(CustomGallerySortOrder.Top, TimeWindow.Month).ConfigureAwait(false);

            Assert.NotNull(gallery);
            Assert.Equal("imgurapidotnet", gallery.AccountUrl);
            Assert.Equal(60, gallery.ItemCount);
            Assert.Equal(60, gallery.Items.Count());
            Assert.Equal("http://imgur.com/custom", gallery.Link);
            Assert.Equal(2, gallery.Tags.Count());
        }

        [Fact]
        public async Task GetCustomGalleryAsync_TopMonth1_NotNull()
        {
            var mockUrl = "https://api.imgur.com/3/g/custom/top/month/1";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCustomGalleryEndpointResponses.GetCustomGallery)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var gallery =
                await
                    endpoint.GetCustomGalleryAsync(CustomGallerySortOrder.Top, TimeWindow.Month, 1)
                        .ConfigureAwait(false);

            Assert.NotNull(gallery);
            Assert.Equal("imgurapidotnet", gallery.AccountUrl);
            Assert.Equal(60, gallery.ItemCount);
            Assert.Equal(60, gallery.Items.Count());
            Assert.Equal("http://imgur.com/custom", gallery.Link);
            Assert.Equal(2, gallery.Tags.Count());
        }

        [Fact]
        public async Task GetCustomGalleryAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var mockUrl = "https://api.imgur.com/3/g/custom/top/month/1";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCustomGalleryEndpointResponses.GetCustomGallery)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));

            var exception =
                await
                    Record.ExceptionAsync(
                        async () =>
                            await
                                endpoint.GetCustomGalleryAsync(CustomGallerySortOrder.Top, TimeWindow.Month, 1)
                                    .ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetCustomGalleryItemAsync_WithAlbum_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/g/custom/AbcDef";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCustomGalleryEndpointResponses.GetCustomGalleryAlbum)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var galleryItem = await endpoint.GetCustomGalleryItemAsync("AbcDef").ConfigureAwait(false);

            Assert.NotNull(galleryItem);
            Assert.True(galleryItem is IGalleryAlbum);
        }

        [Fact]
        public async Task GetCustomGalleryItemAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var mockUrl = "https://api.imgur.com/3/g/custom/AbcDef";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCustomGalleryEndpointResponses.GetCustomGalleryImage)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetCustomGalleryItemAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetCustomGalleryItemAsync_WithImage_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/g/custom/AbcDef";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCustomGalleryEndpointResponses.GetCustomGalleryImage)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var galleryItem = await endpoint.GetCustomGalleryItemAsync("AbcDef").ConfigureAwait(false);

            Assert.NotNull(galleryItem);
            Assert.True(galleryItem is IGalleryImage);
        }

        [Fact]
        public async Task GetCustomGalleryItemAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var mockUrl = "https://api.imgur.com/3/g/custom/AbcDef";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCustomGalleryEndpointResponses.GetCustomGalleryImage)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetCustomGalleryItemAsync("AbcDef").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetFilteredOutGalleryAsync_DefaultParameters_NotNull()
        {
            var mockUrl = "https://api.imgur.com/3/g/filtered/viral/week/";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCustomGalleryEndpointResponses.GetFilteredOutGallery)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var gallery = await endpoint.GetFilteredOutGalleryAsync().ConfigureAwait(false);

            Assert.NotNull(gallery);
            Assert.Equal("imgurapidotnet", gallery.AccountUrl);
            Assert.Equal(60, gallery.ItemCount);
            Assert.Equal(60, gallery.Items.Count());
            Assert.Equal("https://imgur.com/filtered", gallery.Link);
            Assert.Equal(2, gallery.Tags.Count());
        }

        [Fact]
        public async Task GetFilteredOutGalleryAsync_TopMonth_NotNull()
        {
            var mockUrl = "https://api.imgur.com/3/g/filtered/top/month/";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCustomGalleryEndpointResponses.GetFilteredOutGallery)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var gallery =
                await
                    endpoint.GetFilteredOutGalleryAsync(CustomGallerySortOrder.Top, TimeWindow.Month)
                        .ConfigureAwait(false);

            Assert.NotNull(gallery);
            Assert.Equal("imgurapidotnet", gallery.AccountUrl);
            Assert.Equal(60, gallery.ItemCount);
            Assert.Equal(60, gallery.Items.Count());
            Assert.Equal("https://imgur.com/filtered", gallery.Link);
            Assert.Equal(2, gallery.Tags.Count());
        }

        [Fact]
        public async Task GetFilteredOutGalleryAsync_TopMonth1_NotNull()
        {
            var mockUrl = "https://api.imgur.com/3/g/filtered/top/month/1";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCustomGalleryEndpointResponses.GetFilteredOutGallery)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var gallery =
                await
                    endpoint.GetFilteredOutGalleryAsync(CustomGallerySortOrder.Top, TimeWindow.Month, 1)
                        .ConfigureAwait(false);

            Assert.NotNull(gallery);
            Assert.Equal("imgurapidotnet", gallery.AccountUrl);
            Assert.Equal(60, gallery.ItemCount);
            Assert.Equal(60, gallery.Items.Count());
            Assert.Equal("https://imgur.com/filtered", gallery.Link);
            Assert.Equal(2, gallery.Tags.Count());
        }

        [Fact]
        public async Task GetFilteredOutGalleryAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var mockUrl = "https://api.imgur.com/3/g/custom/top/month/1";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCustomGalleryEndpointResponses.GetFilteredOutGallery)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));

            var exception =
                await
                    Record.ExceptionAsync(
                        async () =>
                            await endpoint.GetFilteredOutGalleryAsync(CustomGallerySortOrder.Top, TimeWindow.Month, 1)
                                .ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task RemoveCustomGalleryTagsAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/g/custom/remove_tags?tags=Cats%2CDogs";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCustomGalleryEndpointResponses.RemoveCustomGalleryTags)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var added =
                await endpoint.RemoveCustomGalleryTagsAsync(new List<string> {"Cats", "Dogs"}).ConfigureAwait(false);

            Assert.True(added);
        }

        [Fact]
        public async Task RemoveCustomGalleryTagsAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);

            var exception = await Record.ExceptionAsync(
                async () =>
                    await endpoint.RemoveCustomGalleryTagsAsync(new List<string> {"Cats", "Dogs"}).ConfigureAwait(false))
                .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task RemoveCustomGalleryTagsAsync_WithTagsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.RemoveCustomGalleryTagsAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }


        [Fact]
        public async Task RemoveFilteredOutGalleryTagAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/g/unblock_tag";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCustomGalleryEndpointResponses.RemoveFilteredOutGalleryTag)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var added = await endpoint.RemoveFilteredOutGalleryTagAsync("Cat").ConfigureAwait(false);

            Assert.True(added);
        }

        [Fact]
        public async Task RemoveFilteredOutGalleryTagAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.RemoveFilteredOutGalleryTagAsync("Cats").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task RemoveFilteredOutGalleryTagAsync_WithTagsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.RemoveFilteredOutGalleryTagAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}