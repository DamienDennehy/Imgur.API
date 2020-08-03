﻿namespace Imgur.API.Authentication
{
    /// <summary>
    /// The type of client that will be used for authentication.
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// The client_id obtained during application registration.
        /// </summary>
        string ClientId { get; }

        /// <summary>
        /// The client secret obtained during application registration.
        /// </summary>
        string ClientSecret { get; }

        /// <summary>
        /// The Base Api Address.
        /// https://api.imgur.com/3/ or https://imgur-apiv3.p.rapidapi.com/3/
        /// </summary>
        string BaseAddress { get; }
    }
}
