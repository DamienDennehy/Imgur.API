using System;
using Imgur.API.JsonConverters;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     A notification of a new message.
    /// </summary>
    public class MessageNotification : IMessageNotification
    {
        /// <summary>
        ///     Account ID of the user in conversation.
        /// </summary>
        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        /// <summary>
        ///     Utc timestamp of last sent message, converted from epoch time.
        /// </summary>
        [JsonConverter(typeof (EpochTimeConverter))]
        public DateTimeOffset DateTime { get; set; }

        /// <summary>
        ///     The username of the other user in conversation.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        ///     Conversation ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The last message
        /// </summary>
        [JsonProperty("last_message")]
        public string LastMessage { get; set; }

        /// <summary>
        ///     Total number of messages in the conversation.
        /// </summary>
        [JsonProperty("message_num")]
        public int MessageNum { get; set; }

        /// <summary>
        ///     Account ID of the other user in conversation.
        /// </summary>
        [JsonProperty("with_account")]
        public int WithAccount { get; set; }
    }
}