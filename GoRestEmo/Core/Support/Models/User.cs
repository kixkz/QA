using Newtonsoft.Json;
using static Bogus.DataSets.Name;

namespace GoRestEmo.Core.Support.Models
{
    public class User
    {
        
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
