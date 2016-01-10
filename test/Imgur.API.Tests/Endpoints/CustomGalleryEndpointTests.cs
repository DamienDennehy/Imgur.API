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
using Imgur.API.Tests.FakeResponses;
using Imgur.API.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class CustomGalleryEndpointTests : TestBase
    {
        [TestMethod]
        public async Task AddCustomGalleryTagsAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/g/custom/add_tags";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.AddCustomGalleryTagsAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var added =
                await endpoint.AddCustomGalleryTagsAsync(new List<string> {"Cats", "Dogs"}).ConfigureAwait(false);

            Assert.IsTrue(added);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task AddCustomGalleryTagsAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);
            await endpoint.AddCustomGalleryTagsAsync(new List<string> {"Cats", "Dogs"}).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task AddCustomGalleryTagsAsync_WithTagsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);
            await endpoint.AddCustomGalleryTagsAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task AddFilteredOutGalleryTagAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/g/block_tag";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.AddFilteredOutGalleryTagAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var added = await endpoint.AddFilteredOutGalleryTagAsync("Cat").ConfigureAwait(false);

            Assert.IsTrue(added);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task AddFilteredOutGalleryTagAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);
            await endpoint.AddFilteredOutGalleryTagAsync("Cat").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task AddFilteredOutGalleryTagAsync_WithTagsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);
            await endpoint.AddFilteredOutGalleryTagAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetCustomGalleryAsync_DefaultParameters_IsNotNull()
        {
            var fakeUrl = "https://api.imgur.com/3/g/custom/viral/week/";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetCustomGalleryAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var gallery = await endpoint.GetCustomGalleryAsync().ConfigureAwait(false);

            Assert.IsNotNull(gallery);
            Assert.AreEqual("imgurapidotnet", gallery.AccountUrl);
            Assert.AreEqual(60, gallery.ItemCount);
            Assert.AreEqual(60, gallery.Items.Count());
            Assert.AreEqual("http://imgur.com/custom", gallery.Link);
            Assert.AreEqual(2, gallery.Tags.Count());
        }

        [TestMethod]
        public async Task GetCustomGalleryAsync_TopMonth_IsNotNull()
        {
            var fakeUrl = "https://api.imgur.com/3/g/custom/top/month/";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetCustomGalleryAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var gallery =
                await endpoint.GetCustomGalleryAsync(CustomGallerySortOrder.Top, TimeWindow.Month).ConfigureAwait(false);

            Assert.IsNotNull(gallery);
            Assert.AreEqual("imgurapidotnet", gallery.AccountUrl);
            Assert.AreEqual(60, gallery.ItemCount);
            Assert.AreEqual(60, gallery.Items.Count());
            Assert.AreEqual("http://imgur.com/custom", gallery.Link);
            Assert.AreEqual(2, gallery.Tags.Count());
        }

        [TestMethod]
        public async Task GetCustomGalleryAsync_TopMonth1_IsNotNull()
        {
            var fakeUrl = "https://api.imgur.com/3/g/custom/top/month/1";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetCustomGalleryAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var gallery =
                await
                    endpoint.GetCustomGalleryAsync(CustomGallerySortOrder.Top, TimeWindow.Month, 1)
                        .ConfigureAwait(false);

            Assert.IsNotNull(gallery);
            Assert.AreEqual("imgurapidotnet", gallery.AccountUrl);
            Assert.AreEqual(60, gallery.ItemCount);
            Assert.AreEqual(60, gallery.Items.Count());
            Assert.AreEqual("http://imgur.com/custom", gallery.Link);
            Assert.AreEqual(2, gallery.Tags.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCustomGalleryAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var fakeUrl = "https://api.imgur.com/3/g/custom/top/month/1";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetCustomGalleryAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var gallery =
                await
                    endpoint.GetCustomGalleryAsync(CustomGallerySortOrder.Top, TimeWindow.Month, 1)
                        .ConfigureAwait(false);

            Assert.IsNotNull(gallery);
        }

        [TestMethod]
        public async Task GetCustomGalleryItemAsync_WithAlbum_AreEqual()
        {
            var fakeUrl = "https://api.imgur.com/3/g/custom/AbcDef";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetCustomGalleryAlbumAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var galleryItem = await endpoint.GetCustomGalleryItemAsync("AbcDef").ConfigureAwait(false);

            Assert.IsNotNull(galleryItem);
            Assert.IsTrue(galleryItem is IGalleryAlbum);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCustomGalleryItemAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var fakeUrl = "https://api.imgur.com/3/g/custom/AbcDef";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetCustomGalleryImageAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var galleryItem = await endpoint.GetCustomGalleryItemAsync(null).ConfigureAwait(false);

            Assert.IsNotNull(galleryItem);
        }

        [TestMethod]
        public async Task GetCustomGalleryItemAsync_WithImage_AreEqual()
        {
            var fakeUrl = "https://api.imgur.com/3/g/custom/AbcDef";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetCustomGalleryImageAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var galleryItem = await endpoint.GetCustomGalleryItemAsync("AbcDef").ConfigureAwait(false);

            Assert.IsNotNull(galleryItem);
            Assert.IsTrue(galleryItem is IGalleryImage);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCustomGalleryItemAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var fakeUrl = "https://api.imgur.com/3/g/custom/AbcDef";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetCustomGalleryImageAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var galleryItem = await endpoint.GetCustomGalleryItemAsync("AbcDef").ConfigureAwait(false);

            Assert.IsNotNull(galleryItem);
        }

        [TestMethod]
        public async Task GetFilteredOutGalleryAsync_DefaultParameters_IsNotNull()
        {
            var fakeUrl = "https://api.imgur.com/3/g/filtered/viral/week/";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetFilteredOutGalleryAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var gallery = await endpoint.GetFilteredOutGalleryAsync().ConfigureAwait(false);

            Assert.IsNotNull(gallery);
            Assert.AreEqual("imgurapidotnet", gallery.AccountUrl);
            Assert.AreEqual(60, gallery.ItemCount);
            Assert.AreEqual(60, gallery.Items.Count());
            Assert.AreEqual("https://imgur.com/filtered", gallery.Link);
            Assert.AreEqual(2, gallery.Tags.Count());
        }

        [TestMethod]
        public async Task GetFilteredOutGalleryAsync_TopMonth_IsNotNull()
        {
            var fakeUrl = "https://api.imgur.com/3/g/filtered/top/month/";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetFilteredOutGalleryAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var gallery =
                await
                    endpoint.GetFilteredOutGalleryAsync(CustomGallerySortOrder.Top, TimeWindow.Month)
                        .ConfigureAwait(false);

            Assert.IsNotNull(gallery);
            Assert.AreEqual("imgurapidotnet", gallery.AccountUrl);
            Assert.AreEqual(60, gallery.ItemCount);
            Assert.AreEqual(60, gallery.Items.Count());
            Assert.AreEqual("https://imgur.com/filtered", gallery.Link);
            Assert.AreEqual(2, gallery.Tags.Count());
        }

        [TestMethod]
        public async Task GetFilteredOutGalleryAsync_TopMonth1_IsNotNull()
        {
            var fakeUrl = "https://api.imgur.com/3/g/filtered/top/month/1";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetFilteredOutGalleryAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var gallery =
                await
                    endpoint.GetFilteredOutGalleryAsync(CustomGallerySortOrder.Top, TimeWindow.Month, 1)
                        .ConfigureAwait(false);

            Assert.IsNotNull(gallery);
            Assert.AreEqual("imgurapidotnet", gallery.AccountUrl);
            Assert.AreEqual(60, gallery.ItemCount);
            Assert.AreEqual(60, gallery.Items.Count());
            Assert.AreEqual("https://imgur.com/filtered", gallery.Link);
            Assert.AreEqual(2, gallery.Tags.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetFilteredOutGalleryAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var fakeUrl = "https://api.imgur.com/3/g/custom/top/month/1";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetFilteredOutGalleryAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var gallery =
                await
                    endpoint.GetFilteredOutGalleryAsync(CustomGallerySortOrder.Top, TimeWindow.Month, 1)
                        .ConfigureAwait(false);

            Assert.IsNotNull(gallery);
        }

        [TestMethod]
        public async Task RemoveCustomGalleryTagsAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/g/custom/remove_tags?tags=Cats%2CDogs";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.RemoveCustomGalleryTagsAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var added =
                await endpoint.RemoveCustomGalleryTagsAsync(new List<string> {"Cats", "Dogs"}).ConfigureAwait(false);

            Assert.IsTrue(added);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task RemoveCustomGalleryTagsAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);
            await endpoint.RemoveCustomGalleryTagsAsync(new List<string> {"Cats", "Dogs"}).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task RemoveCustomGalleryTagsAsync_WithTagsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);
            await endpoint.RemoveCustomGalleryTagsAsync(null).ConfigureAwait(false);
        }


        [TestMethod]
        public async Task RemoveFilteredOutGalleryTagAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/g/unblock_tag";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.RemoveFilteredOutGalleryTagAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CustomGalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var added = await endpoint.RemoveFilteredOutGalleryTagAsync("Cat").ConfigureAwait(false);

            Assert.IsTrue(added);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task RemoveFilteredOutGalleryTagAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);
            await endpoint.RemoveFilteredOutGalleryTagAsync("Cats").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task RemoveFilteredOutGalleryTagAsync_WithTagsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);
            await endpoint.RemoveFilteredOutGalleryTagAsync(null).ConfigureAwait(false);
        }
    }
}