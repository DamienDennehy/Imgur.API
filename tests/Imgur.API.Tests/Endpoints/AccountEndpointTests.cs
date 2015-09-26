using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Helpers;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.Tests.EndpointResponses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class AccountEndpointTests
    {
        private ImageHelper ImageHelper { get; } = new ImageHelper();

        [TestMethod]
        public void GetAccountAsync_WithUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountAsync("Bob");
            endpoint.Received().GetAccountAsync("Bob");
        }

        [TestMethod]
        public void GetAccountAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountAsync();
            endpoint.Received().GetAccountAsync("me");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetAccountAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAccountAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetAccountAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAccountAsync();
        }

        [TestMethod]
        public void GetAccountAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var account = endpoint.ProcessEndpointResponse<Account>(AccountEndpointResponses.Imgur.GetAccountResponse);

            Assert.AreEqual(12456, account.Id);
            Assert.AreEqual("Bob", account.Url);
            Assert.AreEqual(null, account.Bio);
            Assert.AreEqual(4343, account.Reputation);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1229591601), account.Created);
        }

        [TestMethod]
        public void GetAccountGalleryFavoritesAsync_WithUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountGalleryFavoritesAsync("Bob");
            endpoint.Received().GetAccountGalleryFavoritesAsync("Bob");
        }

        [TestMethod]
        public void GetAccountGalleryFavoritesAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountGalleryFavoritesAsync();
            endpoint.Received().GetAccountGalleryFavoritesAsync();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetAccountGalleryFavoritesAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAccountGalleryFavoritesAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetAccountGalleryFavoritesAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAccountGalleryFavoritesAsync();
        }

        [TestMethod]
        public void GetAccountGalleryFavoritesAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var favorites = endpoint.ProcessEndpointResponse<IEnumerable<object>>(AccountEndpointResponses.Imgur.GetAccountGalleryFavoritesResponse);

            Assert.AreEqual(favorites.Count(), ImageHelper.ConvertToGalleryItems(favorites).Count());
        }

        [TestMethod]
        public void GetAccountFavoritesAsync_WithUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountFavoritesAsync();
            endpoint.Received().GetAccountFavoritesAsync();
        }

        [TestMethod]
        public void GetAccountFavoritesAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountFavoritesAsync();
            endpoint.Received().GetAccountFavoritesAsync();
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetAccountFavoritesAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAccountFavoritesAsync();
        }

        [TestMethod]
        public void GetAccountFavoritesAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var favorites = endpoint.ProcessEndpointResponse<IEnumerable<object>>(AccountEndpointResponses.Imgur.GetAccountFavoritesResponse);

            Assert.AreEqual(favorites.Count(), ImageHelper.ConvertToGalleryItems(favorites).Count());
        }

        [TestMethod]
        public void GetAccountSubmissionsAsync_WithUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountSubmissionsAsync("Bob");
            endpoint.Received().GetAccountSubmissionsAsync("Bob");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetAccountSubmissionsAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAccountSubmissionsAsync();
        }

        [TestMethod]
        public void GetAccountSubmissionsAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountSubmissionsAsync();
            endpoint.Received().GetAccountSubmissionsAsync();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetAccountSubmissionsAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAccountSubmissionsAsync(null);
        }

        [TestMethod]
        public void GetAccountSubmissionsAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var submissions = endpoint.ProcessEndpointResponse<IEnumerable<object>>(AccountEndpointResponses.Imgur.GetAccountSubmissionsResponse);

            Assert.AreEqual(submissions.Count(), ImageHelper.ConvertToGalleryItems(submissions).Count());
        }
    }
}