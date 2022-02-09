using QueueTriggerDI.Utils.Exceptions;
using System;

namespace QueueTriggerDI.Utils.Checkers
{
    public static class Verify
    {
        public static string NotEmpty(string name, string value)
        {
            if (name == null)
                throw VerifyException.ValueIsNullOrEmpty("name");

            if (string.IsNullOrWhiteSpace(name))
                throw VerifyException.ValueIsNullOrEmpty("name");

            if (value == null)
                throw VerifyException.ValueIsNullOrEmpty(value);

            if(string.IsNullOrWhiteSpace(value))
                throw VerifyException.ValueIsNullOrEmpty(value);

            return value;
        }

        public static Guid NotEmpty(string name, Guid value)
        {
            NotEmpty("name", name);

            if (value == Guid.Empty)
                throw VerifyException.ValueIsNullOrEmpty(name);

            return value;
        }

        public static int NotNegative(string name, int value)
        {
            NotEmpty("name", name);

            if (value < 0)
                throw VerifyException.ValueIsLessThanZero(name);

            return value;
        }

        public static T NotNullOrDefault<T>(string name, T value)
        {
            NotEmpty("name", name);

            if (value == null)
                throw VerifyException.ValueIsNullOrDefault(name);

            return value;
        }
    }
}
