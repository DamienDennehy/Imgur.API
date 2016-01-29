using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.EndpointTests
{
    [TestClass]
    public class CommentEndpointTests : TestBase
    {
        [TestMethod]
        public async Task CreateCommentAsync_AreEqual()
        {
            var mockUrl = "https://api.imgur.com/3/comment";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCommentEndpointResponses.CreateComment)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var comment = await endpoint.CreateCommentAsync("Hello World!", "xyz", "Abc").ConfigureAwait(false);

            Assert.IsNotNull(comment);
            Assert.AreEqual(539710677, comment);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateCommentAsync_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateCommentAsync(null, "xyz", "Abc").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateCommentAsync_WithImageIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateCommentAsync("Hello World!", null, "Abc").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateCommentAsync("Hello World", "xyz", "Abc").ConfigureAwait(false);
        }

        [TestMethod]
        public async Task CreateReplyAsync_AreEqual()
        {
            var mockUrl = "https://api.imgur.com/3/comment/BNMxDs";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCommentEndpointResponses.CreateReply)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var comment = await endpoint.CreateReplyAsync("Hello World!", "xyz", "BNMxDs").ConfigureAwait(false);

            Assert.IsNotNull(comment);
            Assert.AreEqual(539717441, comment);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateReplyAsync_WithCommentIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateReplyAsync("Hello World!", "xyz", null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateReplyAsync_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateReplyAsync(null, "xyz", "Abc").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateReplyAsync_WithImageIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateReplyAsync("Hello World", null, "Abc").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateReplyAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateReplyAsync("Hello World", "xyz", "Abc").ConfigureAwait(false);
        }

        [TestMethod]
        public async Task DeleteCommentAsync_IsTrue()
        {
            var mockUrl = "https://api.imgur.com/3/comment/6767676";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCommentEndpointResponses.DeleteComment)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var deleted = await endpoint.DeleteCommentAsync(6767676).ConfigureAwait(false);

            Assert.IsTrue(deleted);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);
            await endpoint.DeleteCommentAsync(678867866).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetCommentAsync_IsTrue()
        {
            var mockUrl = "https://api.imgur.com/3/comment/539556821";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCommentEndpointResponses.GetComment)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var comment = await endpoint.GetCommentAsync(539556821).ConfigureAwait(false);

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
        public async Task GetRepliesAsync_IsTrue()
        {
            var mockUrl = "https://api.imgur.com/3/comment/539556821/replies";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCommentEndpointResponses.GetCommentReplies)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var comments = await endpoint.GetRepliesAsync(539556821).ConfigureAwait(false);

            Assert.IsTrue(comments.Children.Count() == 7);
        }

        [TestMethod]
        public async Task ReportCommentAsync_IsTrue()
        {
            var mockUrl = "https://api.imgur.com/3/comment/539556821/report";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCommentEndpointResponses.ReportComment)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var reported =
                await
                    endpoint.ReportCommentAsync(539556821, ReportReason.MatureContentNotMarked).ConfigureAwait(false);

            Assert.IsTrue(reported);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task ReportCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);
            await endpoint.ReportCommentAsync(539556821, ReportReason.Spam).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task VoteCommentAsync_IsTrue()
        {
            var mockUrl = "https://api.imgur.com/3/comment/539556821/vote/down";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCommentEndpointResponses.VoteComment)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var reported = await endpoint.VoteCommentAsync(539556821, VoteOption.Down).ConfigureAwait(false);

            Assert.IsTrue(reported);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task VoteCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);
            await endpoint.VoteCommentAsync(539556821, VoteOption.Down).ConfigureAwait(false);
        }
    }
}