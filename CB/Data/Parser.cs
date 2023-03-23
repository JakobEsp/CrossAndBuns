using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CB.Data
{
    /*
     * Vi prøvede at få fat i div. properties fra json filen
     * Efter mange forsøg måtte vi forgæves opgive
     * Du kan se her hvordan vi opstillede vores klasser
    */
    public class Parser
    {
        [JsonProperty("isPublic")]
        public bool isPublic { get; set; }
        [JsonProperty("hostnames")]
        public string? hostnames { get; set; }
        [JsonProperty("totalReports")]
        public int totalReports { get; set; }

        public int numDistinctUsers { get; set; }

    }
    public class ParseList
    {
        public List<Parser> parsers { get; set; } = new();

    }
    public class Root
    {
        public ParseList parse { get; set; }
    }
}
