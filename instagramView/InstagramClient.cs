using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace instagramView
{
    public class InstagramClient
    {
        private static WebBrowser mWebBrowser;

        public const string UserName = "taeyeon_ss";
        public const string WebUrl = "https://instagram.com/";

        public InstagramClient(WebBrowser webBrowser)
        {
            mWebBrowser = webBrowser;
        }

        public async static void GetData()
        {
            mWebBrowser.Visible = true;
            mWebBrowser.Url = new Uri(WebUrl);

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(WebUrl);

            var response = await httpClient.GetAsync(UserName).ConfigureAwait(false);
            string contentDetailsString = await response.Content.ReadAsStringAsync();

            object jsonData = GetMiddleString(contentDetailsString, "window._sharedData = ", ";</script>");

        }

        /**
         * Get to code: http://kimstar.kr/2452/
         */
        public static object GetMiddleString(string str, string begin, string end)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            string result = null;
            if (str.IndexOf(begin) > -1)
            {
                str = str.Substring(str.IndexOf(begin) + begin.Length);
                if (str.IndexOf(end) > -1)
                {
                    result = str.Substring(0, str.IndexOf(end));
                }
                else
                {
                    result = str;
                }
            }
            return JsonConvert.DeserializeObject(result);
        }
    }
}
