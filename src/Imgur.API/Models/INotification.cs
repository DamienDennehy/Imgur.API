namespace Imgur.API.Models
{
    /// <summary>
    ///     The base model for a notification.
    /// </summary>
    public interface INotification<T>
    {
        /// <summary>
        ///     The ID for the notification
        /// </summary>
        int Id { get; set; }

        /// <summary>
        ///     The Account ID for the notification
        /// </summary>
        int AccountId { get; set; }

        /// <summary>
        ///     Has the user viewed the image yet?
        /// </summary>
        bool Viewed { get; set; }

        /// <summary>
        ///     This can be any other model.
        /// </summary>
        T Content { get; set; }
    }
}