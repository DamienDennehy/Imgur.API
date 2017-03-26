using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Enums;
using Imgur.API.RequestBuilders;
using Xunit;

namespace Imgur.API.Tests.RequestBuilderTests
{
    public class AccountRequestBuilderTests
    {
        [Fact]
        public async Task UpdateAccountSettingsRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AccountRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}account/me/settings";
            var request = AccountRequestBuilder.UpdateAccountSettingsRequest(
                mockUrl, "BioTest", true, true, AlbumPrivacy.Public,
                true, "Bob2", true, true);

            Assert.NotNull(request);
            var expected =
                "public_images=true&messaging_enabled=true&album_privacy=public&accepted_gallery_terms=true&show_mature=true&newsletter_subscribed=true&bio=BioTest&username=Bob2";

            Assert.Equal(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("https://api.imgur.com/3/account/me/settings", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
        }

        [Fact]
        public void UpdateAccountSettingsRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AccountRequestBuilder();

            var exception = Record.Exception(() => AccountRequestBuilder.UpdateAccountSettingsRequest(null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }
    }
}