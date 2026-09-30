namespace CountriesManagementSystem.Shared.SharedDTOs
{
    public class PageListResult<TEntity>
    {
        public List<TEntity> DataList { get; set; }
        public long TotalCount { get; set; }
        public PageListResult()
        {

        }
        public PageListResult(List<TEntity> dataList, long totalCount)
        {
            DataList = dataList;
            TotalCount = totalCount;
        }
    }
}
