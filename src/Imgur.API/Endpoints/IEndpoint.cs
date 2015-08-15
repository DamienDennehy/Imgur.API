using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Endpoint definition.
    /// </summary>
    public interface IEndpoint
    {
        /// <summary>
        ///     Interface for all Api authentication types.
        /// </summary>
        IApiAuthentication ApiAuthentication { get; }

        /// <summary>
        ///     Gets the endpoint url based on the authentication type.
        /// </summary>
        string EndpointUrl { get; }

        /// <summary>
        ///     Switch from one authentication type to another.
        /// </summary>
        /// <param name="apiAuthentication"></param>
        void SwitchAuthentication(IApiAuthentication apiAuthentication);

        /// <summary>
        ///     Make requests to the endpoint.
        /// </summary>
        /// <param name="endpointUrl">The endpointUrl that should be called.</param>
        /// <param name="httpMethod">The HttpMethod that should be used.</param>
        /// <param name="content">The FormUrlEncodedContent that should be submitted.</param>
        /// <returns></returns>
        Task<IDataModel> MakeEndpointRequestAsync<T>(
            HttpMethod httpMethod,
            string endpointUrl,
            HttpContent content);
    }
}