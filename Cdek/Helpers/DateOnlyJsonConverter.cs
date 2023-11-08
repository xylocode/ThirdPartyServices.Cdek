using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XyloCode.ThirdPartyServices.Cdek.Helpers
{
    internal class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        const string format = "yyyy-MM-dd";
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString();
            if (DateOnly.TryParseExact(s, format, null, System.Globalization.DateTimeStyles.None, out DateOnly result))
                return result;

            throw new NotImplementedException(s);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(format));
        }
    }
}
