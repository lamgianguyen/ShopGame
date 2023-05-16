namespace DUCtrongAPI.Requests
{
    public class UserViewPaging
    {
        //extend user models
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int? PhoneNumber { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
        
    }
}
