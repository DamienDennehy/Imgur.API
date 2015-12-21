using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Tests.FakeResponses;
using Imgur.API.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class AlbumEndpointTests
    {
        [TestMethod]
        public async Task AddAlbumImagesAsync_IsTrue()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.AddAlbumImagesResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/album/12x5454/add"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var updated = await endpoint.AddAlbumImagesAsync("12x5454", new List<string> {"AbcDef", "IrcDef"});

            Assert.IsTrue(updated);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task AddAlbumImagesAsync_WithAlbumNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            await endpoint.AddAlbumImagesAsync(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task AddAlbumImagesAsync_WithIdNulls_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            await endpoint.AddAlbumImagesAsync("12x5454", null);
        }

        [TestMethod]
        public async Task CreateAlbumAsync_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.CreateAlbumResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/album"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var album = await endpoint.CreateAlbumAsync();

            Assert.IsNotNull(album);
            Assert.AreEqual("3gfxo", album.Id);
            Assert.AreEqual("iIFJnFpVbYOvzvv", album.DeleteHash);
        }

        [TestMethod]
        public async Task DeleteAlbumAsync_IsTrue()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.DeleteAlbumResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/album/12x5454"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var deleted = await endpoint.DeleteAlbumAsync("12x5454");

            Assert.IsTrue(deleted);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteAlbumAsync_WithAlbumNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            await endpoint.DeleteAlbumAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task FavoriteAlbumAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new AlbumEndpoint(client);
            await endpoint.FavoriteAlbumAsync(null);
        }

        [TestMethod]
        public async Task FavoriteAlbumAsync_WithImgurClient_IsFalse()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.FavoriteAlbumResponseFalse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/album/zVpyzhW/favorite"),
                fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new AlbumEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var favorited = await endpoint.FavoriteAlbumAsync("zVpyzhW");

            Assert.IsFalse(favorited);
        }

        [TestMethod]
        public async Task FavoriteAlbumAsync_WithImgurClient_IsTrue()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.FavoriteAlbumResponseTrue)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/album/zVpyzhW/favorite"),
                fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new AlbumEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var favorited = await endpoint.FavoriteAlbumAsync("zVpyzhW");

            Assert.IsTrue(favorited);
        }

        [TestMethod]
        public async Task FavoriteAlbumAsync_WithMashapeClient_IsFalse()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Mashape.FavoriteAlbumResponseFalse)
            };

            fakeHttpMessageHandler.AddFakeResponse(
                new Uri("https://imgur-apiv3.p.mashape.com/3/album/zVpyzhW/favorite"), fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new MashapeClient("123", "1234", "xyz", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new AlbumEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var favorited = await endpoint.FavoriteAlbumAsync("zVpyzhW");

            Assert.IsFalse(favorited);
        }

        [TestMethod]
        public async Task FavoriteAlbumAsync_WithMashapeClient_IsTrue()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Mashape.FavoriteAlbumResponseTrue)
            };

            fakeHttpMessageHandler.AddFakeResponse(
                new Uri("https://imgur-apiv3.p.mashape.com/3/album/zVpyzhW/favorite"), fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new MashapeClient("123", "1234", "xyz", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new AlbumEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var favorited = await endpoint.FavoriteAlbumAsync("zVpyzhW");

            Assert.IsTrue(favorited);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task FavoriteImageAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ImageEndpoint(client);
            await endpoint.FavoriteImageAsync("zVpyzhW");
        }

        [TestMethod]
        public async Task GetAlbumAsync_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.GetAlbumResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/album/5F5Cy"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var album = await endpoint.GetAlbumAsync("5F5Cy");

            Assert.IsNotNull(album);
            Assert.AreEqual(album.Id, "5F5Cy");
            Assert.AreEqual(album.Title, null);
            Assert.AreEqual(album.Description, null);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1446591779), album.DateTime);
            Assert.AreEqual(album.Cover, "79MH23L");
            Assert.AreEqual(album.CoverWidth, 609);
            Assert.AreEqual(album.CoverHeight, 738);
            Assert.AreEqual(album.AccountUrl, "sarah");
            Assert.AreEqual(album.AccountId, 9571);
            Assert.AreEqual(album.Privacy, AlbumPrivacy.Public);
            Assert.AreEqual(album.Layout, AlbumLayout.Blog);
            Assert.AreEqual(album.Views, 4);
            Assert.AreEqual(album.Link, "http://imgur.com/a/5F5Cy");
            Assert.AreEqual(album.Favorite, false);
            Assert.AreEqual(album.Nsfw, null);
            Assert.AreEqual(album.Section, null);
            Assert.AreEqual(album.ImagesCount, 3);
            Assert.AreEqual(album.Images.Count(), 3);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            await endpoint.GetAlbumAsync(null);
        }

        [TestMethod]
        public async Task GetAlbumImageAsync_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.GetAlbumImageResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/album/5F5Cy/image/79MH23L"),
                fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var image = await endpoint.GetAlbumImageAsync("5F5Cy", "79MH23L");

            Assert.IsNotNull(image);

            Assert.AreEqual(image.Id, "79MH23L");
            Assert.AreEqual(image.Title, null);
            Assert.AreEqual(image.Description, null);
            Assert.AreEqual(new DateTimeOffset(new DateTime(2015, 11, 3, 23, 03, 03, DateTimeKind.Utc)), image.DateTime);
            Assert.AreEqual(image.Type, "image/png");
            Assert.AreEqual(image.Animated, false);
            Assert.AreEqual(image.Width, 609);
            Assert.AreEqual(image.Height, 738);
            Assert.AreEqual(image.Size, 451530);
            Assert.AreEqual(image.Views, 2849);
            Assert.AreEqual(image.Bandwidth, 1286408970);
            Assert.AreEqual(image.Vote, null);
            Assert.AreEqual(image.Favorite, false);
            Assert.AreEqual(image.Nsfw, null);
            Assert.AreEqual(image.Section, null);
            Assert.AreEqual(image.AccountUrl, null);
            Assert.AreEqual(image.AccountId, null);
            Assert.AreEqual(image.Link, "http://i.imgur.com/79MH23L.png");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            await endpoint.GetAlbumImageAsync(null, "xyuOi");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumImageAsync_WithImageNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            await endpoint.GetAlbumImageAsync("PioAxs8", null);
        }

        [TestMethod]
        public async Task GetAlbumImagesAsync_ImageCountIsTrue()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.GetAlbumImagesResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/album/5F5Cy/images"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var images = await endpoint.GetAlbumImagesAsync("5F5Cy");

            Assert.IsTrue(images.Count() == 3);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumImagesAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            await endpoint.GetAlbumImagesAsync(null);
        }

        [TestMethod]
        public async Task RemoveAlbumImagesAsync_IsTrue()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.RemoveAlbumImagesResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(
                new Uri("https://api.imgur.com/3/album/12x5454/remove_images?ids=AbcDef%2CIrcDef"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var updated = await endpoint.RemoveAlbumImagesAsync("12x5454", new List<string> {"AbcDef", "IrcDef"});

            Assert.IsTrue(updated);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task RemoveAlbumImagesAsync_WithAlbumNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            await endpoint.RemoveAlbumImagesAsync(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task RemoveAlbumImagesAsync_WithIdNulls_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            await endpoint.RemoveAlbumImagesAsync("12x5454", null);
        }

        [TestMethod]
        public async Task SetAlbumImagesAsync_IsTrue()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.SetAlbumImagesResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/album/12x5454"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var updated = await endpoint.SetAlbumImagesAsync("12x5454", new List<string> {"AbcDef", "IrcDef"});

            Assert.IsTrue(updated);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task SetAlbumImagesAsync_WithAlbumNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            await endpoint.SetAlbumImagesAsync(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task SetAlbumImagesAsync_WithIdNulls_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            await endpoint.SetAlbumImagesAsync("12x5454", null);
        }

        [TestMethod]
        public async Task UpdateAlbumAsync_IsTrue()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.UpdateAlbumResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/album/12x5454"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var updated = await endpoint.UpdateAlbumAsync("12x5454");

            Assert.IsTrue(updated);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task UpdateAlbumAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            await endpoint.UpdateAlbumAsync(null);
        }
    }
}