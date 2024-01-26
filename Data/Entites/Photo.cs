using System.Text.Json.Serialization;

namespace BoolBnB_MAUI.Data.Entites
{
    public class Photo
    {
        public int Id { get; set; }

        [JsonPropertyName("apartment_id")]
        public int ApartmentId { get; set; }

        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
