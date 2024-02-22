namespace TopSaladSolution.DataAccess.Common.Paging
{
    public class PaginateResultData<T>
    {
        public int TotalCounts { get; set; }
        public double TotalPages { get; set; }
        public ICollection<T> Records { get; set; }
    }
}
