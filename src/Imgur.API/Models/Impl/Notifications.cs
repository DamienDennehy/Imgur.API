using System.Collections.Generic;
using Imgur.API.JsonConverters;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     Account notifications.
    /// </summary>
    public class Notifications : INotifications
    {
        /// <summary>
        ///     A list of message notifications.
        /// </summary>
        [JsonConverter(typeof(TypeConverter<IEnumerable<Notification>>))]
        public virtual IEnumerable<INotification> Messages { get; set; } = new List<INotification>();

        /// <summary>
        ///     A list of comment notifications.
        /// </summary>
        [JsonConverter(typeof(TypeConverter<IEnumerable<Notification>>))]
        public virtual IEnumerable<INotification> Replies { get; set; } = new List<INotification>();
    }
}