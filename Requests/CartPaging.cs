using DUCtrongAPI.Repositories.EmplementedRepository.Paging;

namespace DUCtrongAPI.Requests
{
    public class CartPaging : PagingRequestBase
    {
       public string userid { get; set; }
    }
}
