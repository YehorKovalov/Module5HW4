namespace Catalog.Host.Models.Response.BrandResponses
{
    public class RemoveBrandResponse<T>
    {
        public T Id { get; set; } = default(T) !;
    }
}
