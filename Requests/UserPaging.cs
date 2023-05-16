using DUCtrongAPI.Repositories.EmplementedRepository.Paging;

namespace DUCtrongAPI.Requests
{
    public class UserPaging : PagingRequestBase
    {
        public string ? NameOrPhone { get; set; }
    }
}
