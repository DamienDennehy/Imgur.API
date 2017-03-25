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
    public class CommentEndpointTests : TestBase
    {
        [Fact]
        public async Task CreateCommentAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/comment";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCommentEndpointResponses.CreateComment)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var comment = await endpoint.CreateCommentAsync("Hello World!", "xyz", "Abc").ConfigureAwait(false);

            Assert.NotNull(comment);
            Assert.Equal(539710677, comment);
        }

        [Fact]
        public async Task CreateCommentAsync_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.CreateCommentAsync(null, "xyz", "Abc").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task CreateCommentAsync_WithImageIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.CreateCommentAsync("Hello World!", null, "Abc").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task CreateCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.CreateCommentAsync("Hello World", "xyz", "Abc").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task CreateReplyAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/comment/BNMxDs";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCommentEndpointResponses.CreateReply)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var comment = await endpoint.CreateReplyAsync("Hello World!", "xyz", "BNMxDs").ConfigureAwait(false);

            Assert.NotNull(comment);
            Assert.Equal(539717441, comment);
        }

        [Fact]
        public async Task CreateReplyAsync_WithCommentIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.CreateReplyAsync("Hello World!", "xyz", null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task CreateReplyAsync_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.CreateReplyAsync(null, "xyz", "Abc").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task CreateReplyAsync_WithImageIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.CreateReplyAsync("Hello World", null, "Abc").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task CreateReplyAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.CreateReplyAsync("Hello World", "xyz", "Abc").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task DeleteCommentAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/comment/6767676";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCommentEndpointResponses.DeleteComment)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var deleted = await endpoint.DeleteCommentAsync(6767676).ConfigureAwait(false);

            Assert.True(deleted);
        }

        [Fact]
        public async Task DeleteCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.DeleteCommentAsync(678867866).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetCommentAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/comment/539556821";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCommentEndpointResponses.GetComment)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var comment = await endpoint.GetCommentAsync(539556821).ConfigureAwait(false);

            Assert.NotNull(comment);
            Assert.Equal(539556821, comment.Id);
            Assert.Equal("n6gcXdY", comment.ImageId);
            Assert.Equal("It's called smirking. Lots of people do it.", comment.CommentText);
            Assert.Equal("WomanWiththeTattooedHands", comment.Author);
            Assert.Equal(499505, comment.AuthorId);
            Assert.Equal(false, comment.OnAlbum);
            Assert.Equal(null, comment.AlbumCover);
            Assert.Equal(379, comment.Ups);
            Assert.Equal(16, comment.Downs);
            Assert.Equal(363, comment.Points);
            Assert.Equal(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1450526522), comment.DateTime);
            Assert.Equal(0, comment.ParentId);
            Assert.Equal("iphone", comment.Platform);
            Assert.Equal(false, comment.Deleted);
            Assert.Equal(VoteOption.Veto, comment.Vote);
        }

        [Fact]
        public async Task GetCommentAsync_False()
        {
            var mockUrl = "https://api.imgur.com/3/comment/539556821";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCommentEndpointResponses.GetCommentNotExists)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetCommentAsync(539556821).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
        }

        [Fact]
        public async Task GetRepliesAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/comment/539556821/replies";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCommentEndpointResponses.GetCommentReplies)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var comments = await endpoint.GetRepliesAsync(539556821).ConfigureAwait(false);

            Assert.True(comments.Children.Count() == 7);
        }

        [Fact]
        public async Task ReportCommentAsync_True()
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

            Assert.True(reported);
        }

        [Fact]
        public async Task ReportCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () =>
                            await endpoint.ReportCommentAsync(539556821, ReportReason.Spam).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task VoteCommentAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/comment/539556821/vote/down";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockCommentEndpointResponses.VoteComment)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new CommentEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var reported = await endpoint.VoteCommentAsync(539556821, VoteOption.Down).ConfigureAwait(false);

            Assert.True(reported);
        }

        [Fact]
        public async Task VoteCommentAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new CommentEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.VoteCommentAsync(539556821, VoteOption.Down).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}