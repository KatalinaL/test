using BusinessLayer;
using OnlineDoctorProject.DataLayer;
using System.Security.Claims;

namespace ServiceLayer
{
    public class IdentityManager
    {
        private readonly IdentityContext _identityContext;

        public IdentityManager(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }


        public async Task CreateUserAsync(User user)
        {
            await _identityContext.CreateUserAsync(user);
        }

        public async Task<ClaimsPrincipal> LogInAsync(string username, string password)
        {
            return await _identityContext.LogInAsync(username, password);
        }

        public async Task<StartLogin> LogInAsync2(string username, string password)
        {
            return await _identityContext.LogInAsync2(username, password);
        }

        public async Task<ClaimsPrincipal> LogOutAsync(ClaimsPrincipal claimsPrincipal)
        {
            return await _identityContext.LogOutAsync(claimsPrincipal);
        }
        public async Task<User> ReadAsync(string key, bool useNavigationalProperties = false)
        {
            return await _identityContext.ReadAsync(key, useNavigationalProperties);
        }

        public async Task<ICollection<User>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            return await _identityContext.ReadAllAsync(useNavigationalProperties);
        }

        public async Task UpdateAsync(User user)
        {
            await _identityContext.UpdateAsync(user);
        }

        public async Task DeleteByNameAsync(string name)
        {
            await _identityContext.DeleteByNameAsync(name);
        }

        public async Task DeleteAsync(string key)
        {
            await _identityContext.DeleteByNameAsync(key);
        }

        public async Task<User> FindByNameAsync(string name)
        {
            return await _identityContext.FindByNameAsync(name);
        }
    }
}
