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

// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.Endpoints
{
    public partial class GalleryEndpointTests
    {
        [TestMethod]
        public async Task GetGalleryAsync_WithTopYearPage7_Any()
        {
            var fakeUrl = "https://api.imgur.com/3/g/memes/top/year/7";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.GetMemesSubGalleryAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var gallery =
                await
                    endpoint.GetMemesSubGalleryAsync(MemesGallerySortOrder.Top, TimeWindow.Year, 7)
                        .ConfigureAwait(false);

            Assert.IsTrue(gallery.Any());
        }

        [TestMethod]
        public async Task GetMemesSubGalleryAsync_DefaultParameters_Any()
        {
            var fakeUrl = "https://api.imgur.com/3/g/memes/viral/week/";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.GetMemesSubGalleryAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var gallery = await endpoint.GetMemesSubGalleryAsync().ConfigureAwait(false);

            Assert.IsTrue(gallery.Any());
        }

        [TestMethod]
        public async Task GetMemesSubGalleryImageAsync_IsNotNull()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/image/rNdMhHm";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.GetGalleryImageAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var image = await endpoint.GetMemesSubGalleryImageAsync("rNdMhHm").ConfigureAwait(false);

            Assert.IsNotNull(image);
            Assert.AreEqual("rNdMhHm", image.Id);
            Assert.AreEqual("wanna make money quickly? follow the instruction below", image.Title);
            Assert.AreEqual("i am not lying", image.Description);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1451524552), image.DateTime);
            Assert.AreEqual("image/png", image.Type);
            Assert.AreEqual(false, image.Animated);
            Assert.AreEqual(610, image.Width);
            Assert.AreEqual(558, image.Height);
            Assert.AreEqual(564133, image.Size);
            Assert.AreEqual(410285, image.Views);
            Assert.AreEqual(231455307905, image.Bandwidth);
            Assert.AreEqual(null, image.Vote);
            Assert.AreEqual(false, image.Favorite);
            Assert.AreEqual(false, image.Nsfw);
            Assert.AreEqual("pics", image.Section);
            Assert.AreEqual("Calasin", image.AccountUrl);
            Assert.AreEqual(22349254, image.AccountId);
            Assert.AreEqual(352, image.CommentCount);
            Assert.AreEqual(10, image.CommentPreview.Count());
            Assert.AreEqual("No Topic", image.Topic);
            Assert.AreEqual(29, image.TopicId);
            Assert.AreEqual("http://i.imgur.com/rNdMhHm.png", image.Link);
            Assert.AreEqual(352, image.CommentCount);
            Assert.AreEqual(15226, image.Ups);
            Assert.AreEqual(2339, image.Downs);
            Assert.AreEqual(12887, image.Points);
            Assert.AreEqual(13092, image.Score);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetMemesSubGalleryImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetMemesSubGalleryImageAsync(null).ConfigureAwait(false);
        }
    }
}