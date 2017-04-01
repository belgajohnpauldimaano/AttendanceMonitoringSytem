using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AMS_Server
{
    class ErrorLogger
    {
        private string ErrorPath;

        public ErrorLogger(string path) {
            ErrorPath = path;
        }

        public void LogError(string message) {
            try {
                using(StreamWriter writer = new StreamWriter(ErrorPath, true)){
                    writer.WriteLine("-------------------------------------------------------------");
                    writer.WriteLine(message + Environment.NewLine);
                    writer.WriteLine("-------------------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(ErrorPath, true))
                {
                    writer.WriteLine("-------------------------------------------------------------");
                    writer.WriteLine(String.Format("Message : {0} ; Stack Trace : {1} ; Date : {2}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString()));
                    writer.WriteLine("-------------------------------------------------------------");
                }
            }
        }
    }
}
