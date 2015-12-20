using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Enums;
using Imgur.API.RequestBuilders;
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
            var requestBuilder = new AccountRequestBuilder();

            var url = $"{client.EndpointUrl}account/me/settings";
            var request = requestBuilder.UpdateAccountSettingsRequest(
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
            var requestBuilder = new AccountRequestBuilder();
            requestBuilder.UpdateAccountSettingsRequest(null);
        }
    }
}