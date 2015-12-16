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
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);

            var url = $"{endpoint.GetEndpointBaseUrl()}account/me/settings";
            var request = endpoint.RequestBuilder.UpdateAccountSettingsRequest(
                url, "BioTest", true, true, AlbumPrivacy.Public,
                true, "Bob2", true, true);

            Assert.IsNotNull(request);
            var expected =
                "bio=BioTest&public_images=true&messaging_enabled=true&album_privacy=public&accepted_gallery_terms=true&username=Bob2&show_mature=true&newsletter_subscribed=true";

            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync());
            Assert.AreEqual("https://api.imgur.com/3/account/me/settings", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UpdateAccountSettingsRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            endpoint.RequestBuilder.UpdateAccountSettingsRequest(null);
        }
    }
}