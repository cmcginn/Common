using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.IO;

namespace Common.Services.Payment.Interfaces
{
    public class Utilities
    {
        private const int DEFAULTTIMEOUT = 100000;

        public static string SendRequestByPost(string serviceUrl, string postData)
        {
            return SendRequestByPost(serviceUrl, postData, null, DEFAULTTIMEOUT);
        }

        public static string SendRequestByPost(string serviceUrl, string postData, System.Net.WebProxy proxy, int timeout)
        {
            WebResponse resp = null;
            WebRequest req = null;
            string response = string.Empty;
            byte[] reqBytes = null;

            try
            {
                reqBytes = Encoding.UTF8.GetBytes(postData);
                req = WebRequest.Create(serviceUrl);
                req.Method = "POST";
                req.ContentLength = reqBytes.Length;
                req.ContentType = "application/x-www-form-urlencoded";
                req.Timeout = timeout;
                if (proxy != null)
                {
                    req.Proxy = proxy;
                }
                Stream outStream = req.GetRequestStream();
                outStream.Write(reqBytes, 0, reqBytes.Length);                
                outStream.Close();
                resp = req.GetResponse();
                StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8, true);
                response += sr.ReadToEnd();
                sr.Close();
            }          
            catch 
            {
                throw;
            }

            return response;
        }


        // Parse Html Tag Function
        // 
        // Parses the value information from any INPUT tag in an HTML string where the name="" attribute 
        // matched the tagID parameter
        //
        // 1/12/2005, mmcconnell1618
        public static string GetInputTagValueFromtHtmlString(string source, string tagID)
        {
            string result = string.Empty;
            string[] tags = source.Split('<');

            foreach (string tag in tags)
            {
               if (tag.Contains(">"))
               {
                   if (tag.ToLower().StartsWith("input "))
                   {
                       if (tag.ToLower().Contains("name=\"" + tagID.ToLower() + "\""))
                       {
                           int valueLocation = tag.ToLower().IndexOf("value=\"");
                           if (valueLocation >= 0)
                           {
                               char[] chars = tag.ToCharArray(0,tag.Length);
                               for (int j = valueLocation + 7; j < chars.Length -1;j++)
                               {
                                   if (chars[j] != '"')
                                   {
                                       result += chars[j];
                                   }
                                   else
                                   {
                                       break;
                                   }
                               }
                           }
                       }
                   }
               }
            }
            return result;
        }
                          
    }
}
