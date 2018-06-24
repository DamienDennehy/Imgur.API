namespace Imgur.API.Models
{
    /// <summary>
    ///     An error returned after an Endpoint request.
    /// </summary>
    public class MashapeError : IMashapeError
    {
        /// <summary>
        ///     A description of the error.
        /// </summary>
        public virtual string Message { get; set; }
    }
}