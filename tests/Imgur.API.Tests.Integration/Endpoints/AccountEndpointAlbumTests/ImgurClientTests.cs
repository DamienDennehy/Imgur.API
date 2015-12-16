﻿using System.Linq;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Endpoints.AccountEndpointAlbumTests
{
    [TestClass]
    public class ImgurClientTests : TestBase
    {
        [TestMethod]
        public async Task GetAlbumAsync_IsNotNull()
        {
            var client = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new AccountEndpoint(client);

            var album = await endpoint.GetAlbumAsync("SbU9Y", "sarah");

            Assert.IsNotNull(album);
        }

        [TestMethod]
        public async Task GetAlbumCountAsync_GreaterThan100()
        {
            var client = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new AccountEndpoint(client);

            var albums = await endpoint.GetAlbumCountAsync("sarah");

            Assert.IsTrue(albums > 100);
        }

        [TestMethod]
        public async Task GetAlbumIdsAsync_AreEqual()
        {
            var client = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new AccountEndpoint(client);

            var albums = await endpoint.GetAlbumIdsAsync("sarah");

            Assert.AreEqual(50, albums.Count());
        }

        [TestMethod]
        public async Task GetAlbumsAsync_AreEqual()
        {
            var client = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new AccountEndpoint(client);

            var albums = await endpoint.GetAlbumsAsync("sarah");

            Assert.AreEqual(50, albums.Count());
        }
    }
}