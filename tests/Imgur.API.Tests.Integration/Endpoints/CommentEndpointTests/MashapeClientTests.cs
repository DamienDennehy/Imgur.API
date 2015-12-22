using System.Linq;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Endpoints.CommentEndpointTests
{
    [TestClass]
    public class MashapeClientTests: TestBase
    {
        [TestMethod]
        public async Task GetCommentAsync_AreEqual()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new CommentEndpoint(client);

            var comment = await endpoint.GetCommentAsync("540468501");

            Assert.IsNotNull(comment);
            Assert.AreEqual(540468501, comment.Id);
        }

        [TestMethod]
        public async Task GetRepliesAsync_AreEqual()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new CommentEndpoint(client);

            var comment = await endpoint.GetRepliesAsync("540468501");

            Assert.IsNotNull(comment);
            Assert.IsTrue(comment.Children.Any());
        }
    }
}
