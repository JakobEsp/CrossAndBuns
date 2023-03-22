using Newtonsoft.Json;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System.Net.Sockets;
using System;

namespace CB.Data
{
    public class Fetch
    {
        public static void GetUrlIp(API api)
        {
            // Initialise a new Uri object from the domain name
            Uri myUri = new(api.Domain);
            // Get the IP address from the domain name
            IPHostEntry host = Dns.GetHostEntry(myUri.Host);
            // Foreach IP address in the host entry
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily != AddressFamily.InterNetwork)
                {
                    continue;
                }

                // Check if api.ip list does not contains the current ip
                if (!api.IP.Contains(ip))
                {
                    // Add the ip
                    api.IP.Add(ip);
                }
            }
        }

        public static API? ApiReq(API api)
        {
            // Run the get url ip method with the instance as parameter
            GetUrlIp(api); 
            // Foreach loop every ip in the list
            foreach (IPAddress ip in api.IP)
            {
                // Initialise a new RestSharp client to the API endpoint
                RestClient client = new("https://api.abuseipdb.com/api/v2/check");
                // Initialise a new RestSharp request to the API endpoint
                RestRequest request = new();

                // Add the API key to the request header
                request.AddHeader("Key", "87de98a405dd35a259ebf321ed41d7c7906417cac5d3a693dc0e2371d03e4a06d2ef29c814ff4b09");

                // Add the Accept header to the request header to specify the response format
                request.AddHeader("Accept", "application/json");

                // Add the IP address to the request parameters
                request.AddParameter("ipAddress", Convert.ToString(ip));

                // Add the maxAgeInDays parameter to the request parameters, might turn it into a variable later
                request.AddParameter("maxAgeInDays", api.Days = api.Days == "0" ? "90" : api.Days);

                // Add the verbose parameter to the request parameters so we get the full response
                request.AddParameter("verbose", "");

                // Execute the request and store the response
                RestResponse response = client.Execute(request);

                // Check if response isn't null
                if (response != null)
                {
                    // Add the response content to the dynamic list
                    api.Text.Add(JsonConvert.DeserializeObject<Parser?>(response.Content));
                }
            }
            // Return our object
            return api;
        }
    }
}
