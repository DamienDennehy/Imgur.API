using System.Linq;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Endpoints.CommentEndpointTests
{
    [TestClass]
    public class MashapeClientWithOAuth2Tests : TestBase
    {
        [TestMethod]
        public async Task GetCommentAsync_AreEqual()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, OAuth2Token);
            var endpoint = new CommentEndpoint(client);

            var comment = await endpoint.GetCommentAsync("540468501");

            Assert.IsNotNull(comment);
            Assert.AreEqual(540468501, comment.Id);
        }

        [TestMethod]
        public async Task GetRepliesAsync_AreEqual()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, OAuth2Token);
            var endpoint = new CommentEndpoint(client);

            var comment = await endpoint.GetRepliesAsync("540468501");

            Assert.IsNotNull(comment);
            Assert.IsTrue(comment.Children.Any());
        }

        [TestMethod]
        public async Task CreateCommentAsync_IsNotNull()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, OAuth2Token);
            var endpoint = new CommentEndpoint(client);

            var comment = await endpoint.CreateCommentAsync("Create Comment", "BJRWQw5");

            Assert.IsNotNull(comment);
        }

        [TestMethod]
        public async Task CreateCommentAsync_WithParentId_IsNotNull()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, OAuth2Token);
            var endpoint = new CommentEndpoint(client);

            var comment = await endpoint.CreateCommentAsync("Create Comment with Parent", "BJRWQw5", "540767605");

            Assert.IsNotNull(comment);
        }

        [TestMethod]
        public async Task CreateReplyAsync_IsNotNull()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, OAuth2Token);
            var endpoint = new CommentEndpoint(client);

            var comment = await endpoint.CreateReplyAsync("Create Reply", "BJRWQw5", "540767605");

            Assert.IsNotNull(comment);
        }

        [TestMethod]
        public async Task DeleteCommentAsync_IsTrue()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, OAuth2Token);
            var endpoint = new CommentEndpoint(client);

            var comment = await endpoint.CreateCommentAsync("Create Comment", "BJRWQw5");
            var deleted = await endpoint.DeleteCommentAsync(comment.Id.ToString());

            Assert.IsTrue(deleted);
        }

        [TestMethod]
        public async Task VoteCommentAsync_IsTrue()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, OAuth2Token);
            var endpoint = new CommentEndpoint(client);

            var comment = await endpoint.CreateCommentAsync("Create Comment", "BJRWQw5");
            comment = await endpoint.GetCommentAsync("540468501");
            var voted = await endpoint.VoteCommentAsync(comment.Id.ToString(), Vote.Down);
            comment = await endpoint.GetCommentAsync("540468501");

            Assert.IsTrue(voted);
            Assert.IsTrue(comment.Vote == Vote.Down);
        }

        //Running the ReportComment method may cause your account to be banned?
        //[TestMethod]
        //public async Task ReportCommentAsync_IsTrue()
        //{
        //    var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, OAuth2Token);
        //    var endpoint = new CommentEndpoint(client);

        //    var comment = await endpoint.CreateCommentAsync("Create Comment", "BJRWQw5");
        //    var reported = await endpoint.ReportCommentAsync(comment.Id.ToString(), ReportReason.DoesNotBelong);

        //    Assert.IsTrue(reported);
        //}
    }
}
