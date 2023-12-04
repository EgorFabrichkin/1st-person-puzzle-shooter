#nullable enable

using System;

namespace Utils
{
    public static class NullExtensions
    {
        public static T EnsureNotNull<T>(this T? value, string? massage = null)
        {
            if (value == null)
            {
                throw new Exception(massage ?? typeof(T).FullName);
            }

            return value;
        }
    }
}