using System.Net;

namespace Imgur.API.Models
{
    /// <summary>
    ///     A response from the API.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBasic<T>
    {
        /// <summary>
        ///     The data returned from the response.
        /// </summary>
        T Data { get; set; }

        /// <summary>
        ///     HTTP Status Code.
        /// </summary>
        HttpStatusCode Status { get; set; }

        /// <summary>
        ///     Was the request successful.
        /// </summary>
        bool Success { get; set; }
    }
}