namespace Catalog.Host.Models.Response.TypeResponses
{
    public class UpdateTypeResponse<T>
    {
        public T Id { get; set; } = default(T) !;
    }
}
