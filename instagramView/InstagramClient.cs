using Newtonsoft.Json;
using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

namespace instagramView
{
    public class InstagramClient
    {
        private static WebBrowser mWebBrowser;

        public static string UserId;

        public readonly static string GET_INSTAGRAM_URL = "https://www.instagram.com/graphql/query/?query_hash=f2405b236d85e8296cf30347c9f08c2a&variables=%7B%22id%22%3A%22{0}%22%2C%22first%22%3A12%7D";

        public InstagramClient(WebBrowser webBrowser)
        {
            mWebBrowser = webBrowser;
        }

        public async static void GetData()
        {
            // Taeyeon instagram user id. It will be using to ulong type parameter.
            string TaeyeonInstaId = "329452045";
            var httpClient = new HttpClient();

            string FullUrl = String.Format(GET_INSTAGRAM_URL, TaeyeonInstaId);
            httpClient.BaseAddress = new Uri(FullUrl);

            // It has not async. will be comaback to respon.
            // TODO(BJRambo): checking using deadlock
            var response = await httpClient.GetAsync(UserId);
            string contentDetailsString = await response.Content.ReadAsStringAsync();

            var ObjectData = JsonConvert.DeserializeObject<Response.InstaList>(contentDetailsString);

            List<Response.Edges> EdgesDatas = ObjectData.data.user.edge_owner_to_timeline_media.edges;

            foreach (var nodeData in EdgesDatas)
            {
                if(DownloadRemoteImageFile(nodeData.node.display_url, $"C:/Users/qw541/Desktop/인스타그램테스트/{nodeData.node.shortcode}_{nodeData.node.id}.jpg") != true)
                {
                    continue;
                }
            }
        }

        /**
         * code by : http://www.gisdeveloper.co.kr/?p=1711
         */ 
        public static bool DownloadRemoteImageFile(string uri, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            bool bImage = response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase);
            if ((response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Moved || response.StatusCode == HttpStatusCode.Redirect) && bImage)
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
                    }
                    while (bytesRead != 0);
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
