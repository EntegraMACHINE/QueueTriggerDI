using System;

namespace QueueTriggerDI.Utils.Exceptions
{
    public class VerifyException : Exception
    {
        public VerifyException() : base() { }

        public VerifyException(string message) : base(message) { }

        public VerifyException(string message, params object[] parameters) : base(string.Format(message, parameters)) { }

        public static VerifyException ValueIsNullOrEmpty(string name)
        {
            const string message = "{0} value is null or empty.";

            throw new VerifyException(message, name);
        }

        public static VerifyException ValueIsLessThanZero(string name)
        {
            const string message = "{0} value is less than zero.";

            throw new VerifyException(message, name);
        }

        public static VerifyException ValueIsNullOrDefault(string name)
        {
            const string message = "{0} value is null or default.";

            throw new VerifyException(message, name);
        }
    }
}
