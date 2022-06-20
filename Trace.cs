using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Laut_News
{
    class Trace
    {

        private static string m_exePath = string.Empty;
        public static void Msg(string logMessage)
        {
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!File.Exists(m_exePath + "\\" + "log.txt"))
                File.Create(m_exePath + "\\" + "log.txt").Close();
            

            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
                    AppendLog(logMessage, w);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void AppendLog(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine("{0}:{1} - {2}", DateTime.Now, DateTime.Now.Millisecond, logMessage);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
