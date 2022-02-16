namespace Catalog.Host.Models.Response.ItemResponses
{
    public class RemoveItemResponse<T>
    {
        public T Id { get; set; } = default(T) !;
    }
}
