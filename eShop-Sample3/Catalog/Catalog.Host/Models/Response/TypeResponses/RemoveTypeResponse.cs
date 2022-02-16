namespace Catalog.Host.Models.Response.TypeResponses
{
    public class RemoveTypeResponse<T>
    {
        public T Id { get; set; } = default(T) !;
    }
}
