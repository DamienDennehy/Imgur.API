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

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class CustomGalleryEndpointTests
    {
        [TestMethod]
        public async Task AddCustomGalleryTagsAsync_IsTrue()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.AddCustomGalleryTagsResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/g/custom/add_tags"), fakeResponse);

            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new CustomGalleryEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var added = await endpoint.AddCustomGalleryTagsAsync(new List<string> {"Cats", "Dogs"});

            Assert.IsTrue(added);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task AddCustomGalleryTagsAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);
            await endpoint.AddCustomGalleryTagsAsync(new List<string> {"Cats", "Dogs"});
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task AddCustomGalleryTagsAsync_WithTagsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);
            await endpoint.AddCustomGalleryTagsAsync(null);
        }

        [TestMethod]
        public async Task AddFilteredOutGalleryTagAsync_IsTrue()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.AddFilteredOutGalleryTagResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/g/custom/block_tag"), fakeResponse);

            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new CustomGalleryEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var added = await endpoint.AddFilteredOutGalleryTagAsync("Cat");

            Assert.IsTrue(added);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task AddFilteredOutGalleryTagAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);
            await endpoint.AddFilteredOutGalleryTagAsync("Cat");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task AddFilteredOutGalleryTagAsync_WithTagsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);
            await endpoint.AddFilteredOutGalleryTagAsync(null);
        }

        [TestMethod]
        public async Task GetCustomGalleryAsync_DefaultParameters_IsNotNull()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetCustomGalleryResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/g/custom/viral/week/"), fakeResponse);

            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new CustomGalleryEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var gallery = await endpoint.GetCustomGalleryAsync();

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
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetCustomGalleryResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/g/custom/top/month/"), fakeResponse);

            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new CustomGalleryEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var gallery = await endpoint.GetCustomGalleryAsync(CustomGallerySortOrder.Top, Window.Month);

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
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetCustomGalleryResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/g/custom/top/month/1"), fakeResponse);

            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new CustomGalleryEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var gallery = await endpoint.GetCustomGalleryAsync(CustomGallerySortOrder.Top, Window.Month, 1);

            Assert.IsNotNull(gallery);
            Assert.AreEqual("imgurapidotnet", gallery.AccountUrl);
            Assert.AreEqual(60, gallery.ItemCount);
            Assert.AreEqual(60, gallery.Items.Count());
            Assert.AreEqual("http://imgur.com/custom", gallery.Link);
            Assert.AreEqual(2, gallery.Tags.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetCustomGalleryAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetCustomGalleryResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/g/custom/top/month/1"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var gallery = await endpoint.GetCustomGalleryAsync(CustomGallerySortOrder.Top, Window.Month, 1);

            Assert.IsNotNull(gallery);
        }

        [TestMethod]
        public async Task GetFilteredOutGalleryAsync_DefaultParameters_IsNotNull()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetFilteredOutGalleryResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/g/filtered/viral/week/"), fakeResponse);

            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new CustomGalleryEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var gallery = await endpoint.GetFilteredOutGalleryAsync();

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
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetFilteredOutGalleryResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/g/filtered/top/month/"), fakeResponse);

            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new CustomGalleryEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var gallery = await endpoint.GetFilteredOutGalleryAsync(CustomGallerySortOrder.Top, Window.Month);

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
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetFilteredOutGalleryResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/g/filtered/top/month/1"), fakeResponse);

            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new CustomGalleryEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var gallery = await endpoint.GetFilteredOutGalleryAsync(CustomGallerySortOrder.Top, Window.Month, 1);

            Assert.IsNotNull(gallery);
            Assert.AreEqual("imgurapidotnet", gallery.AccountUrl);
            Assert.AreEqual(60, gallery.ItemCount);
            Assert.AreEqual(60, gallery.Items.Count());
            Assert.AreEqual("https://imgur.com/filtered", gallery.Link);
            Assert.AreEqual(2, gallery.Tags.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetFilteredOutGalleryAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetFilteredOutGalleryResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/g/custom/top/month/1"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var gallery = await endpoint.GetFilteredOutGalleryAsync(CustomGallerySortOrder.Top, Window.Month, 1);

            Assert.IsNotNull(gallery);
        }

        [TestMethod]
        public async Task GetCustomGalleryItemAsync_WithImage_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetCustomGalleryImageResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/g/custom/AbcDef"), fakeResponse);

            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new CustomGalleryEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var galleryItem = await endpoint.GetCustomGalleryItemAsync("AbcDef");

            Assert.IsNotNull(galleryItem);
            Assert.IsTrue(galleryItem is IGalleryImage);
        }

        [TestMethod]
        public async Task GetCustomGalleryItemAsync_WithAlbum_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetCustomGalleryAlbumResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/g/custom/AbcDef"), fakeResponse);

            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new CustomGalleryEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var galleryItem = await endpoint.GetCustomGalleryItemAsync("AbcDef");

            Assert.IsNotNull(galleryItem);
            Assert.IsTrue(galleryItem is IGalleryAlbum);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetCustomGalleryItemAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetCustomGalleryImageResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/g/custom/AbcDef"), fakeResponse);

            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new CustomGalleryEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var galleryItem = await endpoint.GetCustomGalleryItemAsync(null);

            Assert.IsNotNull(galleryItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetCustomGalleryItemAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.GetCustomGalleryImageResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/g/custom/AbcDef"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var galleryItem = await endpoint.GetCustomGalleryItemAsync("AbcDef");

            Assert.IsNotNull(galleryItem);
        }

        [TestMethod]
        public async Task RemoveCustomGalleryTagsAsync_IsTrue()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.RemoveCustomGalleryTagsResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/g/custom/remove_tags"), fakeResponse);

            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new CustomGalleryEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var added = await endpoint.RemoveCustomGalleryTagsAsync(new List<string> {"Cats", "Dogs"});

            Assert.IsTrue(added);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task RemoveCustomGalleryTagsAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);
            await endpoint.RemoveCustomGalleryTagsAsync(new List<string> {"Cats", "Dogs"});
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task RemoveCustomGalleryTagsAsync_WithTagsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);
            await endpoint.RemoveCustomGalleryTagsAsync(null);
        }


        [TestMethod]
        public async Task RemoveFilteredOutGalleryTagAsync_IsTrue()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CustomGalleryEndpointResponses.RemoveFilteredOutGalleryTagResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/g/custom/unblock_tag"), fakeResponse);

            var client = new ImgurClient("123", "1234", new FakeOAuth2TokenHandler().GetOAuth2TokenCodeResponse());
            var endpoint = new CustomGalleryEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var added = await endpoint.RemoveFilteredOutGalleryTagAsync("Cat");

            Assert.IsTrue(added);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task RemoveFilteredOutGalleryTagAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);
            await endpoint.RemoveFilteredOutGalleryTagAsync("Cats");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task RemoveFilteredOutGalleryTagAsync_WithTagsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CustomGalleryEndpoint(client);
            await endpoint.RemoveFilteredOutGalleryTagAsync(null);
        }
    }
}