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
    public partial class AccountEndpointTests
    {
        [TestMethod]
        public async Task DeleteCommentAsync_IsNotNull()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.DeleteCommentResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/sarah/comment/yMgB7"),
                fakeResponse);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var deleted = await endpoint.DeleteCommentAsync("yMgB7", "sarah");

            Assert.IsTrue(deleted);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteCommentAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.DeleteCommentAsync("yMgB7", null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteCommentAsync_WithNullId_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.DeleteCommentAsync(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteCommentAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.DeleteCommentAsync("yMgB7", null);
        }

        [TestMethod]
        public async Task GetCommentAsync_IsNotNull()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetCommentResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/sarah/comment/yMgB7"),
                fakeResponse);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var comment = await endpoint.GetCommentAsync("yMgB7", "sarah");

            Assert.IsNotNull(comment);
            Assert.AreEqual(comment.Id, 487008510);
            Assert.AreEqual(comment.ImageId, "DMcOm2V");
            Assert.AreEqual(comment.CommentText, "Gyroscope detectors measure inertia.. the stabilization is done entirely by brushless motors. There are stabilizers which actually use 1/2");
            Assert.AreEqual(comment.Author, "Scabab");
            Assert.AreEqual(comment.AuthorId, 4194299);
            Assert.AreEqual(comment.OnAlbum, false);
            Assert.AreEqual(comment.AlbumCover, null);
            Assert.AreEqual(comment.Ups, 24);
            Assert.AreEqual(comment.Downs, 0);
            Assert.AreEqual(comment.Points, 24);
            Assert.AreEqual(comment.DateTime, new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1443969120));
            Assert.AreEqual(comment.ParentId, 486983435);
            Assert.AreEqual(comment.Deleted, false);
            Assert.AreEqual(comment.Vote, null);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetCommentAsync("yMgB7", null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentAsync_WithNullId_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetCommentAsync(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetCommentAsync("yMgB7", null);
        }

        [TestMethod]
        public async Task GetCommentCountAsync_IsNotNull()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetCommentCountResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/sarah/comments/count"),
                fakeResponse);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var count = await endpoint.GetCommentCountAsync("sarah");

            Assert.AreEqual(count, 1500);
        }

        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentCountAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetCommentCountAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentCountAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetCommentCountAsync(null);
        }

        [TestMethod]
        public async Task GetCommentIdsAsync_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetCommentIdsResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/bob/comments/ids/worst/2"),
                fakeResponse);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var comments = await endpoint.GetCommentIdsAsync("bob", CommentSortOrder.Worst, 2);

            Assert.AreEqual(50, comments.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentIdsAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetCommentIdsAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentIdsAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetCommentIdsAsync(null);
        }

        [TestMethod]
        public async Task GetCommentsAsync_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetCommentsResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/bob/comments/worst/2"), fakeResponse);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var comments = await endpoint.GetCommentsAsync("bob", CommentSortOrder.Worst, 2);

            Assert.AreEqual(50, comments.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentsAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetCommentsAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentsAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetCommentsAsync(null);
        }
    }
}