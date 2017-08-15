using System;

namespace Imgur.API.Models
{
    /// <summary>
    ///     A message from another user.
    /// </summary>
    public interface IMessage : IDataModel
    {
        /// <summary>
        ///     The account ID of the person receiving the message.
        /// </summary>
        int AccountId { get; set; }

        /// <summary>
        ///     Text of the message.
        /// </summary>
        string Body { get; set; }

        /// <summary>
        ///     ID for the overall conversation.
        /// </summary>
        int ConversationId { get; set; }

        /// <summary>
        ///     Time message was sent, epoch time.
        /// </summary>
        DateTimeOffset DateTime { get; set; }

        /// <summary>
        ///     Account username of person sending the message.
        /// </summary>
        string From { get; set; }

        /// <summary>
        ///     The ID for the message.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        ///     The account ID of the person who sent the message.
        /// </summary>
        int SenderId { get; set; }
    }
}