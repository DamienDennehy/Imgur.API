using System;
using Imgur.API.JsonConverters;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     The base model for a message from account notifications.
    /// </summary>
    public class Message : IMessage
    {
        /// <summary>
        ///     Conversation ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Account ID of the user in conversation.
        /// </summary>
        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        /// <summary>
        ///     Account ID of the other user in conversation.
        /// </summary>
        [JsonProperty("with_account")]
        public int WithAccountId { get; set; }

        /// <summary>
        ///     Total number of messages in the conversation.
        /// </summary>
        [JsonProperty("message_num")]
        public int MessageNum { get; set; }

        /// <summary>
        ///     The last message
        /// </summary>
        [JsonProperty("last_message")]
        public string LastMessage { get; set; }

        /// <summary>
        ///     The username of the other user in conversation.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        ///     Utc timestamp of last sent message, converted from epoch time.
        /// </summary>
        [JsonProperty("datetime")]
        [JsonConverter(typeof (EpochTimeConverter))]
        public DateTimeOffset DateTime { get; set; }
    }
}