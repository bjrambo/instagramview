using Newtonsoft.Json;
using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
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
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(WebUrl);

            var response = await httpClient.GetAsync(UserName).ConfigureAwait(false);
            string contentDetailsString = await response.Content.ReadAsStringAsync();

            string jsonData = GetMiddleString(contentDetailsString, "window._sharedData = ", ";</script>");

            var ObjectData = JsonConvert.DeserializeObject<Response.InstaList>(jsonData);

            List<Response.Edges> EdgesDatas = ObjectData.entry_data.ProfilePage[0].graphql.user.edge_owner_to_timeline_media.edges;

            foreach (var nodeData in EdgesDatas)
            {
                if(DownloadRemoteImageFile(nodeData.node.display_url, $"C:/Users/qw541/Desktop/인스타그램테스트/{nodeData.node.shortcode}_{nodeData.node.id}.jpg") != true)
                {
                    break;
                }
            }

        }

        public static bool DownloadRemoteImageFile(string uri, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            bool bImage = response.ContentType.StartsWith("image",
                StringComparison.OrdinalIgnoreCase);
            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect) &&
                bImage)
            {
                using (Stream inputStream = response.GetResponseStream())
                using (Stream outputStream = File.OpenWrite(fileName))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * Get to code: http://kimstar.kr/2452/
         */
        public static string GetMiddleString(string str, string begin, string end)
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
            return result;
        }
    }
}
