using Microsoft.EntityFrameworkCore;
using Models.Entities;
using ToDoLIstAPi.DbContext;

namespace ToDoLIstAPi;

public class AuthenticationService : IAuthenticationService
{
    private readonly ApplicationDbContext _context;

    public AuthenticationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> Authenticate(string username, string password)
    {
        var user = await _context.Set<User>().SingleOrDefaultAsync(u => u.Username == username);

        if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
            return null;

        return user; 
    }
    

    public bool VerifyPasswordHash(string password, string storedHash)
    {
        // Implement password verification logic (e.g., using BCrypt)
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }
}
