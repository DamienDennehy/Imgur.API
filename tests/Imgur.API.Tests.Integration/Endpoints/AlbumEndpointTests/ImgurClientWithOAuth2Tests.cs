using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Endpoints.AlbumEndpointTests
{
    [TestClass]
    public class ImgurClientWithOAuth2Tests : TestBase
    {
        public async Task AddAlbumImagesAsync_WithAlbum_AreEqual(IAlbum actualAlbum)
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AlbumEndpoint(client);

            var updated =
                await
                    endpoint.AddAlbumImagesAsync(actualAlbum.Id,
                        new List<string> {"uH3kfZP", "VzbrLbO", "OkFyVOe", "Y8BbQuU"});

            Assert.IsTrue(updated);
        }

        [TestMethod]
        public async Task CreateAlbumAsync_IsNotNull()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AlbumEndpoint(client);

            var album = await endpoint.CreateAlbumAsync(
                "TheTitle", "TheDescription",
                AlbumPrivacy.Hidden, AlbumLayout.Grid,
                "uH3kfZP", new List<string> {"uH3kfZP", "VzbrLbO"});

            Assert.IsNotNull(album);
            Assert.IsNotNull(album.Id);
            Assert.IsNotNull(album.DeleteHash);

            await GetAlbumAsync_WithAlbum_AreEqual(album);
            await GetAlbumImageAsync_WithAlbum_AreEqual(album);
            await GetAlbumImagesAsync_WithAlbum_AreEqual(album);
            await UpdateAlbumAsync_WithAlbum_AreEqual(album);
            await AddAlbumImagesAsync_WithAlbum_AreEqual(album);
            await RemoveAlbumImagesAsync_WithAlbum_AreEqual(album);
            await SetAlbumImagesAsync_WithAlbum_AreEqual(album);
            await FavoriteAlbumAsync_WithImage_IsTrue(album);
            await FavoriteAlbumAsync_WithImage_IsFalse(album);
            await DeleteAlbumAsync_WithImage_IsTrue(album);
        }

        public async Task DeleteAlbumAsync_WithImage_IsTrue(IAlbum actualAlbum)
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AlbumEndpoint(client);

            var deleted = await endpoint.DeleteAlbumAsync(actualAlbum.Id);

            Assert.IsTrue(deleted);
        }

        public async Task FavoriteAlbumAsync_WithImage_IsFalse(IAlbum actualAlbum)
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AlbumEndpoint(client);

            var favorited = await endpoint.FavoriteAlbumAsync(actualAlbum.Id);

            Assert.IsFalse(favorited);
        }

        public async Task FavoriteAlbumAsync_WithImage_IsTrue(IAlbum actualAlbum)
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AlbumEndpoint(client);

            var favorited = await endpoint.FavoriteAlbumAsync(actualAlbum.Id);

            Assert.IsTrue(favorited);
        }

        public async Task GetAlbumAsync_WithAlbum_AreEqual(IAlbum actualAlbum)
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AlbumEndpoint(client);

            var album = await endpoint.GetAlbumAsync(actualAlbum.Id);

            Assert.AreEqual(actualAlbum.Id, album.Id);
        }

        public async Task GetAlbumImageAsync_WithAlbum_AreEqual(IAlbum actualAlbum)
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AlbumEndpoint(client);

            var image = await endpoint.GetAlbumImageAsync(actualAlbum.Id, "uH3kfZP");

            Assert.IsNotNull(image);
            Assert.AreEqual("uH3kfZP", image.Id);
        }

        public async Task GetAlbumImagesAsync_WithAlbum_AreEqual(IAlbum actualAlbum)
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AlbumEndpoint(client);

            var albums = await endpoint.GetAlbumImagesAsync(actualAlbum.Id);

            Assert.AreEqual(2, albums.Count());
        }

        public async Task RemoveAlbumImagesAsync_WithAlbum_AreEqual(IAlbum actualAlbum)
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AlbumEndpoint(client);

            var updated = await endpoint.RemoveAlbumImagesAsync(actualAlbum.Id, new List<string> {"uH3kfZP", "VzbrLbO"});

            Assert.IsTrue(updated);
        }

        public async Task SetAlbumImagesAsync_WithAlbum_AreEqual(IAlbum actualAlbum)
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AlbumEndpoint(client);

            var updated =
                await endpoint.SetAlbumImagesAsync(actualAlbum.Id, new List<string> {"uH3kfZP", "OkFyVOe", "Y8BbQuU"});

            Assert.IsTrue(updated);
        }

        public async Task UpdateAlbumAsync_WithAlbum_AreEqual(IAlbum actualAlbum)
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AlbumEndpoint(client);

            var updated = await endpoint.UpdateAlbumAsync(actualAlbum.Id, "TestTitle");

            Assert.IsTrue(updated);
        }
    }
}