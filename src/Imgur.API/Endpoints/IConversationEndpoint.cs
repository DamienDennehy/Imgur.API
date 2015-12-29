using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Conversation related actions.
    /// </summary>
    public interface IConversationEndpoint
    {
        /// <summary>
        ///     Block the user from sending messages to the user that is logged in.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="username">The sender that should be blocked.</param>
        Task<bool> BlockSenderAsync(string username);

        /// <summary>
        ///     Create a new message. OAuth authentication required.
        /// </summary>
        /// <param name="recipient">The recipient username, this person will receive the message.</param>
        /// <param name="body">The message itself, similar to the body of an email.</param>
        Task<bool> CreateConversationAsync(string recipient, string body);

        /// <summary>
        ///     Delete a conversation by the given id. OAuth authentication required.
        /// </summary>
        /// <param name="conversationId">The conversation id.</param>
        Task<bool> DeleteConversationAsync(string conversationId);

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
        Task<IConversation> GetConversationAsync(string conversationId, int? page = null, int? offset = null);

        /// <summary>
        ///     Get list of all conversations for the logged in user.
        ///     OAuth authentication required.
        /// </summary>
        Task<IEnumerable<IConversation>> GetConversationsAsync();

        /// <summary>
        ///     Report a user for sending messages that are against the Terms of Service.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="username">The sender that should be reported.</param>
        Task<bool> ReportSenderAsync(string username);
    }
}