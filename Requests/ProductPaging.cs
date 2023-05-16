using DUCtrongAPI.Repositories.EmplementedRepository.Paging;

namespace DUCtrongAPI.Requests
{
    public class ProductPaging : PagingRequestBase
    {
        public string? ProductName { get; set; }
    }
}
