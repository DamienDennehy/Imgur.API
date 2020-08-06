namespace Imgur.API.Models
{
    /// <summary>
    /// An error returned after an Endpoint request.
    /// </summary>
    public class ImgurErrorDetail : IImgurErrorDetail
    {
        public Error Error { get; set; }

        public string Method { get; set; }

        public string Request { get; set; }
    }
}
