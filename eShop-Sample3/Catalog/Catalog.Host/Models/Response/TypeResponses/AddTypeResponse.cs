namespace Catalog.Host.Models.Response.TypeResponses
{
    public class AddTypeResponse<T>
    {
        public T Id { get; set; } = default(T) !;
    }
}
