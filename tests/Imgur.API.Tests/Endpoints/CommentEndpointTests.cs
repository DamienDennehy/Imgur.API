using System;
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
    [TestClass]
    public class CommentEndpointTests : TestBase
    {
        [TestMethod]
        public async Task CreateCommentAsync_AreEqual()
        {
            var fakeUrl = "https://api.imgur.com/3/comment";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CommentEndpointResponses.CreateCommentResponse)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CommentEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var comment = await endpoint.CreateCommentAsync("Hello World!", "xyz", "Abc");

            Assert.IsNotNull(comment);
            Assert.AreEqual(539710677, comment.Id);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateCommentAsync_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateCommentAsync(null, "xyz", "Abc");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateCommentAsync_WithImageIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateCommentAsync("Hello World!", null, "Abc");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateCommentAsync("Hello World", "xyz", "Abc");
        }

        [TestMethod]
        public async Task CreateReplyAsync_AreEqual()
        {
            var fakeUrl = "https://api.imgur.com/3/comment/BNMxDs";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CommentEndpointResponses.CreateReplyResponse)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CommentEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var comment = await endpoint.CreateReplyAsync("Hello World!", "xyz", "BNMxDs");

            Assert.IsNotNull(comment);
            Assert.AreEqual(539717441, comment.Id);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateReplyAsync_WithCommentIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateReplyAsync("Hello World!", "xyz", null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateReplyAsync_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateReplyAsync(null, "xyz", "Abc");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateReplyAsync_WithImageIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateReplyAsync("Hello World", null, "Abc");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateReplyAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateReplyAsync("Hello World", "xyz", "Abc");
        }

        [TestMethod]
        public async Task DeleteCommentAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/comment/12x5454";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CommentEndpointResponses.DeleteCommentResponse)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CommentEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var deleted = await endpoint.DeleteCommentAsync("12x5454");

            Assert.IsTrue(deleted);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteCommentAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CommentEndpoint(client);
            await endpoint.DeleteCommentAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);
            await endpoint.DeleteCommentAsync("12x5454");
        }

        [TestMethod]
        public async Task GetCommentAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/comment/539556821";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CommentEndpointResponses.GetCommentResponse)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var comment = await endpoint.GetCommentAsync("539556821");

            Assert.IsNotNull(comment);
            Assert.AreEqual(539556821, comment.Id);
            Assert.AreEqual("n6gcXdY", comment.ImageId);
            Assert.AreEqual("It's called smirking. Lots of people do it.", comment.CommentText);
            Assert.AreEqual("WomanWiththeTattooedHands", comment.Author);
            Assert.AreEqual(499505, comment.AuthorId);
            Assert.AreEqual(false, comment.OnAlbum);
            Assert.AreEqual(null, comment.AlbumCover);
            Assert.AreEqual(379, comment.Ups);
            Assert.AreEqual(16, comment.Downs);
            Assert.AreEqual(363, comment.Points);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1450526522), comment.DateTime);
            Assert.AreEqual(0, comment.ParentId);
            Assert.AreEqual("iphone", comment.Platform);
            Assert.AreEqual(false, comment.Deleted);
            Assert.AreEqual(VoteOption.Veto, comment.Vote);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);
            await endpoint.GetCommentAsync(null);
        }

        [TestMethod]
        public async Task GetRepliesAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/comment/539556821/replies";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CommentEndpointResponses.GetCommentReplies)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var comments = await endpoint.GetRepliesAsync("539556821");

            Assert.IsTrue(comments.Children.Count() == 7);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetRepliesAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);
            await endpoint.GetRepliesAsync(null);
        }

        [TestMethod]
        public async Task ReportCommentAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/comment/539556821/report";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CommentEndpointResponses.ReportCommentResponse)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CommentEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var reported = await endpoint.ReportCommentAsync("539556821", ReportReason.MatureContentNotMarked);

            Assert.IsTrue(reported);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task ReportCommentAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CommentEndpoint(client);
            await endpoint.ReportCommentAsync(null, ReportReason.Spam);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task ReportCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);
            await endpoint.ReportCommentAsync("539556821", ReportReason.Spam);
        }

        [TestMethod]
        public async Task VoteCommentAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/comment/539556821/vote/down";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CommentEndpointResponses.VoteCommentResponse)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CommentEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var reported = await endpoint.VoteCommentAsync("539556821", VoteOption.Down);

            Assert.IsTrue(reported);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task VoteCommentAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new CommentEndpoint(client);
            await endpoint.VoteCommentAsync(null, VoteOption.Down);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task VoteCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);
            await endpoint.VoteCommentAsync("539556821", VoteOption.Down);
        }
    }
}