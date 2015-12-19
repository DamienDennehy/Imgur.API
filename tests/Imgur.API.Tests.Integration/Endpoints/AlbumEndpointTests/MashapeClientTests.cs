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
    public class MashapeClientTests : TestBase
    {
        public async Task AddAlbumImagesAsync_WithAlbum_AreEqual(IAlbum actualAlbum)
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new AlbumEndpoint(client);

            var updated =
                await
                    endpoint.AddAlbumImagesAsync(actualAlbum.DeleteHash,
                        new List<string> {"uH3kfZP", "VzbrLbO", "OkFyVOe", "Y8BbQuU"});

            Assert.IsTrue(updated);
        }

        [TestMethod]
        public async Task CreateAlbumAsync_IsNotNull()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
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
            await DeleteImageAsync_WithImage_IsTrue(album);
        }

        public async Task DeleteImageAsync_WithImage_IsTrue(IAlbum actualAlbum)
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new AlbumEndpoint(client);

            var deleted = await endpoint.DeleteAlbumAsync(actualAlbum.DeleteHash);

            Assert.IsTrue(deleted);
        }

        public async Task GetAlbumAsync_WithAlbum_AreEqual(IAlbum actualAlbum)
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new AlbumEndpoint(client);

            var album = await endpoint.GetAlbumAsync(actualAlbum.Id);

            Assert.AreEqual(actualAlbum.Id, album.Id);
        }

        public async Task GetAlbumImageAsync_WithAlbum_AreEqual(IAlbum actualAlbum)
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new AlbumEndpoint(client);

            var image = await endpoint.GetAlbumImageAsync(actualAlbum.Id, "uH3kfZP");

            Assert.IsNotNull(image);
            Assert.AreEqual("uH3kfZP", image.Id);
        }

        public async Task GetAlbumImagesAsync_WithAlbum_AreEqual(IAlbum actualAlbum)
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new AlbumEndpoint(client);

            var albums = await endpoint.GetAlbumImagesAsync(actualAlbum.Id);

            Assert.AreEqual(2, albums.Count());
        }

        public async Task RemoveAlbumImagesAsync_WithAlbum_AreEqual(IAlbum actualAlbum)
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new AlbumEndpoint(client);

            var updated =
                await endpoint.RemoveAlbumImagesAsync(actualAlbum.DeleteHash, new List<string> {"uH3kfZP", "VzbrLbO"});

            Assert.IsTrue(updated);
        }

        public async Task SetAlbumImagesAsync_WithAlbum_AreEqual(IAlbum actualAlbum)
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new AlbumEndpoint(client);

            var updated =
                await
                    endpoint.SetAlbumImagesAsync(actualAlbum.DeleteHash,
                        new List<string> {"uH3kfZP", "OkFyVOe", "Y8BbQuU"});

            Assert.IsTrue(updated);
        }

        public async Task UpdateAlbumAsync_WithAlbum_AreEqual(IAlbum actualAlbum)
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new AlbumEndpoint(client);

            var updated = await endpoint.UpdateAlbumAsync(actualAlbum.DeleteHash, "TestTitle");

            Assert.IsTrue(updated);
        }
    }
}