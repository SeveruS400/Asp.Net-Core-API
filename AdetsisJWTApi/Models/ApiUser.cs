namespace Adetsis_JWT.Models
{
    public class ApiUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? Role { get; set; }
        public string Password { get; set; } = string.Empty;

    }
}
