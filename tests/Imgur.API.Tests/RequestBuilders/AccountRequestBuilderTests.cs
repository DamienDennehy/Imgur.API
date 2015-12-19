using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.RequestBuilders
{
    [TestClass]
    public class AccountRequestBuilderTests
    {
        [TestMethod]
        public async Task UpdateAccountSettingsRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var url = $"{endpoint.ApiClient.EndpointUrl}account/me/settings";
            var request = endpoint.RequestBuilder.UpdateAccountSettingsRequest(
                url, "BioTest", true, true, AlbumPrivacy.Public,
                true, "Bob2", true, true);

            Assert.IsNotNull(request);
            var expected =
                "public_images=true&messaging_enabled=true&album_privacy=public&accepted_gallery_terms=true&show_mature=true&newsletter_subscribed=true&username=Bob2&bio=BioTest";

            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync());
            Assert.AreEqual("https://api.imgur.com/3/account/me/settings", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UpdateAccountSettingsRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            endpoint.RequestBuilder.UpdateAccountSettingsRequest(null);
        }
    }
}