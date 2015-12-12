namespace Imgur.API.Enums
{
    /// <summary>
    ///     Determines if Imgur returns a Code, a PIN code, or an opaque Token.
    ///     The value of this parameter determines which OAuth 2.0 flow will be used and impacts the processing your
    ///     application will need to perform.
    /// </summary>
    public enum OAuth2ResponseType
    {
        /// <summary>
        ///     If you choose code, then you must immediately exchange the Code for a Token.
        /// </summary>
        Code,

        /// <summary>
        ///     If you chose token, then the AccessToken and RefreshToken will be given to you
        ///     in the form of query string parameters attached to your redirect URL, which the user may be able to read.
        /// </summary>
        Token,

        /// <summary>
        ///     If you chose pin, then the user will receive a PIN code that they will enter into your app to complete the
        ///     authorization process.
        /// </summary>
        Pin
    }
}