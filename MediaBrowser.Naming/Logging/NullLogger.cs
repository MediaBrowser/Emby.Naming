using System;

namespace MediaBrowser.Naming.Logging
{
    public class NullLogger : ILogger
    {
        public void Info(string message, params object[] paramList)
        {
        }

        public void Error(string message, params object[] paramList)
        {
        }

        public void Warn(string message, params object[] paramList)
        {
        }

        public void Debug(string message, params object[] paramList)
        {
        }

        public void Fatal(string message, params object[] paramList)
        {
        }

        public void FatalException(string message, Exception exception, params object[] paramList)
        {
        }

        public void ErrorException(string message, Exception exception, params object[] paramList)
        {
        }
    }
}
