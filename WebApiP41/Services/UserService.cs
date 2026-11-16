using Microsoft.EntityFrameworkCore;
using WebApiP41.Models;

namespace WebApiP41.Services;

public class UserService(ApiDbContext dbContext)
{
    public async Task<User> RegisterUserAsync(User newUser)
    {
        if (await dbContext.Users
                .AnyAsync(x => x.Email == newUser.Email))
        {
            throw new Exception("User already exists");
        }
        newUser.Token = Guid.NewGuid().ToString();
        await dbContext.Users.AddAsync(newUser);
        await dbContext.SaveChangesAsync();
        return newUser;
    }

    public async Task<User?> AuthenticateAsync(string email, string password) =>
        await dbContext.Users.FirstOrDefaultAsync(p =>
            p.Email.ToLower() == email.ToLower() && p.Password == password);
    
    public async Task<User?> GetUserByTokenAsync(string token) =>
        await dbContext.Users.FirstOrDefaultAsync(p => p.Token.Equals(token));

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetUserByName(string name)
    {
        var users = await dbContext.Users.ToListAsync();
        return users.FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}