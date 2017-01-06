using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SignalR.WebAPI.Common
{
    public static class Extensions
    {
        public static T GetSessionValue<T>(this System.Web.HttpContextBase httpContext, String SessionKey)
        {
            object value = httpContext.Session[SessionKey];

            if (value != null)
            {
                return value.Parse<T>();
            }

            return default(T);
        }

        public static T Parse<T>(this Object value)
        {
            if (value == null)
                return default(T);



            try
            {
                return (T)(object)value;
            }
            catch
            {
                return default(T);
            }
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return String.IsNullOrWhiteSpace(value);
        }

        public static void LogException(this log4net.ILog log, LogLevel level = LogLevel.Fatal, String message = null, Exception exception = null)
        {
            if (message.IsNullOrEmpty() && exception == null)
                throw new ArgumentNullException("message", "message and exception both can't be null.");

            // get call stack
            string methodName = GetCallingMethodName();

            var messageWithMoreInfo = methodName;

            if (!message.IsNullOrEmpty())
            {
                messageWithMoreInfo += (" - " + message);
            }

            switch (level)
            {
                case LogLevel.Debug:
                    if (exception == null)
                    {
                        log.Debug(messageWithMoreInfo);
                    }
                    else
                    {
                        log.Debug(messageWithMoreInfo, exception);
                    }

                    break;
                case LogLevel.Info:
                    if (exception == null)
                    {
                        log.Info(messageWithMoreInfo);
                    }
                    else
                    {
                        log.Info(messageWithMoreInfo, exception);
                    }

                    break;
                case LogLevel.Warn:
                    if (exception == null)
                    {
                        log.Warn(messageWithMoreInfo);
                    }
                    else
                    {
                        log.Warn(messageWithMoreInfo, exception);
                    }
                    break;
                case LogLevel.Error:
                    if (exception == null)
                    {
                        log.Error(messageWithMoreInfo);
                    }
                    else
                    {
                        log.Error(messageWithMoreInfo, exception);
                    }
                    break;
                case LogLevel.Fatal:
                    if (exception == null)
                    {
                        log.Fatal(messageWithMoreInfo);
                    }
                    else
                    {
                        log.Fatal(messageWithMoreInfo, exception);

                    }
                    break;
                case LogLevel.Off:
                default:
                    break;
            }
        }

        private static string GetCallingMethodName(int frameNumber = 2)
        {
            // get call stack
            var stackTrace = new StackTrace();

            try
            {
                // get calling method name
                var methodName = stackTrace.GetFrame(frameNumber).GetMethod().Name;

                return methodName;
            }
            catch
            {
                return String.Format("NoMethodName - {0}", frameNumber);
            }
        }

        public enum LogLevel
        {
            Off = 0,
            Debug = 1,
            Info = 2,
            Warn = 3,
            Error = 4,
            Fatal = 5
        }
    }
}