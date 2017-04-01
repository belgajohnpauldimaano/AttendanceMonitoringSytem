using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS_Server
{
    class SMSSender
    {
        
        //const  string apiCode = "09486489832PG16PHJ4";
        //const  string apiCode = "09486489832_EBZU5";
        const string apiCode = "09486489832_2U3YV";
        //########################################################################################
        //iTexmo API for C# / ASP --> go to www.itexmo.com/developers.php for API Documentation
        //########################################################################################
        public object itexmo(string Number, string Message, string API_CODE = apiCode)
        {

            object functionReturnValue = null;
            try
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    System.Collections.Specialized.NameValueCollection parameter = new System.Collections.Specialized.NameValueCollection();
                    string url = "https://www.itexmo.com/php_api/api.php";
                    parameter.Add("1", Number);
                    parameter.Add("2", Message);
                    parameter.Add("3", API_CODE);
                    dynamic rpb = client.UploadValues(url, "POST", parameter);
                    functionReturnValue = (new System.Text.UTF8Encoding()).GetString(rpb);
                }
            }
            catch { }
            return functionReturnValue;
        }
        public object itexmo_bal()
        {
            object functionReturnValue = null;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                System.Collections.Specialized.NameValueCollection parameter = new System.Collections.Specialized.NameValueCollection();
                string url = "https://www.itexmo.com/php_api/api.php";
                //string url = "http://127.0.0.1/sms/iTexko/api.php";
                parameter.Add("4", apiCode);
                dynamic rpb = client.UploadValues(url, "POST", parameter);
                functionReturnValue = (new System.Text.UTF8Encoding()).GetString(rpb);
            }
            return functionReturnValue;
        }
        //########################################################################################
        //API END     '###########################################################################
        //########################################################################################

        public int sendmsg(string number , string msg) {
            try
            {
                return int.Parse(itexmo(number, msg, apiCode).ToString());
            }
            catch
            {
                return -1;
            }
        }

    }
}
