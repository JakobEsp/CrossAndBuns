using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Runtime.CompilerServices;

namespace CB.Data
{
    public class API
    {
        public int Id { get; set; }
        public string[]? IP { get; set; }
        public string[]? Domain { get; set; }

        public List<string> Text { get; set; } = new();

        private string? days { get; set; }

        public string? Days
        {
            get { return days; }

            set {
                if (value == null)
                    days = "0";
                else
                    days = value;
            }
        }

        private readonly int _id = 0;

        public API()
        {
            Id = Interlocked.Increment(ref _id);
        }

    }
}
