namespace Imgur.API.Models
{
    /// <summary>
    ///     Represents errors returned after a Imgur Endpoint request.
    /// </summary>
    public interface IImgurError
    {
        /// <summary>
        ///     A description of the error.
        /// </summary>
        string ErrorMessage { get; set; }

        /// <summary>
        ///     The request Uri that the error came from.
        /// </summary>
        string Request { get; set; }

        /// <summary>
        ///     The HttpMethod that was used to send the request.
        /// </summary>
        string Method { get; set; }
    }
}