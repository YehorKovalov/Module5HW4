namespace Catalog.Host.Models.Requests.ItemRequests;

public class PaginatedItemsRequest
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }
}