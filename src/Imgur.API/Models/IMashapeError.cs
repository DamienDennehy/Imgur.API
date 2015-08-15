namespace Imgur.API.Models
{
    /// <summary>
    ///     Represents errors returned after an Endpoint request.
    /// </summary>
    public interface IMashapeError
    {
        /// <summary>
        ///     A description of the error.
        /// </summary>
        string Message { get; set; }
    }
}