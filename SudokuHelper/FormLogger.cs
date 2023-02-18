using Common;
using Common.Extensions;
using System;
using System.Threading;
using System.Windows.Forms;

namespace SudokuHelper
{
    public static class FormLogger
    {
        public static void Log(string message, TextBox tbLogger, bool bLogTextbox = true, bool bLogFile = true)
        {            
            try
            {
                if(bLogFile) Logger.Log(message);
                if (bLogTextbox)
                {
                    if (tbLogger != null)
                    {
                        //message = $"{DateTime.Now.ToDefaultString()} : [Thread-{Thread.CurrentThread.ManagedThreadId}] | {message}";
                        message = $"{DateTime.Now.ToDefaultString()} : {message}";

                        //using extension method
                        tbLogger.InvokeIfRequired(() =>
                        {
                            tbLogger.AppendText($"{message}{Environment.NewLine}");
                        });
                        //if (tbLogger.InvokeRequired)
                        //{
                        //    tbLogger.Invoke(new MethodInvoker(delegate
                        //    {
                        //        tbLogger.AppendText($"{message}{Environment.NewLine}");
                        //    }));
                        //}
                        //else
                        //{
                        //    tbLogger.AppendText($"{message}{Environment.NewLine}");
                        //}
                    }
                    else
                    {
                        Logger.Log("TextBox tbLogger is not init!");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}
