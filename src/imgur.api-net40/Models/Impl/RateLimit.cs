namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     Remaining credits for the application.
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
    }
}