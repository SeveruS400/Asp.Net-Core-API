namespace Adetsis_JWT.Models;

public class ApiUsers
{
    public static List<ApiUser> Users = new()
        {
            new ApiUser { Id = 1, UserName = "asdfg", Password = "password", Role = "Admin" },
            new ApiUser { Id = 2, UserName = "ghjhg", Password = "password", Role = "User" },
            new ApiUser { Id = 3, UserName = "xcvbn", Password = "passwod", Role = "User" },
            new ApiUser { Id = 4, UserName = "erytu", Password = "passwrd", Role = "Admin" }
        };
}

