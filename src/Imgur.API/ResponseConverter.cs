using System.Text.Json;
using Imgur.API.Models;

namespace Imgur.API.ResponseConverters
{
    internal class ResponseConverter
    {
        /// <summary>
        /// Parses the string response from the endpoint into an expected type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        internal T ConvertResponse<T>(string response) where T : IDataModel
        {
            ThrowImgurExceptionIfResponseIsNull(response);
            ThrowImgurExceptionIfResponseIsInvalid(response);
            ThrowImgurExceptionIfResponseContainsRawError(response);
            ThrowImgurExceptionIfResponseContainsError(response);
            ThrowImgurExceptionIfResponseContainsErrorMessage(response);
            ThrowImgurExceptionIfResponseNotSuccess(response);

            return GetResponse<T>(response);
        }

        /// <summary>
        /// Parses the string response from the endpoint into an OAuth2Token.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        internal IOAuth2Token ConvertOAuth2TokenResponse(string response)
        {
            ThrowImgurExceptionIfResponseIsNull(response);
            ThrowImgurExceptionIfResponseIsInvalid(response);
            ThrowImgurExceptionIfResponseContainsRawError(response);
            ThrowImgurExceptionIfResponseContainsError(response);
            ThrowImgurExceptionIfResponseContainsErrorMessage(response);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<OAuth2Token>(response, options);
        }

        internal void ThrowImgurExceptionIfResponseIsNull(string response)
        {
            //If no result is found, then we can't proceed.
            if (string.IsNullOrWhiteSpace(response))
            {
                throw new ImgurException($"The response from the endpoint is missing.");
            }
        }

        internal void ThrowImgurExceptionIfResponseIsInvalid(string response)
        {
            //If the result isn't a json response, then we can't proceed.
            if (!response.StartsWith("{"))
            {
                throw new ImgurException($"The response from the endpoint is invalid.");
            }
        }

        internal void ThrowImgurExceptionIfResponseContainsRawError(string response)
        {
            //If an error occured, throw an exception.
            if (response.StartsWith("{\"data\":\"An error occurred"))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var imgurError = JsonSerializer.Deserialize<Basic<string>>(response, options);
                throw new ImgurException(imgurError.Data);
            }
        }

        internal void ThrowImgurExceptionIfResponseContainsError(string response)
        {
            //If an error occured, throw an exception.
            if (response.StartsWith("{\"data\":{\"error\":")
                && !response.Contains("\"message\":"))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var imgurError = JsonSerializer.Deserialize<Basic<ImgurError>>(response, options);
                throw new ImgurException(imgurError.Data.Error);
            }
        }

        internal void ThrowImgurExceptionIfResponseContainsErrorMessage(string response)
        {
            //If an error occured, throw an exception.
            if (response.StartsWith("{\"data\":{\"error\":")
                && response.Contains("\"message\":"))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var imgurError = JsonSerializer.Deserialize<Basic<ImgurErrorDetail>>(response, options);
                throw new ImgurException(imgurError.Data.Error.Message);
            }
        }

        internal void ThrowImgurExceptionIfResponseNotSuccess(string response)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var objectResponse = JsonSerializer.Deserialize<Basic<object>>(response, options);

            if (!objectResponse.Success)
            {
                var errorResponse = JsonSerializer.Deserialize<Basic<ImgurError>>(response, options);
                throw new ImgurException(errorResponse.Data.Error);
            }
        }

        internal T GetResponse<T>(string response)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var basicResponse = JsonSerializer.Deserialize<Basic<T>>(response, options);

            return basicResponse.Data;
        }
    }
}
