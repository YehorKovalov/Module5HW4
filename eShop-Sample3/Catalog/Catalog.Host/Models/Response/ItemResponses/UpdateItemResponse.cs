namespace Catalog.Host.Models.Response.ItemResponses
{
    public class UpdateItemResponse<T>
    {
        public T Id { get; set; } = default(T) !;
    }
}
