namespace Imgur.API.Models
{
    /// <summary>
    ///     A user that has been blocked from messaging.
    /// </summary>
    public interface IBlockedUser : IDataModel
    {
        /// <summary>
        ///     The account id of the user.
        /// </summary>
        int BlockedId { get; set; }

        /// <summary>
        ///     The account username.
        /// </summary>
        string BlockedUrl { get; set; }
    }
}