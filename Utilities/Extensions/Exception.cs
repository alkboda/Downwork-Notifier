using System;
using System.Text;

namespace Utilities.Extensions
{
    public static class ExceptionEx
    {
        public static String GetFullMessage(this Exception exception)
        {
            StringBuilder msg = new StringBuilder(exception.Message);
            var ex = exception.InnerException;
            while (ex != null)
            {
                msg.AppendLine();
                msg.Append(ex.Message);
                ex = ex.InnerException;
            };
            return msg.ToString();
        }
    }
}
