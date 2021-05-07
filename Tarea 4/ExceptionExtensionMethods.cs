using System;
using System.Reflection;

namespace Tarea_4
{
    public static class ExceptionExtensionMethods
    {
        public static void SetMessage(this Exception exception, string message)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            Type type = typeof(Exception);
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo fieldInfo = type.GetField("_message", flags);

            fieldInfo.SetValue(exception, message);
        }

        public static string GetFullMessage(this Exception exception)
        {
            string fullMessage = exception.Message;
            Exception innerException = exception;

            while(innerException != null)
            {
                fullMessage += "\n\n" + innerException.Message;

                innerException = innerException.InnerException;
            }

            return fullMessage;
        }

        public static string GetFullStackTrace(this Exception exception)
        {
            string stackTrace = exception.StackTrace;
            Exception innerException = exception.InnerException;

            while(innerException != null)
            {
                stackTrace = innerException.StackTrace + "\n" + stackTrace;

                innerException = innerException.InnerException;
            }

            return stackTrace;
        }
    }
}
