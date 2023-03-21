using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace CB.Data
{
    public class API
    {
        // Initialise a list of dynamic objects to store the JSON response, don't think get; set; is needed
        static List<dynamic> lines = new List<dynamic>();
        public static List<dynamic> FetchRequest()
        {
            // Get the string from getipurl and pass it to geturlip, then pass the result to apireq and store it in lines
            lines = ApiReq(GetUrlIp(GetIpUrl()));
            // Return the list
            return lines;
        }

        public static string GetIpUrl()
        {
            // Create a new ipaddress variable and set it to an ip (currently hardcoded to facebook for testing)
            IPAddress addr = IPAddress.Parse("31.13.71.36");
            // Print the ipaddress to the console, debug purposes
            Console.WriteLine(addr);
            // Get the host entry from the ipaddress
            IPHostEntry entry = Dns.GetHostEntry(addr);
            // If the entry isn't null, might reverse for non nesting
            if (entry != null)
            {
                // Print the hostname to the console, debug purposes
                Console.WriteLine(entry.HostName);
                // Print all the ip addresses to the console, debug purposes
                foreach (IPAddress ip in entry.AddressList)
                {

                    Console.WriteLine(ip.ToString());
                }
            }
            // Return the hostname, might change to return the domain name instead
            return Convert.ToString(entry.HostName);
        }
        public static string GetUrlIp(string url)
        {
            // Add https:// to the url if it doesn't already have it
            if (!url.StartsWith("https://"))
            {
                url = "https://" + url;
            }
            // Create a new Uri object from the url
            Uri myUri = new Uri(url);
            // Get the IP addressArray from the Uri object
            IPAddress[] ip = Dns.GetHostAddresses(myUri.Host);
            // Print the IP addressArray to the console, debug purposes
            foreach (IPAddress ipAddress in ip)
            {
                Console.WriteLine(ipAddress.ToString());
            }
            // Return the first IP address in the array, which is the ipv4 
            return Convert.ToString(ip[0]);
        }

        public static List<dynamic> ApiReq(string ip)
        {
            // Initialise an empty list
            List<dynamic> json = new List<dynamic>();
            // Initialise a new RestSharp client to the API endpoint
            RestClient client = new RestClient("https://api.abuseipdb.com/api/v2/check");
            // Initialise a new RestSharp request to the API endpoint
            RestRequest request = new();
            // Add the API key to the request header
            request.AddHeader("Key", "87de98a405dd35a259ebf321ed41d7c7906417cac5d3a693dc0e2371d03e4a06d2ef29c814ff4b09");
            // Add the Accept header to the request header to specify the response format
            request.AddHeader("Accept", "application/json");
            // Add the IP address to the request parameters
            request.AddParameter("ipAddress", ip);
            // Add the maxAgeInDays parameter to the request parameters, might turn it into a variable later
            request.AddParameter("maxAgeInDays", "90");
            // Add the verbose parameter to the request parameters so we get the full response
            request.AddParameter("verbose", "");
            // Execute the request and store the response
            RestResponse response = client.Execute(request);
            // Parse the response content into a dynamic object
            dynamic? parsedJson = JsonConvert.DeserializeObject(response.Content);
            // Add the parsed JSON to the list
            json.Add(parsedJson);
            // Return the list
            return json;
        }
    }
}
