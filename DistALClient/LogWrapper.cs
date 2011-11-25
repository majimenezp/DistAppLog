using System;
using System.Collections.Generic;
using System.Text;

namespace DistALClient
{
    public class LogWrapper:ILog
    {
        public string Name { get; set; }
        public LogWrapper(string name)
        {
            Name = name;
        }
        public void Debug(object message)
        {
            AppLogClient.Instance.SendDebugMessage(Name, message.ToString());
        }

        public void DebugFormat(string format, params object[] args)
        {
            AppLogClient.Instance.SendDebugMessage(Name, string.Format(format,args));
        }

        public void DebugFormat(string format, object arg0)
        {
            AppLogClient.Instance.SendDebugMessage(Name, string.Format(format, arg0));
        }

        public void DebugFormat(string format, object arg0, object arg1)
        {
            AppLogClient.Instance.SendDebugMessage(Name, string.Format(format, arg0, arg1));
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(object message)
        {
            AppLogClient.Instance.SendInfoMessage(Name, message.ToString());
        }

        public void Info(object message, Exception exception)
        {
            AppLogClient.Instance.SendInfoMessage(Name,message.ToString() +"|" +exception.ToString());
        }

        public void InfoFormat(string format, params object[] args)
        {
            AppLogClient.Instance.SendInfoMessage(Name, string.Format(format, args));
        }

        public void InfoFormat(string format, object arg0)
        {
            AppLogClient.Instance.SendInfoMessage(Name, string.Format(format, arg0));
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            AppLogClient.Instance.SendInfoMessage(Name, string.Format(format, arg0, arg1));
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            AppLogClient.Instance.SendInfoMessage(Name, string.Format(format, arg0, arg1, arg2));
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn(object message)
        {
            AppLogClient.Instance.SendWarningMessage(Name, message.ToString());
        }

        public void Warn(object message, Exception exception)
        {
            AppLogClient.Instance.SendWarningMessage(Name, message.ToString(),exception);
        }

        public void WarnFormat(string format, params object[] args)
        {
            AppLogClient.Instance.SendWarningMessage(Name,string.Format(format,args));
        }

        public void WarnFormat(string format, object arg0)
        {
            AppLogClient.Instance.SendWarningMessage(Name, string.Format(format, arg0));
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            AppLogClient.Instance.SendWarningMessage(Name, string.Format(format, arg0, arg1));
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            AppLogClient.Instance.SendWarningMessage(Name, string.Format(format, arg0, arg1, arg2));
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(object message)
        {
            AppLogClient.Instance.SendErrorMessage(Name, message.ToString());
        }

        public void Error(object message, Exception exception)
        {
            AppLogClient.Instance.SendErrorMessage(Name, message.ToString(),exception);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            AppLogClient.Instance.SendErrorMessage(Name, string.Format(format, args));
        }

        public void ErrorFormat(string format, object arg0)
        {
            AppLogClient.Instance.SendErrorMessage(Name, string.Format(format, arg0));
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            AppLogClient.Instance.SendErrorMessage(Name, string.Format(format, arg0,arg1));
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            AppLogClient.Instance.SendErrorMessage(Name, string.Format(format, arg0, arg2));
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(object message)
        {
            AppLogClient.Instance.SendFatalMessage(Name, message.ToString());
        }

        public void Fatal(object message, Exception exception)
        {
            AppLogClient.Instance.SendFatalMessage(Name, message.ToString(), exception);
        }

        public void FatalFormat(string format, params object[] args)
        {
            AppLogClient.Instance.SendFatalMessage(Name, string.Format(format,args));
        }

        public void FatalFormat(string format, object arg0)
        {
            AppLogClient.Instance.SendFatalMessage(Name, string.Format(format, arg0));
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            AppLogClient.Instance.SendFatalMessage(Name, string.Format(format, arg0,arg1));
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            AppLogClient.Instance.SendFatalMessage(Name, string.Format(format, arg0, arg2));
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
