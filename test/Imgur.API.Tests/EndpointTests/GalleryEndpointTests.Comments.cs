using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.EndpointTests
{
    public partial class GalleryEndpointTests
    {
        [Fact]
        public async Task CreateGalleryItemCommentAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/dO484/comment";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.CreateGalleryItemComment)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new GalleryEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var comment = await endpoint.CreateGalleryItemCommentAsync("Hello World!", "dO484").ConfigureAwait(false);

            Assert.NotNull(comment);
            Assert.Equal(548357773, comment);
        }

        [Fact]
        public async Task CreateGalleryItemCommentAsync_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.CreateGalleryItemCommentAsync(null, "Xyz").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task CreateGalleryItemCommentAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () =>
                            await endpoint.CreateGalleryItemCommentAsync("Hello World!", null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task CreateGalleryItemCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () =>
                            await endpoint.CreateGalleryItemCommentAsync("Hello World!", "Xyz").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task CreateGalleryItemCommentReplyAsync_Equal()
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

            Assert.NotNull(comment);
            Assert.Equal(548358985, comment);
        }

        [Fact]
        public async Task CreateGalleryItemCommentReplyAsync_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () =>
                            await endpoint.CreateGalleryItemCommentReplyAsync(null, "Xyz", "1234").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task CreateGalleryItemCommentReplyAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () =>
                            await
                                endpoint.CreateGalleryItemCommentReplyAsync("Hello World!", null, "1234")
                                    .ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task CreateGalleryItemCommentReplyAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () =>
                            await
                                endpoint.CreateGalleryItemCommentReplyAsync("Hello World!", "Xyz", "123")
                                    .ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task CreateGalleryItemCommentReplyAsync_WithParentIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () =>
                            await
                                endpoint.CreateGalleryItemCommentReplyAsync("Hello World!", "sshj", null)
                                    .ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetGalleryItemCommentAsync_NotNull()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/Mxd8cg0/comment/548357773";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.GetGalleryItemComment)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var comment = await endpoint.GetGalleryItemCommentAsync(548357773, "Mxd8cg0").ConfigureAwait(false);

            Assert.NotNull(comment);
            Assert.Equal(548357773, comment.Id);
            Assert.Equal("Mxd8cg0", comment.ImageId);
            Assert.Equal("Would be nice to be in my 20s again and not have to favorite this :/", comment.CommentText);
            Assert.Equal("imgurapidotnet", comment.Author);
            Assert.Equal(24562464, comment.AuthorId);
            Assert.Equal(null, comment.AlbumCover);
            Assert.Equal(1, comment.Ups);
            Assert.Equal(0, comment.Downs);
            Assert.Equal(1, comment.Points);
            Assert.Equal(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1451479114), comment.DateTime);
            Assert.Equal(0, comment.ParentId);
            Assert.Equal(false, comment.Deleted);
            Assert.Equal(VoteOption.Up, comment.Vote);
            Assert.Equal("api", comment.Platform);
            Assert.Equal(0, comment.Children.Count());
        }

        [Fact]
        public async Task GetGalleryItemCommentAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetGalleryItemCommentAsync(987878, null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetGalleryItemCommentCountAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/Mxd8cg0/comments/count";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.GetGalleryItemCommentCount)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var count = await endpoint.GetGalleryItemCommentCountAsync("Mxd8cg0").ConfigureAwait(false);

            Assert.Equal(22, count);
        }

        [Fact]
        public async Task GetGalleryItemCommentCountAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetGalleryItemCommentCountAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetGalleryItemCommentIdsAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/gallery/Mxd8cg0/comments/ids";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockGalleryEndpointResponses.GetGalleryItemCommentIds)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var commentIds = await endpoint.GetGalleryItemCommentIdsAsync("Mxd8cg0").ConfigureAwait(false);

            Assert.Equal(23, commentIds.Count());
        }

        [Fact]
        public async Task GetGalleryItemCommentIdsAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetGalleryItemCommentIdsAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetGalleryItemCommentsAsync_Equal()
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

            Assert.Equal(12, comments.Count());
        }

        [Fact]
        public async Task GetGalleryItemCommentsAsync_WithGalleryItemIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new GalleryEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetGalleryItemCommentsAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}