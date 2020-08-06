namespace Imgur.API.Models
{
    /// <summary>
    /// An error returned after an Endpoint request.
    /// </summary>
    public interface IImgurError: IDataModel
    {
        /// <summary>
        /// A description of the error.
        /// </summary>
        string Error { get; }

        /// <summary>
        /// The HttpMethod that was used to send the request.
        /// </summary>
        string Method { get; }

        /// <summary>
        /// The request Uri that the error came from.
        /// </summary>
        string Request { get; }
    }
}
