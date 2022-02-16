namespace Catalog.Host.Models.Response.BrandResponses
{
    public class UpdateBrandResponse<T>
    {
        public T Id { get; set; } = default(T) !;
    }
}
