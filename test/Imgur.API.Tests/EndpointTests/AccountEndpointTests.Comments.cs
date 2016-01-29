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
    public partial class AccountEndpointTests
    {
        [TestMethod]
        public async Task DeleteCommentAsync_IsNotNull()
        {
            var mockUrl = "https://api.imgur.com/3/account/sarah/comment/478897894";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.DeleteComment)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var deleted = await endpoint.DeleteCommentAsync(478897894, "sarah").ConfigureAwait(false);

            Assert.IsTrue(deleted);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteCommentAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteCommentAsync(456456456, null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteCommentAsync(435345345, "sarah").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteCommentAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client);
            await endpoint.DeleteCommentAsync(45345353, null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetCommentAsync_IsNotNull()
        {
            var mockUrl = "https://api.imgur.com/3/account/sarah/comment/8787887";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetComment)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var comment = await endpoint.GetCommentAsync(8787887, "sarah").ConfigureAwait(false);

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
            await endpoint.GetCommentAsync(453534535, null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetCommentAsync(68767677, null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetCommentCountAsync_IsNotNull()
        {
            var mockUrl = "https://api.imgur.com/3/account/sarah/comments/count";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetCommentCount)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
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
            var mockUrl = "https://api.imgur.com/3/account/bob/comments/ids/worst/2";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetCommentIds)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
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
            var mockUrl = "https://api.imgur.com/3/account/bob/comments/worst/2";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetComments)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
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