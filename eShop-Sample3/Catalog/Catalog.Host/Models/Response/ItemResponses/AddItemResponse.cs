namespace Catalog.Host.Models.Response.ItemResponses;

public class AddItemResponse<T>
{
    public T Id { get; set; } = default(T) !;
}