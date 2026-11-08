using WebApiP41.Models;

namespace WebApiP41.Services;

public class UserService(IDataProvider<User> userProvider)
{
    public async Task<User> RegisterUserAsync(User newUser)
    {
        var users = await userProvider.GetAllAsync();
        if (users.Any(p => p.Email.Equals(newUser.Email, StringComparison.OrdinalIgnoreCase)))
        {
            throw new Exception("User already exists");
        }
        newUser.Token = Guid.NewGuid().ToString();
        users.Add(newUser);
        await userProvider.SaveAsync(users);
        return newUser;
    }

    public async Task<User?> AuthenticateAsync(string email, string password) =>
        (await userProvider.GetAllAsync()).FirstOrDefault(p =>
            p.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && p.Password.Equals(password));
    
    public async Task<User?> GetUserByTokenAsync(string token) =>
        (await userProvider.GetAllAsync()).FirstOrDefault(p => p.Token.Equals(token));
}