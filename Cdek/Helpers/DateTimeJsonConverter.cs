using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XyloCode.ThirdPartyServices.Cdek.Helpers
{
    internal class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var sb = new StringBuilder();
            
            sb.Append(value.ToString("s"));

            var offset = value.Kind switch
            {
                DateTimeKind.Utc => new TimeSpan(0, 0, 0),
                DateTimeKind.Unspecified or DateTimeKind.Local => TimeZoneInfo.Local.GetUtcOffset(value),
                _ => throw new NotImplementedException(),
            };

            if (offset.TotalSeconds >= 0)
                sb.Append('+');
            else
                sb.Append('-');
            
            sb.Append(offset.ToString("hhmm"));
            
            writer.WriteStringValue(sb.ToString());
        }
    }
}
