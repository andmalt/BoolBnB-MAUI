
using System.Diagnostics.CodeAnalysis;

namespace BoolBnB_MAUI.Data.Entites
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        [MaybeNull]
        public ICollection<Photo> Photos { get; set; }
    }
}
