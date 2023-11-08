using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XyloCode.ThirdPartyServices.Cdek.Helpers
{
    internal class DateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString();
            return DateTimeOffset.Parse(s);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            var dateTime = value.ToString("s");
            var sign = value.Offset.TotalSeconds >= 0 ? '+' : '-';
            var offset = value.Offset.ToString("hhmm");
            writer.WriteStringValue(dateTime + sign + offset);
        }
    }
}
