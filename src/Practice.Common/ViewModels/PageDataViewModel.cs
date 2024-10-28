namespace Practice.Common.ViewModels
{
    public class PageDataViewModel<T>
    {
        public PageDataViewModel()
        {
            this.List = Array.Empty<T>();
        }

        public int TotalCount { get; set; }

        public IEnumerable<T> List { get; set; }
    }
}
