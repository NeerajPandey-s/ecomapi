using System.Text.Json;
using System.Text.Json.Serialization;

namespace EcomAPI.Common
{
    public class DateTimeOffsetJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return UserDatetimeOffSetDetails.ToUtc(DateTime.Parse(reader.GetString()!), UserDatetimeOffSetDetails.UserTimeZone!);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    public class DateTimeNullableOffsetJsonConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.GetString() is null)
            {
                return null;
            }

            return UserDatetimeOffSetDetails.ToUtc(DateTime.Parse(reader.GetString()!), UserDatetimeOffSetDetails.UserTimeZone!);
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }



    public static class UserDatetimeOffSetDetails
    {
        public static TimeZoneInfo? UserTimeZone { get; set; }
        
        internal static DateTime ToUtc(DateTime dateTime, TimeZoneInfo timeZone)
        {
            var unspecifiedDate = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeToUtc(unspecifiedDate, timeZone);
        }
    }



}
