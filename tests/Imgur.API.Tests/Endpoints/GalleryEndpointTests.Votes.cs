using System;
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
        public async Task GetGalleryItemVotesAsync_AreEqual()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/RoAjx/votes";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.GetGalleryItemVotesAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var votes = await endpoint.GetGalleryItemVotesAsync("RoAjx").ConfigureAwait(false);

            Assert.IsNotNull(votes);
            Assert.AreEqual(751, votes.Downs);
            Assert.AreEqual(11347, votes.Ups);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryItemVotesAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetGalleryItemVotesAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task VoteGalleryItemAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/gallery/XoPkL/vote/down";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GalleryEndpointResponses.VoteGalleryTagAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new GalleryEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var voted = await endpoint.VoteGalleryItemAsync("XoPkL", VoteOption.Down).ConfigureAwait(false);

            Assert.IsNotNull(voted);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task VoteGalleryItemAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new GalleryEndpoint(client);
            await endpoint.VoteGalleryItemAsync(null, VoteOption.Down).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task VoteGalleryItemAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.VoteGalleryItemAsync("kjkjk", VoteOption.Down).ConfigureAwait(false);
        }
    }
}