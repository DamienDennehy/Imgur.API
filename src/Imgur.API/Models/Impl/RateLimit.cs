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
        ///     Total credits that can be allocated.
        /// </summary>
        public virtual int UserLimit { get; set; } = 500;

        /// <summary>
        ///     Total credits available.
        /// </summary>
        public virtual int UserRemaining { get; set; } = 500;

        /// <summary>
        ///     Utc timestamp for when the credits will be reset, converted from epoch time.
        /// </summary>
        [JsonConverter(typeof (EpochTimeToDateTimeOffset))]
        public virtual DateTimeOffset UserReset { get; set; } = DateTimeOffset.UtcNow.Date.AddDays(1);

        /// <summary>
        ///     Total credits that can be allocated for the application in a day.
        /// </summary>
        public virtual int ClientLimit { get; set; } = 12500;

        /// <summary>
        ///     Total credits remaining for the application in a day.
        /// </summary>
        public virtual int ClientRemaining { get; set; } = 12500;
    }
}