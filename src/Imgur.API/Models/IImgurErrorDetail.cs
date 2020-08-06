namespace Imgur.API.Models
{
    /// <summary>
    /// An error returned after an Endpoint request.
    /// </summary>
    public interface IImgurErrorDetail : IDataModel
    {
        /// <summary>
        /// A description of the error.
        /// </summary>
        Error Error { get; }
    }
}
