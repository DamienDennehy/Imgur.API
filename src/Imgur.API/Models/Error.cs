namespace Imgur.API.Models
{
    /// <summary>
    /// Error Information.
    /// </summary>
    public class Error : IError
    {
        public virtual string Message { get; set; }
    }
}
