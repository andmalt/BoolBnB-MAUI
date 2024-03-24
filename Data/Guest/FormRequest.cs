using System.Text.Json.Serialization;

namespace BoolBnB_MAUI.Data.Guest
{
    public class FormRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("message_content")]
        public string MessageContent { get; set; }

        [JsonPropertyName("houseId")]
        public int HouseId { get; set; }
    }
}
