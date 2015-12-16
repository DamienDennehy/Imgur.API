using System.Linq;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Endpoints.AccountEndpointCommentTests
{
    [TestClass]
    public class ImgurClientWithOAuth2Tests : TestBase
    {
        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public async Task DeleteCommentAsync_WithValidReponse_ThrowsImgurException()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var deleted = await endpoint.DeleteCommentAsync("487153732");

            Assert.IsTrue(deleted);
        }

        [TestMethod]
        public async Task GetCommentCountAsync_WithValidReponse_AreEqual()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var commentCount = await endpoint.GetCommentCountAsync();

            Assert.IsTrue(commentCount > 1);
        }

        [TestMethod]
        public async Task GetCommentIdsAsync_AreEqual()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var comments = await endpoint.GetCommentIdsAsync();

            Assert.IsTrue(comments.Count() > 1);
        }

        [TestMethod]
        public async Task GetCommentsAsync_AreEqual()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var comments = await endpoint.GetCommentsAsync();

            Assert.IsTrue(comments.Count() >= 2);
        }
    }
}