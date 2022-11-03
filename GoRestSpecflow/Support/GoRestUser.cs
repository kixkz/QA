using Newtonsoft.Json;
using static Bogus.DataSets.Name;

namespace GoRestSpecflow.Support
{
    public class GoRestUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("gender")]
        public Gender Gender { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
