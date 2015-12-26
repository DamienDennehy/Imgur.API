﻿using Imgur.API.JsonConverters;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     A notification.
    /// </summary>
    public class Notification : INotification
    {
        /// <summary>
        ///     The Account ID for the notification
        /// </summary>
        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        /// <summary>
        ///     This can be any other model.
        /// </summary>
        [JsonConverter(typeof (NotificationConverter))]
        public IDataModel Content { get; set; }

        /// <summary>
        ///     The ID for the notification
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Has the user viewed the notification yet?
        /// </summary>
        public bool Viewed { get; set; }
    }
}