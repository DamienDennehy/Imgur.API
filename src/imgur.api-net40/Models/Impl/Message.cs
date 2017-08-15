using System;
using Imgur.API.JsonConverters;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     A message from another user.
    /// </summary>
    public class Message : IMessage
    {
        /// <summary>
        ///     The account ID of the person receiving the message.
        /// </summary>
        [JsonProperty("account_id")]
        public virtual int AccountId { get; set; }

        /// <summary>
        ///     Text of the message.
        /// </summary>
        public virtual string Body { get; set; }

        /// <summary>
        ///     ID for the overall conversation.
        /// </summary>
        [JsonProperty("conversation_id")]
        public virtual int ConversationId { get; set; }

        /// <summary>
        ///     Time message was sent, epoch time.
        /// </summary>
        [JsonConverter(typeof(EpochTimeConverter))]
        public virtual DateTimeOffset DateTime { get; set; }

        /// <summary>
        ///     Account username of person sending the message.
        /// </summary>
        public virtual string From { get; set; }

        /// <summary>
        ///     The ID for the message.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///     The account ID of the person who sent the message.
        /// </summary>
        [JsonProperty("sender_id")]
        public virtual int SenderId { get; set; }
    }
}