using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Notification related actions.
    /// </summary>
    public class NotificationEndpoint : EndpointBase, INotificationEndpoint
    {
        /// <summary>
        ///     Initializes a new instance of the NotificationsEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        public NotificationEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the NotificationsEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <param name="httpClient"> The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.</param>
        internal NotificationEndpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }

        internal NotificationRequestBuilder RequestBuilder { get; } = new NotificationRequestBuilder();

        /// <summary>
        ///     Returns the data about a specific notification.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="notificationId">The notification id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<INotification> GetNotificationAsync(string notificationId)
        {
            if (string.IsNullOrWhiteSpace(notificationId))
                throw new ArgumentNullException(nameof(notificationId));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"notification/{notificationId}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var notification = await SendRequestAsync<Notification>(request).ConfigureAwait(false);
                return notification;
            }
        }

        /// <summary>
        ///     Returns all of the notifications for the user.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="newNotifications">false for all notifications, true for only non-viewed notification. Default is true.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<INotifications> GetNotificationsAsync(bool newNotifications = true)
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var newNotificationsValue = $"{newNotifications}".ToLower();
            var url = $"notification?new={newNotificationsValue}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var notifications = await SendRequestAsync<Notifications>(request).ConfigureAwait(false);
                return notifications;
            }
        }

        /// <summary>
        ///     Marks notifications as viewed.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="ids">The notification id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> MarkNotificationsViewedAsync(IEnumerable<string> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = "notification";

            using (var request = NotificationRequestBuilder.MarkNotificationsViewedRequest(url, ids))
            {
                var viewed = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return viewed;
            }
        }

        /// <summary>
        ///     Marks a notification as viewed.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="notificationId">The notification id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> MarkNotificationViewedAsync(string notificationId)
        {
            if (string.IsNullOrWhiteSpace(notificationId))
                throw new ArgumentNullException(nameof(notificationId));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"notification/{notificationId}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Post, url))
            {
                var viewed = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return viewed;
            }
        }
    }
}