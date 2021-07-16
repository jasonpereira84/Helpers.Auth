using System;

namespace JasonPereira84.Helpers
{
    internal static partial class _internalHelpers
    {
        public static Boolean IsNull(this Object @object) => @object == null;

        public static Boolean IsNotNull(this Object @object) => !IsNull(@object);
    }
}
