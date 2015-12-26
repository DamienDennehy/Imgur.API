using System;
using System.Collections.Generic;

namespace Imgur.API.Models
{
    /// <summary>
    ///     A conversation.
    /// </summary>
    public interface IConversation : IDataModel
    {
        /// <summary>
        ///     Utc timestamp of last sent message, converted from epoch time.
        /// </summary>
        DateTimeOffset DateTime { get; set; }

        /// <summary>
        ///     OPTIONAL: (only available when requesting a specific conversation).
        ///     Flag to indicate whether you've reached the beginning of the thread.
        /// </summary>
        bool? Done { get; set; }

        /// <summary>
        ///     Conversation ID
        /// </summary>
        int Id { get; set; }

        /// <summary>
        ///     Preview of the last message
        /// </summary>
        string LastMessagePreview { get; set; }

        /// <summary>
        ///     Total number of messages in the conversation.
        /// </summary>
        int MessageCount { get; set; }

        /// <summary>
        ///     OPTIONAL: (only available when requesting a specific conversation).
        ///     Reverse sorted such that most recent message is at the end of the array.
        /// </summary>
        IEnumerable<IMessage> Messages { get; set; }

        /// <summary>
        ///     OPTIONAL: (only available when requesting a specific conversation)
        ///     Number of the next page.
        /// </summary>
        int? Page { get; set; }

        /// <summary>
        ///     Account ID of the other user in conversation.
        /// </summary>
        string WithAccount { get; set; }

        /// <summary>
        ///     Account ID of the other user in conversation.
        /// </summary>
        int WithAccountId { get; set; }
    }
}