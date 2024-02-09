
using BoolBnB_MAUI.Services;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace BoolBnB_MAUI.Data.Entites
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set;}
        public short Rooms { get; set; }
        public short Beds { get; set; }
        public short Bathrooms { get; set; }
        public short Visible { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Lat {  get; set; }
        public string Lon { get; set; }
        public string Price { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("update_at")]
        public DateTime UpdatedAt { get; set;}

        [MaybeNull]
        public ObservableCollection<Photo> Photos { get; set; }
        public string FirstPhotoUrl
        {
            get
            {
                if (Photos != null && Photos.Any())
                {
                    //Console.WriteLine(string.Format(HttpService.BaseUrl, $"/{Photos.First().ImageUrl}"));
                    var firstPhotoUrl = Photos.First().ImageUrl;
                    // Check if the URL is already complete
                    if (firstPhotoUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                        || firstPhotoUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                    {
                        return firstPhotoUrl;
                    }
                    else
                    {
                        // If not, prepend the BaseUrl
                        return string.Format(HttpService.BaseUrl, $"/storage/apartments/images/{Photos.First().ImageUrl}");
                    }
                }
                else
                {
                    return "home.png"; // default image
                }
            }
        }
    }
}
