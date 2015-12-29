using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Notification related actions.
    /// </summary>
    public interface INotificationEndpoint
    {
        /// <summary>
        ///     Returns the data about a specific notification.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="notificationId">The notification id.</param>
        /// <returns></returns>
        Task<INotification> GetNotificationAsync(string notificationId);

        /// <summary>
        ///     Returns all of the notifications for the user.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="newNotifications">false for all notifications, true for only non-viewed notification. Default is true.</param>
        /// <returns></returns>
        Task<INotifications> GetNotificationsAsync(bool newNotifications = true);

        /// <summary>
        ///     Marks notifications as viewed.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="ids">The notification id.</param>
        /// <returns></returns>
        Task<bool> MarkNotificationsViewedAsync(IEnumerable<string> ids);

        /// <summary>
        ///     Marks a notification as viewed.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="notificationId">The notification id.</param>
        /// <returns></returns>
        Task<bool> MarkNotificationViewedAsync(string notificationId);
    }
}