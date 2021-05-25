using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace RS {
    public class HTTP {
        private const int BUFFER_SIZE = 4096;
        public static string POST(string uri, Dictionary<string, string> parameters)
        {
            return POST(uri, parameters, null, 0);
        }

        public static string POST(string uri, Dictionary<string, string> parameters, String proxy, int proxyport) {
            StringBuilder sb_parameters = new StringBuilder();
            bool isfirst = true;
            // parameters: name1=value1&name2=value2	
            foreach (KeyValuePair<string, string> kvp in parameters) {
                if (isfirst) {
                    isfirst = false;
                } else {
                    sb_parameters.Append('&');
                }
                if (kvp.Value.Length > BUFFER_SIZE) {
                    int i = 0;
                    StringBuilder tmp_param = new StringBuilder();
                    while (i < kvp.Value.Length) {
                        int rem = Math.Min(kvp.Value.Length - i , BUFFER_SIZE);
                        tmp_param.Append(Uri.EscapeDataString(kvp.Value.Substring(i, rem)));
                        i += BUFFER_SIZE;
                    }
                    sb_parameters.Append(kvp.Key).Append('=').Append(tmp_param.ToString());
                }
                else {
                    sb_parameters.Append(kvp.Key).Append('=').Append(Uri.EscapeDataString(kvp.Value));
                }
            }
            System.Net.ServicePointManager.Expect100Continue = false;
            WebRequest webRequest = WebRequest.Create(uri);
            if (Str.nullify(proxy)!=null) {
                webRequest.Proxy = new WebProxy(proxy, proxyport);
            }
            //Commenting out above required change to App.Config
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            Debug.Print(sb_parameters.ToString());
            byte[] bytes = Encoding.ASCII.GetBytes(sb_parameters.ToString());
            Stream os = null;
            try { // send the Post
                webRequest.ContentLength = bytes.Length;  //Count bytes to send
                os = webRequest.GetRequestStream();
                os.Write(bytes, 0, bytes.Length);         //Send it
            } catch (WebException ex) {
                throw new HTTPException(ex.Message);
            } finally {
                if (os != null) {
                    os.Close();
                }
            }

            try {
                WebResponse webResponse = webRequest.GetResponse();
                if (webResponse == null) { return null; }
                StreamReader sr = new StreamReader(webResponse.GetResponseStream());
                string res = sr.ReadToEnd();
                return res;
            } catch (WebException ex) {
                throw new HTTPException(ex.Message);
            }
        }
    }
}
