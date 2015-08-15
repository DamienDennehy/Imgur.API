using System;

namespace Imgur.API.Models
{
    /// <summary>
    ///     Remaining credits for the application and user.
    /// </summary>
    public interface IRateLimit : IDataModel
    {
        /// <summary>
        ///     Total credits that can be allocated.
        /// </summary>
        int UserLimit { get; set; }

        /// <summary>
        ///     Total credits available.
        /// </summary>
        int UserRemaining { get; set; }

        /// <summary>
        ///     Utc timestamp for when the credits will be reset, converted from epoch time.
        /// </summary>
        DateTimeOffset UserReset { get; set; }

        /// <summary>
        ///     Total credits that can be allocated for the application in a day.
        /// </summary>
        int ClientLimit { get; set; }

        /// <summary>
        ///     Total credits remaining for the application in a day.
        /// </summary>
        int ClientRemaining { get; set; }
    }
}