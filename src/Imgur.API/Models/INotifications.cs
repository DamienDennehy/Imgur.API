using System.Collections.Generic;

namespace Imgur.API.Models
{
    /// <summary>
    ///     Account notifications.
    /// </summary>
    public interface INotifications : IDataModel
    {
        /// <summary>
        ///     A list of message notifications.
        /// </summary>
        IEnumerable<INotification> Messages { get; set; }

        /// <summary>
        ///     A list of comment notifications.
        /// </summary>
        IEnumerable<INotification> Replies { get; set; }
    }
}