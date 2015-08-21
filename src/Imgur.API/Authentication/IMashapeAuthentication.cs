namespace Imgur.API.Authentication
{
    /// <summary>
    ///     Mashape API application credentials.
    ///     Register at https://www.mashape.com/imgur/imgur-9
    /// </summary>
    public interface IMashapeAuthentication : IApiAuthentication
    {
        /// <summary>
        ///     The Mashape Key.
        /// </summary>
        string MashapeKey { get; }
    }
}