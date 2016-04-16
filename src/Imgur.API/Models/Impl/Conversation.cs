using System;
using System.Collections.Generic;
using Imgur.API.JsonConverters;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     A conversation.
    /// </summary>
    public class Conversation : IConversation
    {
        /// <summary>
        ///     Utc timestamp of last sent message, converted from epoch time.
        /// </summary>
        [JsonConverter(typeof(EpochTimeConverter))]
        public virtual DateTimeOffset DateTime { get; set; }

        /// <summary>
        ///     OPTIONAL: (only available when requesting a specific conversation).
        ///     Flag to indicate whether you've reached the beginning of the thread.
        /// </summary>
        public virtual bool? Done { get; set; }

        /// <summary>
        ///     Conversation ID
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///     Preview of the last message
        /// </summary>
        [JsonProperty("last_message_preview")]
        public virtual string LastMessagePreview { get; set; }

        /// <summary>
        ///     Total number of messages in the conversation.
        /// </summary>
        [JsonProperty("message_count")]
        public virtual int MessageCount { get; set; }

        /// <summary>
        ///     OPTIONAL: (only available when requesting a specific conversation).
        ///     Reverse sorted such that most recent message is at the end of the array.
        /// </summary>
        [JsonConverter(typeof(TypeConverter<IEnumerable<Message>>))]
        public virtual IEnumerable<IMessage> Messages { get; set; } = new List<IMessage>();

        /// <summary>
        ///     OPTIONAL: (only available when requesting a specific conversation)
        ///     Number of the next page.
        /// </summary>
        public virtual int? Page { get; set; }

        /// <summary>
        ///     Account username of the other user in conversation.
        /// </summary>
        [JsonProperty("with_account")]
        public virtual string WithAccount { get; set; }

        /// <summary>
        ///     Account ID of the other user in conversation.
        /// </summary>
        [JsonProperty("with_account_id")]
        public virtual int WithAccountId { get; set; }
    }
}