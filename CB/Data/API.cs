using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace CB.Data
{
    public class API
    {
        static List<dynamic> lines = new List<dynamic>();
        public static List<dynamic> FetchRequest()
        {
            lines = ApiReq(GetUrlIp(GetIpUrl()));
            return lines;

        }

        public static string GetIpUrl()
        {
            IPAddress addr = IPAddress.Parse("31.13.71.36");
            Console.WriteLine(addr);
            IPHostEntry entry = Dns.GetHostEntry(addr);
            if (entry != null)
            {
                Console.WriteLine(entry.HostName);
                foreach (IPAddress ip in entry.AddressList)
                {
                    Console.WriteLine(ip.ToString());
                }
            }
            return Convert.ToString(entry.HostName);
        }
        public static string GetUrlIp(string url)
        {
            string temp = "https://" + url;
            Uri myUri = new Uri(temp);
            IPAddress[] ip = Dns.GetHostAddresses(myUri.Host);

            foreach (IPAddress ipAddress in ip)
            {
                Console.WriteLine(ipAddress.ToString());
            }


            return Convert.ToString(ip[0]);
        }

        public static List<dynamic> ApiReq(string ip)
        {
            List<dynamic> json = new List<dynamic>();
            RestClient client = new RestClient("https://api.abuseipdb.com/api/v2/check");
            RestRequest request = new();
            request.AddHeader("Key", "87de98a405dd35a259ebf321ed41d7c7906417cac5d3a693dc0e2371d03e4a06d2ef29c814ff4b09");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("ipAddress", ip);
            request.AddParameter("maxAgeInDays", "90");
            request.AddParameter("verbose", "");

            RestResponse response = client.Execute(request);

            dynamic? parsedJson = JsonConvert.DeserializeObject(response.Content);
            json.Add(parsedJson);
            return json;
        }
    }
}
