namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     An error returned after an Endpoint request.
    /// </summary>
    public class MashapeError : IMashapeError
    {
        /// <summary>
        ///     A description of the error.
        /// </summary>
        public string Message { get; set; }
    }
}