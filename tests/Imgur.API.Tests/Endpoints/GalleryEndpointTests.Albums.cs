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
    [TestClass]
    public class GalleryEndpointTests
    {
        [TestMethod]
        public async Task GetAlbumAsync_IsNotNull()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/album/dO484";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.GetGalleryAlbumResponse)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var album = await endpoint.GetGalleryAlbumAsync("dO484");

            Assert.IsNotNull(album);
            Assert.AreEqual("dO484", album.Id);
            Assert.AreEqual("25 Films on Netflix I'd like to recommend.", album.Title);
            Assert.AreEqual(null, album.Description);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1451445880), album.DateTime);
            Assert.AreEqual("xUPbs5g", album.Cover);
            Assert.AreEqual(700, album.CoverWidth);
            Assert.AreEqual(394, album.CoverHeight);
            Assert.AreEqual("bellsofwar3", album.AccountUrl);
            Assert.AreEqual(28720941, album.AccountId);
            Assert.AreEqual(AlbumPrivacy.Public, album.Privacy);
            Assert.AreEqual(AlbumLayout.Blog, album.Layout);
            Assert.AreEqual(13972, album.Views);
            Assert.AreEqual("http://imgur.com/a/dO484", album.Link);
            Assert.AreEqual(2024, album.Ups);
            Assert.AreEqual(28, album.Downs);
            Assert.AreEqual(1996, album.Points);
            Assert.AreEqual(1994, album.Score);
            Assert.AreEqual(null, album.Vote);
            Assert.AreEqual(false, album.Favorite);
            Assert.AreEqual(false, album.Nsfw);
            Assert.AreEqual("", album.Section);
            Assert.AreEqual(79, album.CommentCount);
            Assert.AreEqual(10, album.CommentPreview.Count());
            Assert.AreEqual("The More You Know", album.Topic);
            Assert.AreEqual(11, album.TopicId);
            Assert.AreEqual(25, album.ImagesCount);
            Assert.AreEqual(25, album.Images.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetGalleryAlbumAsync(null);
        }
    }
}