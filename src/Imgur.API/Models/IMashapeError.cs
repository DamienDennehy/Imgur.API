namespace Imgur.API.Models
{
    /// <summary>
    ///     An error returned after an Endpoint request.
    /// </summary>
    public interface IMashapeError
    {
        /// <summary>
        ///     A description of the error.
        /// </summary>
        string Message { get; set; }
    }
}