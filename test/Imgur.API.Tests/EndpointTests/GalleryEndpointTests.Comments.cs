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
    public partial class GalleryEndpointTests
    {
        [TestMethod]
        public async Task CreateGalleryItemCommentAsync_AreEqual()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/dO484/comment";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.CreateGalleryItemComment)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new GalleryEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var comment = await endpoint.CreateGalleryItemCommentAsync("Hello World!", "dO484").ConfigureAwait(false);

            Assert.IsNotNull(comment);
            Assert.AreEqual(548357773, comment);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateGalleryItemCommentAsync_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.CreateGalleryItemCommentAsync(null, "Xyz").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateGalleryItemCommentAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.CreateGalleryItemCommentAsync("Hello World!", null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateGalleryItemCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.CreateGalleryItemCommentAsync("Hello World!", "Xyz").ConfigureAwait(false);
        }

        [TestMethod]
        public async Task CreateGalleryItemCommentReplyAsync_AreEqual()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/dO484/comment/1234890";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.CreateGalleryItemCommentReply)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new GalleryEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var comment =
                await
                    endpoint.CreateGalleryItemCommentReplyAsync("Hello World!", "dO484", "1234890")
                        .ConfigureAwait(false);

            Assert.IsNotNull(comment);
            Assert.AreEqual(548358985, comment);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateGalleryItemCommentReplyAsync_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.CreateGalleryItemCommentReplyAsync(null, "Xyz", "1234").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateGalleryItemCommentReplyAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.CreateGalleryItemCommentReplyAsync("Hello World!", null, "1234").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateGalleryItemCommentReplyAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.CreateGalleryItemCommentReplyAsync("Hello World!", "Xyz", "123").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task CreateGalleryItemCommentReplyAsync_WithParentIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.CreateGalleryItemCommentReplyAsync("Hello World!", "sshj", null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetGalleryItemCommentAsync_IsNotNull()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/Mxd8cg0/comment/548357773";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.GetGalleryItemComment)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var comment = await endpoint.GetGalleryItemCommentAsync(548357773, "Mxd8cg0").ConfigureAwait(false);

            Assert.IsNotNull(comment);
            Assert.AreEqual(548357773, comment.Id);
            Assert.AreEqual("Mxd8cg0", comment.ImageId);
            Assert.AreEqual("Would be nice to be in my 20s again and not have to favorite this :/", comment.CommentText);
            Assert.AreEqual("imgurapidotnet", comment.Author);
            Assert.AreEqual(24562464, comment.AuthorId);
            Assert.AreEqual(null, comment.AlbumCover);
            Assert.AreEqual(1, comment.Ups);
            Assert.AreEqual(0, comment.Downs);
            Assert.AreEqual(1, comment.Points);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1451479114), comment.DateTime);
            Assert.AreEqual(0, comment.ParentId);
            Assert.AreEqual(false, comment.Deleted);
            Assert.AreEqual(VoteOption.Up, comment.Vote);
            Assert.AreEqual("api", comment.Platform);
            Assert.AreEqual(0, comment.Children.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryItemCommentAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetGalleryItemCommentAsync(987878, null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetGalleryItemCommentCountAsync_AreEqual()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/Mxd8cg0/comments/count";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.GetGalleryItemCommentCount)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var count = await endpoint.GetGalleryItemCommentCountAsync("Mxd8cg0").ConfigureAwait(false);

            Assert.AreEqual(22, count);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryItemCommentCountAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetGalleryItemCommentCountAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetGalleryItemCommentIdsAsync_AreEqual()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/Mxd8cg0/comments/ids";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.GetGalleryItemCommentIds)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var commentIds = await endpoint.GetGalleryItemCommentIdsAsync("Mxd8cg0").ConfigureAwait(false);

            Assert.AreEqual(23, commentIds.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryItemCommentIdsAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetGalleryItemCommentIdsAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetGalleryItemCommentsAsync_AreEqual()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/Mxd8cg0/comments/oldest";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.GetGalleryItemComments)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var comments =
                await endpoint.GetGalleryItemCommentsAsync("Mxd8cg0", CommentSortOrder.Oldest).ConfigureAwait(false);

            Assert.AreEqual(12, comments.Count());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryItemCommentsAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);
            await endpoint.GetGalleryItemCommentsAsync(null).ConfigureAwait(false);
        }
    }
}