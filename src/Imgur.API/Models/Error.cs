namespace Imgur.API.Models
{
    /// <summary>
    /// Error Information.
    /// </summary>
    public class Error : IError
    {
        /// <summary>
        /// Friendly Message.
        /// </summary>
        public virtual string Message { get; set; }
    }
}
