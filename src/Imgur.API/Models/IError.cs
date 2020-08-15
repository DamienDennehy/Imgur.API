namespace Imgur.API.Models
{
    /// <summary>
    /// Error Information.
    /// </summary>
    public interface IError
    {
        /// <summary>
        /// Friendly Message.
        /// </summary>
        string Message { get; }
    }
}
