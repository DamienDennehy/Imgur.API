namespace Imgur.API.Models
{
    /// <summary>
    ///     A notification.
    /// </summary>
    public interface INotification : IDataModel
    {
        /// <summary>
        ///     The Account ID for the notification
        /// </summary>
        int AccountId { get; set; }

        /// <summary>
        ///     This can be any other model.
        /// </summary>
        IDataModel Content { get; set; }

        /// <summary>
        ///     The ID for the notification
        /// </summary>
        int Id { get; set; }

        /// <summary>
        ///     Has the user viewed the notification yet?
        /// </summary>
        bool Viewed { get; set; }
    }
}