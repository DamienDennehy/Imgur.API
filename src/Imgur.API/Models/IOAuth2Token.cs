namespace Imgur.API.Models
{
    /// <summary>
    /// An OAuth2 Token used for actions against a user's account.
    /// </summary>
    public interface IOAuth2Token: IDataModel
    {
        /// <summary>
        /// The secret key used to access the user's data. 
        /// It can be thought of the user's password and username combined into one, and is used to access the user's account. It expires after 1 month.
        /// </summary>
        string AccessToken { get; }

        /// <summary>
        /// The lifetime of the token in seconds.
        /// </summary>
        int? ExpiresIn { get; }

        /// <summary>
        /// The kind of token that is being returned.
        /// </summary>
        string TokenType { get; }

        /// <summary>
        /// Used to generate an Access Token.
        /// </summary>
        string RefreshToken { get; }

        /// <summary>
        /// The account id.
        /// </summary>
        int? AccountId { get; }

        /// <summary>
        /// The account's username.
        /// </summary>
        string AccountUsername { get; }
    }
}
