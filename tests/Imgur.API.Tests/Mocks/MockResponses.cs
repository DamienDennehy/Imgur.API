namespace Imgur.API.Tests.Mocks
{
    public static class MockResponses
    {
        public const string SuccessfulResponse = 
            "{\"data\":true,\"success\":true,\"status\":200}";

        public const string RawError =
            "{\"data\":\"An error occurred\",\"success\":false,\"status\":500}";

        public const string Error =
            "{\"data\":{\"error\":\"Imgur is over capacity. Please try again.\", \"request\":\"/image/MwJTXbW\", \"method\":\"POST\" }, \"success\":false, \"status\":1203 }";

        public const string ErrorMessage =
            "{\"data\":{\"error\":{\"code\":500,\"message\":\"Could not complete request\",\"type\":\"ImgurException\",\"exception\":{}},\"request\":\"/3/account/sarah/gallery_profile\",\"method\":\"GET\"},\"success\":false,\"status\":500}";

        public const string FailureError =
            "{\"data\":false,\"success\":false,\"status\":400}";

        public const string CacheError =
            "\r\n" +
            " <xml";
    }
}
