namespace Catalog.Host.Models.Response.BrandResponses
{
    public class AddBrandResponse<T>
    {
        public T Id { get; set; } = default(T) !;
    }
}
