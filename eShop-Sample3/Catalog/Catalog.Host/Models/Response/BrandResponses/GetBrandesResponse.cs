namespace Catalog.Host.Models.Response.BrandResponses
{
    public class GetBrandesResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = null!;
    }
}
