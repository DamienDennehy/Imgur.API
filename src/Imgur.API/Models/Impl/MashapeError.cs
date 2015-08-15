namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     Represents errors returned after a Mashape Endpoint request.
    /// </summary>
    public class MashapeError : IMashapeError
    {
        /// <summary>
        ///     A description of the error.
        /// </summary>
        public string Message { get; set; }
    }
}