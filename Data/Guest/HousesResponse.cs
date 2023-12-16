
using BoolBnB_MAUI.Data.Entites;

namespace BoolBnB_MAUI.Data.Guest
{
    public class HousesResponse
    {
        public PagedList<Apartment> Apartments { get; set; }
        public bool Success { get; set; }
    }
}
