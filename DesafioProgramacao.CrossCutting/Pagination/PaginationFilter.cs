namespace DesafioProgramacao.CrossCutting.Pagination
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Description { get; set; }
        public string OrderBy { get; set; } = "asc";

        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize;
        }

        public int CalcSkip() => (PageNumber - 1) * PageSize;
    }
}
