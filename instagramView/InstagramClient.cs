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
            var contentDetailsJSON = await response.Content.ReadAsStringAsync();
        }
    }
}
