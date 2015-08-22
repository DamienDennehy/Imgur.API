using System;
using Imgur.API.JsonAttributes;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     Remaining credits for the application and user.
    /// </summary>
    public class RateLimit : IRateLimit
    {
        /// <summary>
        ///     Total credits that can be allocated for the application in a day.
        /// </summary>
        public virtual int ClientLimit { get; set; } = 12500;

        /// <summary>
        ///     Total credits remaining for the application in a day.
        /// </summary>
        public virtual int ClientRemaining { get; set; } = 12500;

        /// <summary>
        ///     Total uploads allowed for the application in a day.
        ///     Only populated when using Mashape Authentication.
        /// </summary>
        public virtual int? MashapeUploadsLimit { get; set; }

        /// <summary>
        ///     Total uploads remaining for the application in a day.
        ///     Only populated when using Mashape Authentication.
        /// </summary>
        public virtual int? MashapeUploadsRemaining { get; set; }

        /// <summary>
        ///     Total credits that can be allocated.
        ///     Not populated when using Mashape Authentication.
        /// </summary>
        public virtual int? UserLimit { get; set; }

        /// <summary>
        ///     Total credits available.
        ///     Not populated when using Mashape Authentication.
        /// </summary>
        public virtual int? UserRemaining { get; set; }

        /// <summary>
        ///     Utc timestamp for when the credits will be reset, converted from epoch time.
        ///     Not populated when using Mashape Authentication.
        /// </summary>
        [JsonConverter(typeof (EpochTimeToDateTimeOffset))]
        public virtual DateTimeOffset? UserReset { get; set; }
    }
}