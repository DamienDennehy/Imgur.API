namespace Imgur.API.Models
{
    /// <summary>
    /// Error Information.
    /// </summary>
    public interface IError
    {
        /// <summary>
        /// Code.
        /// </summary>
        int Code { get; }

        /// <summary>
        /// Friendly Message.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Type of error.
        /// </summary>
        string Type { get; }
    }
}
