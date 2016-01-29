using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Tests.Mocks;
using Xunit;

// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.EndpointTests
{
    public class ConversationEndpointTests : TestBase
    {
        [Fact]
        public async Task BlockSenderAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/conversations/block/Bob";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockConversationEndpointResponses.BlockSender)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new ConversationEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var created = await endpoint.BlockSenderAsync("Bob").ConfigureAwait(false);

            Assert.True(created);
        }

        [Fact]
        public async Task BlockSenderAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.BlockSenderAsync("Recipient").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task BlockSenderAsync_WithRecipientNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new ConversationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.BlockSenderAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task CreateConversationAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/conversations/Bob";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockConversationEndpointResponses.CreateConversation)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new ConversationEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var created = await endpoint.CreateConversationAsync("Bob", "Hello World!").ConfigureAwait(false);

            Assert.True(created);
        }

        [Fact]
        public async Task CreateConversationAsync_WithBodyNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new ConversationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.CreateConversationAsync("Recipient", null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task CreateConversationAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.CreateConversationAsync("Recipient", "Body").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task CreateConversationAsync_WithRecipientNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new ConversationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.CreateConversationAsync(null, "Body").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task DeleteConversationAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/conversations/Bob";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockConversationEndpointResponses.DeleteConversation)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new ConversationEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var deleted = await endpoint.DeleteConversationAsync("Bob").ConfigureAwait(false);

            Assert.True(deleted);
        }

        [Fact]
        public async Task DeleteConversationAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new ConversationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.DeleteConversationAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task DeleteConversationAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.DeleteConversationAsync("id").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetConversationAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/conversations/1234/1/0";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockConversationEndpointResponses.GetConversation)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new ConversationEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var conversation = await endpoint.GetConversationAsync("1234").ConfigureAwait(false);

            Assert.NotNull(conversation);
            Assert.Equal(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1451010592),
                conversation.DateTime);
            Assert.Equal(false, conversation.Done);
            Assert.Equal(34361981, conversation.Id);
            Assert.Equal("Test 2", conversation.LastMessagePreview);
            Assert.Equal(3, conversation.Messages.Count());
            Assert.Equal(3, conversation.MessageCount);
            Assert.Equal(2, conversation.Page);
            Assert.Equal("Bob", conversation.WithAccount);
            Assert.Equal(1198054, conversation.WithAccountId);
            Assert.True(conversation.Messages.Any());
        }

        [Fact]
        public async Task GetConversationAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new ConversationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetConversationAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetConversationAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetConversationAsync("1234").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetConversationsAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/conversations";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockConversationEndpointResponses.GetConversations)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new ConversationEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var conversations = await endpoint.GetConversationsAsync().ConfigureAwait(false);

            Assert.NotNull(conversations);
            Assert.True(conversations.Any());
        }

        [Fact]
        public async Task GetConversationsAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetConversationsAsync().ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task ReportSenderAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/conversations/report/Bob";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockConversationEndpointResponses.ReportSender)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new ConversationEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var created = await endpoint.ReportSenderAsync("Bob").ConfigureAwait(false);

            Assert.True(created);
        }

        [Fact]
        public async Task ReportSenderAsync_WithOAuth2TokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new ConversationEndpoint(client);
            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.ReportSenderAsync("Username").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task ReportSenderAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new ConversationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.ReportSenderAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}