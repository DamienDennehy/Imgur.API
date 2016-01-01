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
// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.Endpoints
{
    public partial class AccountEndpointTests
    {
        [TestMethod]
        public async Task DeleteCommentAsync_IsNotNull()
        {
            var fakeUrl = "https://api.imgur.com/3/account/sarah/comment/yMgB7";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.DeleteCommentAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var deleted = await endpoint.DeleteCommentAsync("yMgB7", "sarah").ConfigureAwait(false);

            Assert.IsTrue(deleted);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteCommentAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteCommentAsync("yMgB7", null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteCommentAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteCommentAsync(null, null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteCommentAsync("yMgB7", "sarah").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteCommentAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteCommentAsync("yMgB7", null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetCommentAsync_IsNotNull()
        {
            var fakeUrl = "https://api.imgur.com/3/account/sarah/comment/yMgB7";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetCommentAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var comment = await endpoint.GetCommentAsync("yMgB7", "sarah").ConfigureAwait(false);

            Assert.IsNotNull(comment);
            Assert.AreEqual(487008510, comment.Id);
            Assert.AreEqual("DMcOm2V", comment.ImageId);
            Assert.AreEqual(
                "Gyroscope detectors measure inertia.. the stabilization is done entirely by brushless motors. There are stabilizers which actually use 1/2",
                comment.CommentText);
            Assert.AreEqual("Scabab", comment.Author);
            Assert.AreEqual(4194299, comment.AuthorId);
            Assert.AreEqual(false, comment.OnAlbum);
            Assert.AreEqual(null, comment.AlbumCover);
            Assert.AreEqual(24, comment.Ups);
            Assert.AreEqual(0, comment.Downs);
            Assert.AreEqual(24, comment.Points);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1443969120), comment.DateTime);
            Assert.AreEqual(486983435, comment.ParentId);
            Assert.AreEqual(false, comment.Deleted);
            Assert.AreEqual(VoteOption.Down, comment.Vote);
            Assert.AreEqual(comment.Platform, "desktop");
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetCommentAsync("yMgB7", null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetCommentAsync(null, null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetCommentAsync("yMgB7", null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetCommentCountAsync_IsNotNull()
        {
            var fakeUrl = "https://api.imgur.com/3/account/sarah/comments/count";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetCommentCountAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var count = await endpoint.GetCommentCountAsync("sarah").ConfigureAwait(false);

            Assert.AreEqual(count, 1500);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentCountAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetCommentCountAsync().ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentCountAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetCommentCountAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetCommentIdsAsync_AreEqual()
        {
            var fakeUrl = "https://api.imgur.com/3/account/bob/comments/ids/worst/2";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetCommentIdsAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var comments = await endpoint.GetCommentIdsAsync("bob", CommentSortOrder.Worst, 2).ConfigureAwait(false);

            Assert.AreEqual(50, comments.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentIdsAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetCommentIdsAsync().ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentIdsAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetCommentIdsAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetCommentsAsync_AreEqual()
        {
            var fakeUrl = "https://api.imgur.com/3/account/bob/comments/worst/2";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetCommentsAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var comments = await endpoint.GetCommentsAsync("bob", CommentSortOrder.Worst, 2).ConfigureAwait(false);

            Assert.AreEqual(50, comments.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentsAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetCommentsAsync().ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentsAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetCommentsAsync(null).ConfigureAwait(false);
        }
    }
}