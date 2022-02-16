namespace Catalog.Host.Models.Response.TypeResponses
{
    public class GetTypesResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = null!;
    }
}
