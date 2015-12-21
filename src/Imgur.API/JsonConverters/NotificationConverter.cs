using System;
using System.Globalization;
using System.Reflection;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Imgur.API.JsonConverters
{
    /// <summary>
    ///     Converts Notifications to their appropriate type.
    /// </summary>
    public class NotificationConverter : JsonConverter
    {
        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            if (typeof (IMessage).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo()))
                return true;

            if (typeof (IComment).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo()))
                return true;

            return false;
        }

        /// <summary>
        ///     Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The Newtonsoft.Json.JsonReader to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var jsonString = JObject.Load(reader).ToString();

            if (CultureInfo.CurrentCulture.CompareInfo.IndexOf(jsonString, "comment", CompareOptions.IgnoreCase) > 0)
                return JsonConvert.DeserializeObject<Comment>(jsonString);

            if (CultureInfo.CurrentCulture.CompareInfo.IndexOf(jsonString, "message_num", CompareOptions.IgnoreCase) > 0)
                return JsonConvert.DeserializeObject<Message>(jsonString);

            throw new NotImplementedException("Unrecognized Notification type.");
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The Newtonsoft.Json.JsonWriter to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}