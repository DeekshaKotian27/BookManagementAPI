namespace BookManagementAPI.Application.DTOs
{
    public class UsersDTO
    {
        public string EmailID { get; set; }
        public string Password { get; set; }
    }

    public class UsersRegisterDTO
    {
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string? UserName { get; set; }
    }


}
