
using BoolBnB_MAUI.Data.Entites;

namespace BoolBnB_MAUI.Data.Guest
{
    public class HousesResponse
    {
        public ICollection<Apartment> Apartments { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public bool Success { get; set; }
    }
}
