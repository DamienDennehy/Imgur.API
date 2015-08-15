namespace Imgur.API.Authentication
{
    /// <summary>
    ///     Imgur API application credentials.
    ///     Register at https://api.imgur.com/oauth2/addclient
    /// </summary>
    public interface IImgurAuthentication : IApiAuthentication
    {
        /// <summary>
        ///     The Imgur app's ClientId.
        /// </summary>
        string ClientId { get; }

        /// <summary>
        ///     The Imgur app's ClientSecret.
        /// </summary>
        string ClientSecret { get; }
    }
}