using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.EndpointTests
{
    public partial class AccountEndpointTests
    {
        [TestMethod]
        public async Task DeleteAlbumAsync_IsNotNull()
        {
            var mockUrl = "https://api.imgur.com/3/account/sarah/album/yMgB7";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.DeleteAlbum)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var deleted = await endpoint.DeleteAlbumAsync("yMgB7", "sarah").ConfigureAwait(false);

            Assert.IsTrue(deleted);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteAlbumAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteAlbumAsync("yMgB7", null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteAlbumAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteAlbumAsync(null, null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteAlbumAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteAlbumAsync("yMgB7", "sarah").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteAlbumAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteAlbumAsync("yMgB7", null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetAlbumAsync_IsNotNull()
        {
            var mockUrl = "https://api.imgur.com/3/account/sarah/album/yMgB7";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetAlbum)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var album = await endpoint.GetAlbumAsync("yMgB7", "sarah").ConfigureAwait(false);

            Assert.IsNotNull(album);
            Assert.AreEqual("yMgB7", album.Id);
            Assert.AreEqual("Day 2 at Camp Imgur", album.Title);
            Assert.AreEqual(null, album.Description);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1439066984), album.DateTime);
            Assert.AreEqual("BOdd9Qd", album.Cover);
            Assert.AreEqual(5184, album.CoverWidth);
            Assert.AreEqual(3456, album.CoverHeight);
            Assert.AreEqual("sarah", album.AccountUrl);
            Assert.AreEqual(9571, album.AccountId);
            Assert.AreEqual(AlbumPrivacy.Public, album.Privacy);
            Assert.AreEqual(AlbumLayout.Blog, album.Layout);
            Assert.AreEqual(413310, album.Views);
            Assert.AreEqual("http://imgur.com/a/yMgB7", album.Link);
            Assert.AreEqual(false, album.Favorite);
            Assert.AreEqual(false, album.Nsfw);
            Assert.AreEqual("pics", album.Section);
            Assert.AreEqual(6, album.ImagesCount);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAlbumAsync("yMgB7", null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAlbumAsync(null, null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAlbumAsync("yMgB7", null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetAlbumCountAsync_IsNotNull()
        {
            var mockUrl = "https://api.imgur.com/3/account/sarah/albums/count";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetAlbumCount)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var count = await endpoint.GetAlbumCountAsync("sarah").ConfigureAwait(false);

            Assert.AreEqual(count, 105);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumCountAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAlbumCountAsync().ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumCountAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAlbumCountAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetAlbumIdsAsync_AreEqual()
        {
            var mockUrl = "https://api.imgur.com/3/account/bob/albums/ids/2";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetAlbumIds)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var albums = await endpoint.GetAlbumIdsAsync("bob", 2).ConfigureAwait(false);

            Assert.AreEqual(50, albums.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumIdsAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAlbumIdsAsync().ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumIdsAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAlbumIdsAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetAlbumsAsync_AreEqual()
        {
            var mockUrl = "https://api.imgur.com/3/account/bob/albums/2";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetAlbums)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var albums = await endpoint.GetAlbumsAsync("bob", 2).ConfigureAwait(false);

            Assert.AreEqual(50, albums.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumsAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAlbumsAsync().ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumsAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAlbumsAsync(null).ConfigureAwait(false);
        }
    }
}