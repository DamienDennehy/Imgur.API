using Imgur.API.Authentication;
using Imgur.API.Models;
using Imgur.API.RequestBuilders;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Imgur.API.Endpoints;

/// <summary>
/// Album Endpoint.
/// </summary>
public class AlbumEndpoint : EndpointBase
{
    /// <summary>
    /// Declares a new instance of the endpoint.
    /// </summary>
    /// <param name="apiClient"></param>
    /// <param name="httpClient"></param>
    public AlbumEndpoint(IApiClient apiClient, HttpClient httpClient) : base(
        apiClient, httpClient)
    {
    }

    /// <summary>
    /// Get the list of album images
    /// </summary>
    /// <param name="albumId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IImage[]> GetImagesAsync(string albumId, CancellationToken cancellationToken = default)
    {
        using var request = RequestBuilder.CreateRequest(HttpMethod.Get, $"album/{albumId}/images");
        var response = await SendRequestAsync<Image[]>(request, cancellationToken);
        return response;
    }
}
