using Newtonsoft.Json;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;
using System.Net;

namespace CB.Data
{
    public class Fetch
    {
        public static void GetIpUrl(API api)
        {
            // Create a new ipaddress variable and set it to an ip (currently hardcoded to facebook for testing)
            IPAddress addr = IPAddress.Parse(api.IP);
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
            // Set the domain field, might change to the domain name instead of "entry.hostname"
            api.Domain = entry.HostName;
        }

        public static void GetUrlIp(API api)
        {
            // Add https:// to the url if it doesn't already have it
            if (!api.Domain.StartsWith("https://"))
            {
                api.Domain = "https://" + api.Domain; // This limits it to only https websites tho
            }

            // Create a new Uri object from the url
            Uri myUri = new(api.Domain);
            // Get the IP addressArray from the Uri object
            IPAddress[] ip = Dns.GetHostAddresses(myUri.Host);
            // Print the IP addressArray to the console, debug purposes
            foreach (IPAddress ipAddress in ip)
            {
                Console.WriteLine(ipAddress.ToString());
            }
            // Return the first IP address in the array, which is the ipv4 
            api.IP = Convert.ToString(ip[0]);
        }

        public static List<API> ApiReq(API api)
        {
            GetUrlIp(api); // Get the IP address from the domain name
            List<API>? content = new();
            // Initialise a new RestSharp client to the API endpoint
            RestClient client = new("https://api.abuseipdb.com/api/v2/check");
            // Initialise a new RestSharp request to the API endpoint
            RestRequest request = new();
            // Add the API key to the request header
            request.AddHeader("Key", "87de98a405dd35a259ebf321ed41d7c7906417cac5d3a693dc0e2371d03e4a06d2ef29c814ff4b09");
            // Add the Accept header to the request header to specify the response format
            request.AddHeader("Accept", "application/json");
            // Add the IP address to the request parameters
            request.AddParameter("ipAddress", api.IP);
            // Add the maxAgeInDays parameter to the request parameters, might turn it into a variable later
            
            request.AddParameter("maxAgeInDays", api.Days = api.Days == "0" ? "90" : api.Days);
            // Add the verbose parameter to the request parameters so we get the full response
            request.AddParameter("verbose", "");
            // Execute the request and store the response
            RestResponse response = client.Execute(request);
            // Add the response to the content list
            content.Add(JsonConvert.DeserializeObject<API>(response.Content));

            return content;
        }
    }
}
