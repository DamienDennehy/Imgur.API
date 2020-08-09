using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;
using Xunit;
using Xunit.Extensions.AssemblyFixture;

[assembly: TestFramework(AssemblyFixtureFramework.TypeName, AssemblyFixtureFramework.AssemblyName)]

namespace Imgur.API.Tests.Integration
{
    public class ApiClientAssemblyFixture : IDisposable
    {
        private bool _disposed;
        private string _refreshToken;
        private string _clientKey;
        private string _clientSecret;
        private IOAuth2Token _oAuth2Token;

        public ApiClientAssemblyFixture()
        {
            ValidateEnvironmentalVariables();
            GetAndSetOAuth2Token();
        }

        private void ValidateEnvironmentalVariables()
        {
            _clientKey = Environment.GetEnvironmentVariable("IMGUR_CLIENT_KEY");
            _clientSecret = Environment.GetEnvironmentVariable("IMGUR_CLIENT_SECRET");
            _refreshToken = Environment.GetEnvironmentVariable("IMGUR_REFRESH_TOKEN");

            if (string.IsNullOrWhiteSpace(_clientKey))
            {
                throw new ArgumentException("IMGUR_CLIENT_KEY Environment Variable is not set");
            }

            if (string.IsNullOrWhiteSpace(_clientSecret))
            {
                throw new ArgumentException("IMGUR_CLIENT_SECRET Environment Variable is not set");
            }

            if (string.IsNullOrWhiteSpace(_refreshToken))
            {
                throw new ArgumentException("IMGUR_REFRESH_TOKEN Environment Variable is not set");
            }
        }

        private void GetAndSetOAuth2Token()
        {
            var apiClient = GetApiClientWithKeyAndSecret();
            var oAuth2Endpoint = new OAuth2Endpoint(apiClient, new HttpClient());
            _oAuth2Token = Task.Run(() => oAuth2Endpoint.GetTokenAsync(_refreshToken)).Result;
        }

        public IOAuth2Token GetOAuth2Token()
        {
            return _oAuth2Token;
        }

        public ApiClient GetApiClientWithKey()
        {
            return new ApiClient(_clientKey);
        }

        public ApiClient GetApiClientWithKeyAndSecret()
        {
            return new ApiClient(_clientKey, _clientSecret);
        }

        public ApiClient GetApiClientWithKeyAndOAuthToken()
        {
            var apiClient = new ApiClient(_clientKey);
            apiClient.SetOAuth2Token(_oAuth2Token);
            return apiClient;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _oAuth2Token = null;
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
