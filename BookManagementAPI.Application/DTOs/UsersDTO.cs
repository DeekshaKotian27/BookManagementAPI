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
        public string? Role { get; set; }
    }

    public class UpdateUserNameDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }

    public class UpdateUserEmailDTO
    {
        public int Id { get; set; }
        public string EmailID { get; set; }
    }

    public class UpdateUserPassword
    {
        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
