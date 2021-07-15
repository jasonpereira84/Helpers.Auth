using System;
using System.Linq;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    internal static partial class _internalHelpers
    {
        public static Boolean None<TSource>(this IEnumerable<TSource> source)
            => !source.Any();

        public static Boolean None<TSource>(this IEnumerable<TSource> source, Func<TSource, Boolean> predicate)
            => !source.Any(predicate);

        public static Boolean IsNullOrNone<TSource>(this IEnumerable<TSource> source)
            => (source == null) || !source.Any();

        public static Boolean IsNullOrNone<TSource>(this IEnumerable<TSource> source, Func<TSource, Boolean> predicate)
            => (source == null) || !source.Any(predicate);

        public static Boolean IsNotNullOrNone<TSource>(this IEnumerable<TSource> source)
            => !IsNullOrNone(source);

        public static Boolean IsNotNullOrNone<TSource>(this IEnumerable<TSource> source, Func<TSource, Boolean> predicate)
            => !IsNullOrNone(source, predicate);

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, Boolean> predicate, TSource defaultValue)
            where TSource : class
            => source.FirstOrDefault(predicate) ?? defaultValue;

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
            where TSource : class
            => source.FirstOrDefault() ?? defaultValue;

        public static Boolean NotContains<TSource>(this IEnumerable<TSource> source, TSource value)
            => !source.Contains(value);

        public static Boolean ContainsAny<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> values)
        {
            if (source.None())
                return false;

            foreach (var value in values)
                if (source.Contains(value))
                    return true;

            return false;
        }

        public static Boolean ContainsAll<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> values)
        {
            if (source.None())
                return false;

            foreach (var value in values)
                if (source.NotContains(value))
                    return false;

            return true;
        }

    }
}
