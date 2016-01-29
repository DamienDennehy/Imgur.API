namespace Imgur.API.Tests.Mocks
{
    public class MockOAuth2EndpointResponses
    {
        public const string GetTokenByCode =
            "{\"access_token\":\"CodeResponse\",\"expires_in\":2419200,\"token_type\":\"bearer\",\"scope\":null,\"refresh_token\":\"2132d34234jkljj84ce0c16fjkljfsdfdc70\",\"account_id\":45344,\"account_username\":\"Bob\"}";

        public const string GetTokenByPin =
            "{\"access_token\":\"PinResponse\",\"expires_in\":2419200,\"token_type\":\"bearer\",\"scope\":null,\"refresh_token\":\"2132d34234jkljj84ce0c16fjkljfsdfdc70\",\"account_id\":45344,\"account_username\":\"Bob\"}";

        public const string GetTokenByRefreshToken =
            "{\"access_token\":\"RefreshTokenResponse\",\"expires_in\":2419200,\"token_type\":\"bearer\",\"scope\":null,\"refresh_token\":\"2132d34234jkljj84ce0c16fjkljfsdfdc70\",\"account_id\":45344,\"account_username\":\"Bob\"}";

        public const string OAuth2TokenResponseError =
            "{\"data\":{\"error\":\"Refresh token doesn't exist or is invalid for the client\",\"request\":\"\\/oauth2\\/token\",\"method\":\"POST\"},\"success\":false,\"status\":400}";
    }
}