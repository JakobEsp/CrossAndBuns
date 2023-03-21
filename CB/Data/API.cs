using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Runtime.CompilerServices;

namespace CB.Data
{
    public class API
    {
        public string? IP { get; set; }
        public string? Domain { get; set; }

        public string? Days { get; set; }
    }
}
