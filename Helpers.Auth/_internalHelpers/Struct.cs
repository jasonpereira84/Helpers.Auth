using System;

namespace JasonPereira84.Helpers
{
    internal static partial class _internalHelpers
    {
        public static Int64? ParseOrDefault(this String @string, Nullable<Int64> defaultValue = default)
            => @string.IsNullOrEmptyOrWhiteSpace() ? defaultValue
                : Int64.TryParse(@string, out Int64 value) ? value
                    : defaultValue;

        public static Int32? ParseOrDefault(this String @string, Nullable<Int32> defaultValue = default)
            => @string.IsNullOrEmptyOrWhiteSpace() ? defaultValue
                : Int32.TryParse(@string, out Int32 value) ? value
                    : defaultValue;

        public static Int16? ParseOrDefault(this String @string, Nullable<Int16> defaultValue = default)
            => @string.IsNullOrEmptyOrWhiteSpace() ? defaultValue
                : Int16.TryParse(@string, out Int16 value) ? value
                    : defaultValue;

        public static Byte? ParseOrDefault(this String @string, Nullable<Byte> defaultValue = default)
            => @string.IsNullOrEmptyOrWhiteSpace() ? defaultValue
                : Byte.TryParse(@string, out Byte value) ? value
                    : defaultValue;

        public static DateTime? ParseOrDefault(this String @string, Nullable<DateTime> defaultValue = default)
            => @string.IsNullOrEmptyOrWhiteSpace() ? defaultValue
                : !Int64.TryParse(@string, out Int64 value) ? defaultValue
                    : DateTimeOffset.FromUnixTimeSeconds(value).DateTime;
    }
}
