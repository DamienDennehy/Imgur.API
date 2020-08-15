namespace Imgur.API.Models
{
    /// <summary>
    /// An error returned after an Endpoint request.
    /// </summary>
    public class ImgurErrorDetail : IImgurErrorDetail
    {
        /// <summary>
        /// A description of the error.
        /// </summary>
        public Error Error { get; set; }
    }
}
