using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CB.Data
{
    public class Parser
    {
        [JsonPropertyName("isPublic")]
        public bool isPublic { get; set; }

        [JsonPropertyName("hostnames")]
        public string? hostnames { get; set; }

        [JsonPropertyName("isp")]
        public string? isp { get; set; }

        [JsonPropertyName("totalReports")]
        public int totalReports { get; set; }

        [JsonPropertyName("numDistinctUsers")]
        public int DistUsers { get; set; }


    }
}