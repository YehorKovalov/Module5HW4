namespace Catalog.Host.Models.Response.ItemResponses
{
    public class SameBrandItemsResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = null!;
    }
}
