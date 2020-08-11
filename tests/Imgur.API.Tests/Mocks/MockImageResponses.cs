namespace Imgur.API.Tests.Mocks
{
    public static class MockImageResponses
    {
        public const string GetImage = 
            "{\"data\":{\"id\":\"mvWNMH4\",\"title\":\"Epic Fail\",\"description\":\"That's got to hurt\",\"datetime\":1596483255,\"type\":\"video\\/mp4\",\"animated\":true,\"width\":854,\"height\":482,\"size\":7701069,\"views\":915460,\"bandwidth\":7050020626740,\"vote\":\"ups\",\"favorite\":true,\"nsfw\":true,\"section\":\"viral\",\"account_url\":\"https://imgur.com/user/Sarah\",\"account_id\":12345679,\"is_ad\":true,\"in_most_viral\":true,\"has_sound\":true,\"tags\":[\"funny\"],\"ad_type\":1,\"ad_url\":\"http://imgur.com\",\"edited\":\"1\",\"in_gallery\":true,\"deletehash\": \"ABCDEFGH1234\",\"name\": \"ouch.mp4\",\"link\":\"https:\\/\\/i.imgur.com\\/mvWNMH4.mp4\",\"mp4_size\":7701069,\"mp4\":\"https:\\/\\/i.imgur.com\\/mvWNMH4.mp4\",\"gifv\":\"https:\\/\\/i.imgur.com\\/mvWNMH4.gifv\",\"hls\":\"https:\\/\\/i.imgur.com\\/mvWNMH4.m3u8\",\"processing\":{\"status\":\"completed\"},\"ad_config\":{\"safeFlags\":[\"not_in_gallery\",\"sixth_mod_safe\",\"share\",\"page_load\"],\"highRiskFlags\":[],\"unsafeFlags\":[],\"wallUnsafeFlags\":[],\"showsAds\":true}},\"success\":true,\"status\":200}";

        public const string GetImagePublc =
            "{\"data\":{\"id\":\"PdvlRWc\",\"title\":null,\"description\":\"After years of playing around with making sauces while jumping from bad to worse job, I\u2019m gonna take a chance at doing something I love for a living. Hoping this gives me more time at home with my family. At the very least no more exhaustion from boring and backbreaking work, so I\u2019ll have that going for me which is nice. Edit: Link by popular demand www.whitewhalesauces.nl Edit2: Thank all of you guys soooo much for all the support and feedback. I\u2019m overwhelmed and taking all of your opinions to heart!\",\"datetime\":1596966065,\"type\":\"image\\/jpeg\",\"animated\":false,\"width\":1600,\"height\":1312,\"size\":150117,\"views\":20625,\"bandwidth\":3096163125,\"vote\":null,\"favorite\":false,\"nsfw\":false,\"section\":null,\"account_url\":null,\"account_id\":null,\"is_ad\":false,\"in_most_viral\":false,\"has_sound\":false,\"tags\":[],\"ad_type\":0,\"ad_url\":\"\",\"edited\":\"0\",\"in_gallery\":false,\"link\":\"https:\\/\\/i.imgur.com\\/PdvlRWc.jpg\",\"ad_config\":{\"safeFlags\":[\"not_in_gallery\",\"share\"],\"highRiskFlags\":[],\"unsafeFlags\":[\"sixth_mod_unsafe\"],\"wallUnsafeFlags\":[],\"showsAds\":false}},\"success\":true,\"status\":200}";

        public const string UploadImage =
            "{\"data\":{\"id\":\"mvWNMH4\",\"title\":\"Epic Fail\",\"description\":\"That's got to hurt\",\"datetime\":1596483255,\"type\":\"video\\/mp4\",\"animated\":true,\"width\":854,\"height\":482,\"size\":7701069,\"views\":915460,\"bandwidth\":7050020626740,\"vote\":\"ups\",\"favorite\":true,\"nsfw\":true,\"section\":\"viral\",\"account_url\":\"https://imgur.com/user/Sarah\",\"account_id\":12345679,\"is_ad\":true,\"in_most_viral\":true,\"has_sound\":true,\"tags\":[\"funny\"],\"ad_type\":1,\"ad_url\":\"http://imgur.com\",\"edited\":\"1\",\"in_gallery\":true,\"deletehash\": \"ABCDEFGH1234\",\"name\": \"ouch.mp4\",\"link\":\"https:\\/\\/i.imgur.com\\/mvWNMH4.mp4\",\"mp4_size\":7701069,\"mp4\":\"https:\\/\\/i.imgur.com\\/mvWNMH4.mp4\",\"gifv\":\"https:\\/\\/i.imgur.com\\/mvWNMH4.gifv\",\"hls\":\"https:\\/\\/i.imgur.com\\/mvWNMH4.m3u8\",\"processing\":{\"status\":\"completed\"},\"ad_config\":{\"safeFlags\":[\"not_in_gallery\",\"sixth_mod_safe\",\"share\",\"page_load\"],\"highRiskFlags\":[],\"unsafeFlags\":[],\"wallUnsafeFlags\":[],\"showsAds\":true}},\"success\":true,\"status\":200}";

        public const string UploadImageWithNullValues =
            "{\"data\":{\"id\":\"mvWNMH4\",\"title\":\"Epic Fail\",\"description\":\"That's got to hurt\",\"datetime\":1596483255,\"type\":\"video\\/mp4\",\"animated\":true,\"width\":854,\"height\":482,\"size\":7701069,\"views\":915460,\"bandwidth\":7050020626740,\"vote\":\"ups\",\"favorite\":true,\"nsfw\":null,\"section\":\"viral\",\"account_url\":\"https://imgur.com/user/Sarah\",\"account_id\":12345679,\"is_ad\":true,\"in_most_viral\":true,\"has_sound\":true,\"tags\":[\"funny\"],\"ad_type\":null,\"ad_url\":\"http://imgur.com\",\"edited\":\"1\",\"in_gallery\":true,\"deletehash\": \"ABCDEFGH1234\",\"name\": \"ouch.mp4\",\"link\":\"https:\\/\\/i.imgur.com\\/mvWNMH4.mp4\",\"mp4_size\":7701069,\"mp4\":\"https:\\/\\/i.imgur.com\\/mvWNMH4.mp4\",\"gifv\":\"https:\\/\\/i.imgur.com\\/mvWNMH4.gifv\",\"hls\":\"https:\\/\\/i.imgur.com\\/mvWNMH4.m3u8\",\"processing\":{\"status\":\"completed\"},\"ad_config\":{\"safeFlags\":[\"not_in_gallery\",\"sixth_mod_safe\",\"share\",\"page_load\"],\"highRiskFlags\":[],\"unsafeFlags\":[],\"wallUnsafeFlags\":[],\"showsAds\":true}},\"success\":true,\"status\":200}";

        public const string FavoriteImage =
            "{\"data\":\"favorited\",\"success\":true,\"status\":200}";

        public const string DeleteImage =
            "{\"data\":true,\"success\":true,\"status\":200}";

        public const string UpdateImage =
            "{\"data\":true,\"success\":true,\"status\":200}";
    }
}
