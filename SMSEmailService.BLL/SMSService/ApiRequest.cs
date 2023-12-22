using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SMSEmailService.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SMSEmailService.BLL.SMSService
{
    public class APIRequest
    {
        HttpWebRequest _req = null;
        Encoding _defaultEncoding = Encoding.UTF8;
        CompanyConfiguration _config;

        /// <summary>
        /// Default encoding for request and reponse content.
        /// By default this is UTF-8
        /// </summary>
        public Encoding DefaultEncoding
        {
            get { return _defaultEncoding; }
            set { _defaultEncoding = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public CompanyConfiguration Config
        {
            get { return _config; }
            set { _config = value; }
        }

        public APIRequest() { }
        public APIRequest(CompanyConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// Process request in a synchronous way. 
        /// It is recommended to run this method on a separate thread than the current thread.
        /// </summary>
        /// <typeparam name="T">Type of return value</typeparam>
        /// <param name="requestData">Request data</param>
        /// <param name="requestUri">Request uri</param>
        /// <param name="method">Request method, that is, PUT,GET,POST,DELETE,etc</param>
        /// <param name="serializeSettings">Serialization settings</param>
        /// <returns>The response data</returns>
        public T ProcessRequest<T>(object requestData, string requestUri, string method)
        {

            _req = WebRequest.Create(requestUri) as HttpWebRequest;
            _req.Method = method.Trim();
            _req.Timeout = 30000;
            _req.ContentType = "application/json";
            if (Config != null && !string.IsNullOrEmpty(Config.ApiKey))
            {
                _req.Headers.Add("X-Apikey", Config.ApiKey);
            }
            if (string.Compare(_req.Method, "PUT", true) == 0 ||
                string.Compare(_req.Method, "POST", true) == 0)
            {
                using (Stream stream = _req.GetRequestStream())
                {
                    string sj = JsonConvert.SerializeObject(requestData);
                    byte[] postData = DefaultEncoding.GetBytes(sj);
                    stream.Write(postData, 0, postData.Length);
                }
            }

            using (HttpWebResponse rsp = _req.GetResponse() as HttpWebResponse)
            {
                using (StreamReader reader = new StreamReader(rsp.GetResponseStream(), DefaultEncoding))
                {
                    string json = reader.ReadToEnd();
                    int statusCode = (int)rsp.StatusCode;
                    if (statusCode >= 200 && statusCode <= 299)
                    {
                        if (!json.Contains("error_message"))
                        {
                            return JsonConvert.DeserializeObject<T>(json);
                        }
                        else
                        {
                           throw new Exception(ParseErrorMessage(json));
                        }
                    }
                    else
                    {
                        throw new Exception(string.Format("Remote Server Error. Error Code={0}", statusCode));
                    }
                }
            }
        }

        string ParseErrorMessage(string json)
        {
            string errMsg = string.Empty;
            JContainer err = JsonConvert.DeserializeObject(json) as JContainer;

            try
            {
                if (err is JObject)
                {
                    errMsg = err.Value<string>("error_message");
                }
                else if (err is JArray)
                {
                    errMsg = string.Join(";", err.Select(a => a["error_message"].Value<string>()));
                }
                else
                {
                    errMsg = "Generic error";
                }
            }
            catch
            {
                errMsg = "Generic error";
            }

             return errMsg;
        }

        public void AbortRequest()
        {
            if (_req != null)
            {
                _req.Abort();
                _req = null;
            }
        }

    }
}
