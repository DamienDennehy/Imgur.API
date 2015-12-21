﻿using System;
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
        public async Task GetAccountFavoritesAsync_Any()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetAccountFavoritesResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/me/favorites"), fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var favorites = await endpoint.GetAccountFavoritesAsync();

            Assert.IsTrue(favorites.Any());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountFavoritesAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAccountFavoritesAsync();
        }

        [TestMethod]
        public async Task GetAccountGalleryFavoritesAsync_Any()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetAccountGalleryFavoritesResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(
                new Uri("https://api.imgur.com/3/account/me/gallery_favorites/2/oldest"), fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var favorites = await endpoint.GetAccountGalleryFavoritesAsync(page: 2, sort: GallerySortOrder.Oldest);

            Assert.IsTrue(favorites.Any());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountGalleryFavoritesAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException
            ()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAccountGalleryFavoritesAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountGalleryFavoritesAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAccountGalleryFavoritesAsync(null);
        }

        [TestMethod]
        public async Task GetAccountSubmissionsAsync_Any()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetAccountSubmissionsResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/me/submissions/2"),
                fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var submissions = await endpoint.GetAccountSubmissionsAsync(page: 2);

            Assert.IsTrue(submissions.Any());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountSubmissionsAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAccountSubmissionsAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountSubmissionsAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAccountSubmissionsAsync(null);
        }

        [TestMethod]
        public async Task GetGalleryProfileAsync_IsNotNull()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetGalleryProfileResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/me/gallery_profile"),
                fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var profile = await endpoint.GetGalleryProfileAsync();

            Assert.IsNotNull(profile);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryProfileAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetGalleryProfileAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryProfileAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetGalleryProfileAsync(null);
        }
    }
}