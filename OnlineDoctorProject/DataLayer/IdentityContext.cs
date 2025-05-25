using BusinessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineDoctorProject.DataLayer
{
    public class IdentityContext
    {
        public readonly UserManager<User> userManager;
        public readonly SignInManager<User> signInManager;
        public readonly MedicalDbContext context;


        public IdentityContext(MedicalDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.context = context;
            this.signInManager = signInManager;
            this.userManager = userManager;

        }



        #region CRUD

        public async Task CreateUserAsync(User item)
        {
            try
            {
                await userManager.CreateAsync(item);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ClaimsPrincipal> LogInAsync(string username, string password)
        {
            try
            {
                User user = await userManager.FindByNameAsync(username);

                if (user == null)
                {
                    return null;
                }

                IdentityResult result = await userManager.PasswordValidators[0].ValidateAsync(userManager, user, password);

                if (result.Succeeded)
                {
                    User loggedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

                    if (loggedUser == null)
                    {
                        return null;
                    }

                    // IF you want to load the navigational properties (Appointments, Examinations...)
                    // make a switch or if-else and check the role, then call PatientContext.Read(..., true) or DoctorContext.Read(.., true)
                    // If admin - no need.
                    return await signInManager.CreateUserPrincipalAsync(loggedUser);

                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<StartLogin> LogInAsync2(string username, string password)
        {
            try
            {
                User user = await userManager.FindByNameAsync(username);

                if (user == null)
                {
                    return null;
                }

                bool result = await userManager.CheckPasswordAsync(user, password);

                if (result)
                {
                    // IF you want to load the navigational properties (Appointments, Examinations...)
                    // make a switch or if-else and check the role, then call PatientContext.Read(..., true) or DoctorContext.Read(.., true)
                    // If admin - no need.
                    return new StartLogin
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Role = user.Role
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ClaimsPrincipal> LogOutAsync(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity is not null && claimsPrincipal.Identity.IsAuthenticated)
            {
                return new ClaimsPrincipal();
            }

            // If should always be true when you call this method!
            return claimsPrincipal;
        }

        public async Task<User> ReadAsync(string key, bool useNavigationalProperties = false)
        {
            try
            {
                    return await userManager.FindByIdAsync(key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<User>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            try
            {
                return await context.Users.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(User item)
        {
            try
            {
                if (!string.IsNullOrEmpty(item.UserName))
                {
                    User user = await context.Users.FindAsync(item.Id);
                    user.UserName = item.UserName;
                    user.FirstName = item.FirstName;
                    user.SecondName = item.SecondName;
                    user.LastName = item.LastName;
                    user.Birthdate = item.Birthdate;
                    user.Email = item.Email;
                    user.PasswordHash = item.PasswordHash;
                    user.PhoneNumber = item.PhoneNumber;
                    user.Role = item.Role;
                    await userManager.UpdateAsync(user);
                }
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteByNameAsync(string name)
        {
            try
            {
                User user = await FindByNameAsync(name);

                if (user == null)
                {
                    throw new InvalidOperationException("User not found for deletion!");
                }

                await userManager.DeleteAsync(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(string key)
        {
            try
            {
                User userFromDb = await ReadAsync(key, false);

                if (userFromDb == null)
                {
                    throw new ArgumentException("A user with that key does not exist!");
                }

                context.Users.Remove(userFromDb);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> FindByNameAsync(string name)
        {
            try
            {
                // Identity return Null if there is no user!
                return await userManager.FindByNameAsync(name);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
