using Newtonsoft.Json;
using System;

namespace ApiLibrary.ApiEntities.Converters
{
    public class YesNoConverter : JsonConverter
    {
        public override bool CanRead { get; } = true;
        public override bool CanWrite { get; } = false;
        
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(bool));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            String value = serializer.Deserialize<String>(reader)?.Trim().ToLowerInvariant();
            if (value == "yes" || value == "1")
            {
                return true;
            }
            return false;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
