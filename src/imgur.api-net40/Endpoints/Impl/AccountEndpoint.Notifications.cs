﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Models;
using Imgur.API.Models.Impl;

namespace Imgur.API.Endpoints.Impl
{
    public partial class AccountEndpoint
    {
        /// <summary>
        ///     Returns all of the reply notifications for the user.
        ///     OAuth authentication required.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <param name="newNotifications">false for all notifications, true for only non-viewed notification. Default is true.</param>
        /// <returns></returns>
        public Basic<Notifications> GetNotifications(bool newNotifications = true)
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var newNotificationsValue = $"{newNotifications}".ToLower();
            var url = $"account/me/notifications?new={newNotificationsValue}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var httpResponse = HttpClient.SendAsync(request).Result;
                var jsonString = httpResponse.Content.ReadAsStringAsync().Result;
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<Basic<Notifications>>(httpResponse.Content.ReadAsStringAsync().Result.ToString());
                return output;
            }
        }
    }
}