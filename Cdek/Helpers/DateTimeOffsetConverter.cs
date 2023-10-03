using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XyloCode.ThirdPartyServices.Cdek.Helpers
{
    internal class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString();
            s = string.Concat(s.AsSpan(0, s.Length - 2), ":", s.AsSpan(s.Length - 2));
            if (DateTimeOffset.TryParse(s, out DateTimeOffset result))
                return result;

            throw new Exception();
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
