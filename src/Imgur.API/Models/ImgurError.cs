using System.Text.Json.Serialization;

namespace Imgur.API.Models
{
    /// <summary>
    /// An error returned after an Endpoint request.
    /// </summary>
    internal class ImgurError : IImgurError
    {
        public virtual string Error { get; set; }

        public virtual string Method { get; set; }

        public virtual string Request { get; set; }
    }
}
