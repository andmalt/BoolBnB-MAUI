using BoolBnB_MAUI.Data.Entites;
using System.Text;
using System.Threading.Tasks;

namespace BoolBnB_MAUI.Data.Guest
{
    public class HouseResponse
    {
        public Apartment Apartment {  get; set; }
        public bool Success { get; set; }
    }
}
