using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Exceptions;
using Imgur.API.Models;
using Imgur.API.Models.Impl;

namespace Imgur.API.Endpoints.Impl
{
    public partial class AccountEndpoint
    {
        /// <summary>
        ///     Returns all of the reply notifications for the user.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <param name="newNotifications">false for all notifications, true for only non-viewed notification. Default is true.</param>
        /// <returns></returns>
        public async Task<INotifications> GetNotificationsAsync(bool newNotifications = true)
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"{GetEndpointBaseUrl()}account/me/notifications?new={newNotifications.ToString().ToLower()}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var notifications = await SendRequestAsync<Notifications>(request);
                return notifications;
            }
        }
    }
}