using System.Net;

namespace Imgur.API.Models
{
    /// <summary>
    /// A response from the API.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBasic<out T>
    {
        /// <summary>
        /// The data returned from the response.
        /// </summary>
        T Data { get; }

        /// <summary>
        /// HTTP Status Code.
        /// </summary>
        int Status { get; }

        /// <summary>
        /// Was the request successful.
        /// </summary>
        bool Success { get; }
    }
}
