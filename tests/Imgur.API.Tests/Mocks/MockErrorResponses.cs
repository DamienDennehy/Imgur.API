namespace Imgur.API.Tests.Mocks
{
    public static class MockErrorResponses
    {
        public const string ImgurRawError =
            "{\"data\":\"An error occurred\",\"success\":false,\"status\":500}";

        public const string ImgurError =
            "{\"data\":{\"error\":\"Imgur is over capacity. Please try again.\", \"request\":\"/image/MwJTXbW\", \"method\":\"POST\" }, \"success\":false, \"status\":1203 }";

        public const string ImgurErrorMessage =
            "{\"data\":{\"error\":{\"code\":500,\"message\":\"Could not complete request\",\"type\":\"ImgurException\",\"exception\":{}},\"request\":\"/3/account/sarah/gallery_profile\",\"method\":\"GET\"},\"success\":false,\"status\":500}";

        public const string ImgurCacheError =
            "\r\n" +
            " <xml";
    }
}
