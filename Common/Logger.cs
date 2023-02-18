using log4net;
using log4net.Config;
using System.Diagnostics;
using System.IO;
namespace Common
{
    public static class Logger
    {
        private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Stopwatch sw = null;
        public static void Start()
        {
            Log("Start timer");
            sw = new Stopwatch();
            sw.Start();
        }
        public static void End()
        {
            //Log(String.Format("Total Process time: {0}", sw.ElapsedMilliseconds));
            Log($"Total Process time: {sw.ElapsedMilliseconds} ms");
            sw = null;
            Log("End timer");
        }
        public static void Log(object message)
        {
            Logger.logger.Info(message);
        }
        public static void Info(string s)
        {
            Logger.logger.Info(s);
        }
        public static void Error(object message)
        {
            Logger.logger.Error(message);
        }
        public static string getConfigPath()
        {
            string s = System.Environment.CurrentDirectory;
            //string directory = AppDomain.CurrentDomain.BaseDirectory;
            //Console.WriteLine(s);
            return s;

        }        
        public static void InitConfig()
        {
            string ConfigPath = System.IO.Path.Combine(getConfigPath(), "log4net.config");
            XmlConfigurator.Configure(new FileInfo(ConfigPath));
        }
    }
}
