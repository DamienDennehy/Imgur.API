using System.Collections.Generic;

namespace Imgur.API.Models
{
    /// <summary>
    ///     The base model for notifications.
    /// </summary>
    public interface INotifications : IDataModel
    {
        /// <summary>
        ///     An array of message notifications.
        /// </summary>
        IEnumerable<INotification> Messages { get; set; }

        /// <summary>
        ///     An array of comment notifications.
        /// </summary>
        IEnumerable<INotification> Replies { get; set; }
    }
}