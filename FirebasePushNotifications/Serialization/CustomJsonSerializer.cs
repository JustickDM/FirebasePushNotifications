using FirebasePushNotifications.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FirebasePushNotifications.Serialization
{
    internal class CustomJsonSerializer : ISerializer
    {
        private static JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
        };

        static CustomJsonSerializer()
        {
            _settings.Converters.Add(new StringEnumConverter());
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value, _settings);
        }
    }
}
