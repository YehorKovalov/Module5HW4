namespace Catalog.Host.Models.Response.ItemResponses
{
    public class SameTypeItemsResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = null!;
    }
}
