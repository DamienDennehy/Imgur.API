using System;
using System.Threading.Tasks;
using Imgur.API.Exceptions;
using Imgur.API.Models;

namespace Imgur.API.Endpoints.Impl
{
    public partial class AccountEndpoint
    {
        private const string GetNotificationsUrl = "account/{0}/notifications?new={1}";

        /// <summary>
        ///     Returns all of the reply notifications for the user.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <param name="newNotifications">false for all notifications, true for only non-viewed notification. Default is true.</param>
        /// <returns></returns>
        public async Task<INotifications> GetNotificationsAsync(bool newNotifications = true)
        {
            throw new NotImplementedException();
            //if (ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetNotificationsUrl);
            //endpointUrl = string.Format(endpointUrl, "me", newNotifications);

            //return await MakeEndpointRequestAsync<Notifications>(HttpMethod.Get, endpointUrl);
        }
    }
}