using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace Practice.Common
{
    // source: https://github.com/JamesNK/Newtonsoft.Json/blob/master/Src/Newtonsoft.Json/Converters/IsoDateTimeConverter.cs

    /// <summary>
    /// UnixTimestamp JsonConverter
    /// </summary>
    public class UnixTimestampDateTimeConvert : DateTimeConverterBase
    {
        private DateTimeStyles _dateTimeStyles = DateTimeStyles.RoundtripKind;
        private string _dateTimeFormat;
        private CultureInfo _culture;

        public DateTimeStyles DateTimeStyles
        {
            get => _dateTimeStyles;
            set => _dateTimeStyles = value;
        }

        public string DateTimeFormat
        {
            get => _dateTimeFormat ?? string.Empty;
            set => _dateTimeFormat = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        public CultureInfo Culture
        {
            get => _culture ?? CultureInfo.CurrentCulture;
            set => _culture = value;
        }


        internal static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool nullable = objectType == typeof(DateTime?) || objectType == typeof(DateTimeOffset?);
            if (reader.TokenType == JsonToken.Null)
            {
                if (!nullable)
                {
                    throw new JsonSerializationException("Cannot convert null value to UnixTimestamp.");
                }

                return null;
            }

            Type t = nullable
                ? Nullable.GetUnderlyingType(objectType)
                : objectType;

            if (reader.TokenType == JsonToken.Date)
            {
                if (t == typeof(DateTimeOffset))
                {
                    return reader.Value is DateTimeOffset ? reader.Value : new DateTimeOffset((DateTime)reader.Value!);
                }

                if (reader.Value is DateTimeOffset offset)
                {
                    return offset.DateTime;
                }

                return reader.Value;
            }

            if (reader.TokenType == JsonToken.Integer)
            {
                long val = (long)reader.Value;

                if (val < 2_147_483_648)
                {
                    val *= 1000;
                }

                if (t == typeof(DateTimeOffset))
                {
                    return DateTimeOffset.FromUnixTimeMilliseconds(val).ToUniversalTime();
                }

                return DateTimeOffset.FromUnixTimeMilliseconds(val).UtcDateTime;
            }

            if (reader.TokenType == JsonToken.String)
            {
                string dateText = reader.Value?.ToString();

                if (string.IsNullOrWhiteSpace(dateText) && nullable)
                {
                    return null;
                }

                if (t == typeof(DateTimeOffset))
                {
                    if (!string.IsNullOrWhiteSpace(_dateTimeFormat))
                    {
                        return DateTimeOffset.ParseExact(dateText, _dateTimeFormat, Culture, _dateTimeStyles);
                    }
                    else
                    {
                        return DateTimeOffset.Parse(dateText, Culture, _dateTimeStyles);
                    }
                }

                if (!string.IsNullOrWhiteSpace(_dateTimeFormat))
                {
                    return DateTime.ParseExact(dateText, _dateTimeFormat, Culture, _dateTimeStyles);
                }
                else
                {
                    return DateTime.Parse(dateText, Culture, _dateTimeStyles);
                }
            }

            throw new JsonSerializationException("Unexpected token parsing date.");

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long milliseconds;

            if (value is DateTime dateTime)
            {
                milliseconds = dateTime.Kind == DateTimeKind.Unspecified
                    ? (long)(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc) - UnixEpoch).TotalMilliseconds
                    : (long)(dateTime.ToUniversalTime() - UnixEpoch).TotalMilliseconds;

            }
            else if (value is DateTimeOffset dateTimeOffset)
            {
                milliseconds = (long)(dateTimeOffset.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
            }
            else
            {
                throw new JsonSerializationException("Expected date object value.");
            }

            // 考慮到生日取消判斷
            //if (milliseconds < 0)
            //{
            //    throw new JsonSerializationException("Cannot convert date value that is before Unix epoch of 00:00:00 UTC on 1 January 1970.");
            //}

            writer.WriteValue(milliseconds);
        }
    }
}
