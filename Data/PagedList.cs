
namespace BoolBnB_MAUI.Data
{
    public class PagedList<T>
    {
        public int CurrentPage { get; set; }
        public IList<T> Data { get; set; }
        public string NextPageUrl { get; set; }
        public string Path { get; set; }
        public int PerPage { get; set; }
        public string PrevPageUrl { get; set; }
        public int To {  get; set; }
        public int Total { get; set; }
    }
}
