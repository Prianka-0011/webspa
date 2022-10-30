namespace DatingApp.Helpers
{
    public class PaginationHeader
    {
        public PaginationHeader(int currentPage, int itemPerPage,int totalItem, int totalPages )
        {
            CurrentPage= currentPage;
            ItemPerPage= itemPerPage;
            TotalItem= totalItem;
            TotalPages= totalPages;
        }
        public int CurrentPage { get; set; }
        public int ItemPerPage { get; set; }
        public int TotalItem { get; set; }
        public int TotalPages { get; set; }
    }
}
