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
    public class AlbumEndpointTests : TestBase
    {
        [TestMethod]
        public async Task AddAlbumImagesAsync_IsTrue()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.AddAlbumImagesResponse)
            };

            var fakeUrl = "https://api.imgur.com/3/album/12x5454/add";
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));

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
        public async Task AddAlbumImagesAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            await endpoint.AddAlbumImagesAsync("12x5454", null);
        }

        [TestMethod]
        public async Task CreateAlbumAsync_AreEqual()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.CreateAlbumResponse)
            };

            var fakeUrl = "https://api.imgur.com/3/album";
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var album = await endpoint.CreateAlbumAsync();

            Assert.IsNotNull(album);
            Assert.AreEqual("3gfxo", album.Id);
            Assert.AreEqual("iIFJnFpVbYOvzvv", album.DeleteHash);
        }

        [TestMethod]
        public async Task DeleteAlbumAsync_IsTrue()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.DeleteAlbumResponse)
            };

            var fakeUrl = "https://api.imgur.com/3/album/12x5454";
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
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
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new AlbumEndpoint(client);
            await endpoint.FavoriteAlbumAsync(null);
        }

        [TestMethod]
        public async Task FavoriteAlbumAsync_WithImgurClient_IsFalse()
        {
            var fakeUrl = "https://api.imgur.com/3/album/zVpyzhW/favorite";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.FavoriteAlbumResponseFalse)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new AlbumEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var favorited = await endpoint.FavoriteAlbumAsync("zVpyzhW");

            Assert.IsFalse(favorited);
        }

        [TestMethod]
        public async Task FavoriteAlbumAsync_WithImgurClient_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/album/zVpyzhW/favorite";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.FavoriteAlbumResponseTrue)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new AlbumEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var favorited = await endpoint.FavoriteAlbumAsync("zVpyzhW");

            Assert.IsTrue(favorited);
        }

        [TestMethod]
        public async Task FavoriteAlbumAsync_WithMashapeClient_IsFalse()
        {
            var fakeUrl = "https://imgur-apiv3.p.mashape.com/3/album/zVpyzhW/favorite";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Mashape.FavoriteAlbumResponseFalse)
            };

            var client = new MashapeClient("123", "1234", "xyz", FakeOAuth2Token);
            var endpoint = new AlbumEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var favorited = await endpoint.FavoriteAlbumAsync("zVpyzhW");

            Assert.IsFalse(favorited);
        }

        [TestMethod]
        public async Task FavoriteAlbumAsync_WithMashapeClient_IsTrue()
        {
            var fakeUrl = "https://imgur-apiv3.p.mashape.com/3/album/zVpyzhW/favorite";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Mashape.FavoriteAlbumResponseTrue)
            };

            var client = new MashapeClient("123", "1234", "xyz", FakeOAuth2Token);
            var endpoint = new AlbumEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
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
            var fakeUrl = "https://api.imgur.com/3/album/5F5Cy";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.GetAlbumResponse)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var album = await endpoint.GetAlbumAsync("5F5Cy");

            Assert.IsNotNull(album);
            Assert.AreEqual("5F5Cy", album.Id);
            Assert.AreEqual(null, album.Title);
            Assert.AreEqual(null, album.Description);
            Assert.AreEqual(album.DateTime, new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1446591779));
            Assert.AreEqual("79MH23L", album.Cover);
            Assert.AreEqual(609, album.CoverWidth);
            Assert.AreEqual(738, album.CoverHeight);
            Assert.AreEqual("sarah", album.AccountUrl);
            Assert.AreEqual(9571, album.AccountId);
            Assert.AreEqual(AlbumPrivacy.Public, album.Privacy);
            Assert.AreEqual(AlbumLayout.Blog, album.Layout);
            Assert.AreEqual(4, album.Views);
            Assert.AreEqual("http://imgur.com/a/5F5Cy", album.Link);
            Assert.AreEqual(false, album.Favorite);
            Assert.AreEqual(null, album.Nsfw);
            Assert.AreEqual(null, album.Section);
            Assert.AreEqual(3, album.ImagesCount);
            Assert.AreEqual(3, album.Images.Count());
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
            var fakeUrl = "https://api.imgur.com/3/album/5F5Cy/image/79MH23L";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.GetAlbumImageResponse)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var image = await endpoint.GetAlbumImageAsync("5F5Cy", "79MH23L");

            Assert.IsNotNull(image);

            Assert.AreEqual("79MH23L", image.Id);
            Assert.AreEqual(null, image.Title);
            Assert.AreEqual(null, image.Description);
            Assert.AreEqual(image.DateTime, new DateTimeOffset(new DateTime(2015, 11, 3, 23, 03, 03, DateTimeKind.Utc)));
            Assert.AreEqual("image/png", image.Type);
            Assert.AreEqual(false, image.Animated);
            Assert.AreEqual(609, image.Width);
            Assert.AreEqual(738, image.Height);
            Assert.AreEqual(451530, image.Size);
            Assert.AreEqual(2849, image.Views);
            Assert.AreEqual(1286408970, image.Bandwidth);
            Assert.AreEqual(null, image.Vote);
            Assert.AreEqual(false, image.Favorite);
            Assert.AreEqual(null, image.Nsfw);
            Assert.AreEqual(null, image.Section);
            Assert.AreEqual(null, image.AccountUrl);
            Assert.AreEqual(null, image.AccountId);
            Assert.AreEqual("http://i.imgur.com/79MH23L.png", image.Link);
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
            var fakeUrl = "https://api.imgur.com/3/album/5F5Cy/images";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.GetAlbumImagesResponse)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
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
            var fakeUrl = "https://api.imgur.com/3/album/12x5454/remove_images?ids=AbcDef%2CIrcDef";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.RemoveAlbumImagesResponse)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
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
        public async Task RemoveAlbumImagesAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            await endpoint.RemoveAlbumImagesAsync("12x5454", null);
        }

        [TestMethod]
        public async Task SetAlbumImagesAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/album/12x5454";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.SetAlbumImagesResponse)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
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
        public async Task SetAlbumImagesAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            await endpoint.SetAlbumImagesAsync("12x5454", null);
        }

        [TestMethod]
        public async Task UpdateAlbumAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/album/12x5454";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AlbumEndpointResponses.Imgur.UpdateAlbumResponse)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
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