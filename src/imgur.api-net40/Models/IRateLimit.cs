namespace Imgur.API.Models
{
    /// <summary>
    ///     Remaining credits for the application.
    /// </summary>
    public interface IRateLimit : IDataModel
    {
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