namespace Imgur.API.Models
{
    /// <summary>
    /// A response from the API.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Basic<T> : IBasic<T>
    {
        /// <summary>
        /// The data returned from the response.
        /// </summary>
        public virtual T Data { get; set; }

        /// <summary>
        /// HTTP Status Code.
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// Was the request successful.
        /// </summary>
        public virtual bool Success { get; set; }
    }
}
