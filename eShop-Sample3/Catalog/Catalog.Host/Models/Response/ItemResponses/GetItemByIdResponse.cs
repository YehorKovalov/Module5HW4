namespace Catalog.Host.Models.Response.ItemResponses
{
    public class GetItemByIdResponse<T>
    {
        public T Item { get; set; } = default(T) !;
    }
}
