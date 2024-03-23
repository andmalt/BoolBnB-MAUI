
using System.Text.Json.Serialization;

namespace BoolBnB_MAUI.Data
{
    public class PagedList<T>
    {
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        public IList<T> Data { get; set; }

        [JsonPropertyName("first_page_url")]
        public string FirstPageUrl { get; set; }

        public int From {  get; set; }

        [JsonPropertyName("last_page")]
        public int LastPage { get; set; }

        [JsonPropertyName("last_page_url")]
        public string LastPageUrl { get; set; }

        [JsonPropertyName("next_page_url")]
        public string NextPageUrl { get; set; }
        public string Path { get; set; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        [JsonPropertyName("prev_page_url")]
        public string PrevPageUrl { get; set; }
        public int To {  get; set; }
        public int Total { get; set; }
    }
}
