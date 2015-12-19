using System;
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
    public partial class AccountEndpointTests
    {
        [TestMethod]
        public async Task DeleteAlbumAsync_IsNotNull()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.DeleteAlbumResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/sarah/album/yMgB7"),
                fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var deleted = await endpoint.DeleteAlbumAsync("yMgB7", "sarah");

            Assert.IsTrue(deleted);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteAlbumAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.DeleteAlbumAsync("yMgB7", null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteAlbumAsync_WithNullId_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.DeleteAlbumAsync(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteAlbumAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.DeleteAlbumAsync("yMgB7", null);
        }

        [TestMethod]
        public async Task GetAlbumAsync_IsNotNull()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetAlbumResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/sarah/album/yMgB7"),
                fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var album = await endpoint.GetAlbumAsync("yMgB7", "sarah");

            Assert.IsNotNull(album);
            Assert.AreEqual(album.Id, "yMgB7");
            Assert.AreEqual(album.Title, "Day 2 at Camp Imgur");
            Assert.AreEqual(album.Description, null);
            Assert.AreEqual(album.DateTime, new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1439066984));
            Assert.AreEqual(album.Cover, "BOdd9Qd");
            Assert.AreEqual(album.CoverWidth, 5184);
            Assert.AreEqual(album.CoverHeight, 3456);
            Assert.AreEqual(album.AccountUrl, "sarah");
            Assert.AreEqual(album.AccountId, 9571);
            Assert.AreEqual(album.Privacy, AlbumPrivacy.Public);
            Assert.AreEqual(album.Layout, AlbumLayout.Blog);
            Assert.AreEqual(album.Views, 413310);
            Assert.AreEqual(album.Link, "http://imgur.com/a/yMgB7");
            Assert.AreEqual(album.Favorite, false);
            Assert.AreEqual(album.Nsfw, false);
            Assert.AreEqual(album.Section, "pics");
            Assert.AreEqual(album.ImagesCount, 6);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumAsync("yMgB7", null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumAsync_WithNullId_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumAsync(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumAsync("yMgB7", null);
        }

        [TestMethod]
        public async Task GetAlbumCountAsync_IsNotNull()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetAlbumCountResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/sarah/albums/count"),
                fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var count = await endpoint.GetAlbumCountAsync("sarah");

            Assert.AreEqual(count, 105);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumCountAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumCountAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumCountAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumCountAsync(null);
        }

        [TestMethod]
        public async Task GetAlbumIdsAsync_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetAlbumIdsResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/bob/albums/ids/2"),
                fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var albums = await endpoint.GetAlbumIdsAsync("bob", 2);

            Assert.AreEqual(50, albums.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumIdsAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumIdsAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumIdsAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumIdsAsync(null);
        }

        [TestMethod]
        public async Task GetAlbumsAsync_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetAlbumsResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/bob/albums/2"), fakeResponse);

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var albums = await endpoint.GetAlbumsAsync("bob", 2);

            Assert.AreEqual(50, albums.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumsAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumsAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumsAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumsAsync(null);
        }
    }
}