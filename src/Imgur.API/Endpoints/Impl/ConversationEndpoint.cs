using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Conversation related actions.
    /// </summary>
    public class ConversationEndpoint : EndpointBase, IConversationEndpoint
    {
        /// <summary>
        ///     Initializes a new instance of the ConversationEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        public ConversationEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the ConversationEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <param name="httpClient"> The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.</param>
        internal ConversationEndpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }

        internal ConversationRequestBuilder RequestBuilder { get; } = new ConversationRequestBuilder();

        /// <summary>
        ///     Block the user from sending messages to the user that is logged in.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="username">The sender that should be blocked.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> BlockSenderAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"conversations/block/{username}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Post, url))
            {
                var blocked = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return blocked;
            }
        }

        /// <summary>
        ///     Create a new message. OAuth authentication required.
        /// </summary>
        /// <param name="recipient">The recipient username, this person will receive the message.</param>
        /// <param name="body">The message itself, similar to the body of an email.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> CreateConversationAsync(string recipient, string body)
        {
            if (string.IsNullOrWhiteSpace(recipient))
                throw new ArgumentNullException(nameof(recipient));

            if (string.IsNullOrWhiteSpace(body))
                throw new ArgumentNullException(nameof(body));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"conversations/{recipient}";

            using (var request = RequestBuilder.CreateMessageRequest(url, body))
            {
                var added = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return added;
            }
        }

        /// <summary>
        ///     Delete a conversation by the given id. OAuth authentication required.
        /// </summary>
        /// <param name="conversationId">The conversation id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> DeleteConversationAsync(string conversationId)
        {
            if (string.IsNullOrWhiteSpace(conversationId))
                throw new ArgumentNullException(nameof(conversationId));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"conversations/{conversationId}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Delete, url))
            {
                var deleted = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return deleted;
            }
        }

        /// <summary>
        ///     Get information about a specific conversation. Includes messages.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="conversationId">The conversation id.</param>
        /// <param name="page">
        ///     Page of message thread. Starting at 1 for the most recent 25 messages and counting upwards. Default:
        ///     null
        /// </param>
        /// <param name="offset">Additional offset in current page.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IConversation> GetConversationAsync(string conversationId, int? page = null,
            int? offset = null)
        {
            if (string.IsNullOrWhiteSpace(conversationId))
                throw new ArgumentNullException(nameof(conversationId));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"conversations/{conversationId}/{page ?? 1}/{offset ?? 0}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var conversation = await SendRequestAsync<Conversation>(request).ConfigureAwait(false);
                return conversation;
            }
        }

        /// <summary>
        ///     Get list of all conversations for the logged in user.
        ///     OAuth authentication required.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<IConversation>> GetConversationsAsync()
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = "conversations";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var conversations = await SendRequestAsync<IEnumerable<Conversation>>(request).ConfigureAwait(false);
                return conversations;
            }
        }

        /// <summary>
        ///     Report a user for sending messages that are against the Terms of Service.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="username">The sender that should be reported.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> ReportSenderAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"conversations/report/{username}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Post, url))
            {
                var reported = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return reported;
            }
        }
    }
}