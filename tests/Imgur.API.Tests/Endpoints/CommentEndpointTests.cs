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
    public class CommentEndpointTests
    {
        [TestMethod]
        public async Task CreateCommentAsync_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CommentEndpointResponses.CreateCommentResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/comment"), fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new CommentEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var comment = await endpoint.CreateCommentAsync("Hello World!", "xyz", "Abc");

            Assert.IsNotNull(comment);
            Assert.AreEqual(539710677, comment.Id);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateCommentAsync_WithCommentNull_ThrowsArgumentNullException()
        {
            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateCommentAsync(null, "xyz", "Abc");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateCommentAsync_WithImageIdNull_ThrowsArgumentNullException()
        {
            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
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
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CommentEndpointResponses.CreateReplyResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/comment/BNMxDs"), fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new CommentEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var comment = await endpoint.CreateReplyAsync("Hello World!", "xyz", "BNMxDs");

            Assert.IsNotNull(comment);
            Assert.AreEqual(539717441, comment.Id);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateReplyAsync_WithCommentIdNull_ThrowsArgumentNullException()
        {
            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateReplyAsync("Hello World!", "xyz", null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateReplyAsync_WithCommentNull_ThrowsArgumentNullException()
        {
            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new CommentEndpoint(client);
            await endpoint.CreateReplyAsync(null, "xyz", "Abc");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateReplyAsync_WithImageIdNull_ThrowsArgumentNullException()
        {
            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
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
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CommentEndpointResponses.DeleteCommentResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/comment/12x5454"), fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new CommentEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var deleted = await endpoint.DeleteCommentAsync("12x5454");

            Assert.IsTrue(deleted);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteCommentAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
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
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CommentEndpointResponses.GetCommentResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/comment/539556821"), fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new CommentEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var comment = await endpoint.GetCommentAsync("539556821");

            Assert.IsNotNull(comment);
            Assert.AreEqual(comment.Id, 539556821);
            Assert.AreEqual(comment.ImageId, "n6gcXdY");
            Assert.AreEqual(comment.CommentText, "It's called smirking. Lots of people do it.");
            Assert.AreEqual(comment.Author, "WomanWiththeTattooedHands");
            Assert.AreEqual(comment.AuthorId, 499505);
            Assert.AreEqual(comment.OnAlbum, false);
            Assert.AreEqual(comment.AlbumCover, null);
            Assert.AreEqual(comment.Ups, 379);
            Assert.AreEqual(comment.Downs, 16);
            Assert.AreEqual(comment.Points, 363);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1450526522), comment.DateTime);
            Assert.AreEqual(comment.ParentId, 0);
            Assert.AreEqual(comment.Deleted, false);
            Assert.AreEqual(comment.Vote, null);
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
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CommentEndpointResponses.GetCommentReplies)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/comment/539556821/replies"),
                fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new CommentEndpoint(client, new HttpClient(fakeHttpMessageHandler));
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
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CommentEndpointResponses.ReportCommentResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/comment/539556821/report"),
                fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new CommentEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var reported = await endpoint.ReportCommentAsync("539556821", ReportReason.MatureContentNotMarked);

            Assert.IsTrue(reported);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task ReportCommentAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
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
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(CommentEndpointResponses.VoteCommentResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/comment/539556821/vote/down"),
                fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new CommentEndpoint(client, new HttpClient(fakeHttpMessageHandler));
            var reported = await endpoint.VoteCommentAsync("539556821", Vote.Down);

            Assert.IsTrue(reported);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task VoteCommentAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var client = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new CommentEndpoint(client);
            await endpoint.VoteCommentAsync(null, Vote.Down);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task VoteCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);
            await endpoint.VoteCommentAsync("539556821", Vote.Down);
        }
    }
}