namespace EShopNative.DataTransferObject
{
    public class UserDTO { }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string ?Username { get; set; }
        public string ?Email { get; set; }
        public string ?Password { get; set; }
        public string ?FullName { get; set; }
    }

    public class LoginResponse
    {
        public string Message { get; set; }
        public UserDto User { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }

}
