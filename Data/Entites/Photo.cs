
namespace BoolBnB_MAUI.Data.Entites
{
    public class Photo
    {
        public int Id { get; set; }
        public int ApartmentId {  get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
