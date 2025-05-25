using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineDoctorProject.DataLayer
{
    public class DoctorContext : IDb<Doctor, string>
    {
        private readonly MedicalDbContext dbContext;

        public DoctorContext(MedicalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<DoctorRating>> GetAllRatingsAsync()
        {
            using var context = new MedicalDbContext(); // или как се казва при теб
            return await context.DoctorRatings.ToListAsync();
        }
        public async Task CreateAsync(Doctor item)
        {
            try
            {
                dbContext.Doctors.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Doctor> ReadAsync(string key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Doctor> query = dbContext.Doctors;

                if (useNavigationalProperties)
                {
                    query = query.Include(a => a.Appointments);


                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(a => a.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Doctor>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                // Стартираме заявката от базата данни без проекция
                IQueryable<User> query = dbContext.Users.Where(u => u.Role == Role.Doctor);

              

                // След това проектираме към Doctor. Ако имаш нужда и от останалите навигационни свойства, ги прехвърли.
                IQueryable<Doctor> doctorQuery = query.Select(u => new Doctor
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    SecondName = u.SecondName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Birthdate = u.Birthdate,
                    DoctorSpecialty = u.DoctorSpecialty
                });

                if (isReadOnly)
                {
                    doctorQuery = doctorQuery.AsNoTrackingWithIdentityResolution();
                }

                return await doctorQuery.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task UpdateAsync(Doctor item, bool useNavigationalProperties = false)
        {
            try
            {
                // Проверяваме дали контекстът вече проследява обект със същото Id
                var localEntry = dbContext.ChangeTracker.Entries<Doctor>()
                                    .FirstOrDefault(e => e.Entity.Id == item.Id);
                if (localEntry != null)
                {
                    // Ако има такъв, отделяме го
                    localEntry.State = EntityState.Detached;
                }

                // Прикачваме актуализирания обект и задаваме състоянието му като Modified
                dbContext.Attach(item);
                dbContext.Entry(item).State = EntityState.Modified;

                await dbContext.SaveChangesAsync();
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
                Doctor doctorFromDb = await ReadAsync(key, false, false);

                if (doctorFromDb is null)
                {
                    throw new ArgumentException("Doctor with that Id does not exist!");
                }

                dbContext.Doctors.Remove(doctorFromDb);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Doctor> ReadByNameAsync(string name)
        {
            // Търсим потребител с дадено потребителско име и role == 3 (което означава, че е доктор)
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName.ToLower() == name.ToLower() && u.Role == Role.Doctor);
            if (user == null)
                return null;

            // Опитваме се да направим cast към Doctor
            Doctor doctor = user as Doctor;
            if (doctor == null)
            {
                // Ако cast-ът върне null, правим ръчна мапировка
                doctor = new Doctor
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Birthdate = user.Birthdate,
                    DoctorSpecialty = user.DoctorSpecialty
                    // Ако имаш допълнителни свойства за Doctor (като DoctorFloor, DoctorRoom и т.н.),
                    // можеш да ги присвоиш тук, ако са налични в user.
                };
            }
            return doctor;
        }

        public async Task<List<Doctor>> GetDoctorsAsync()
        {
            // Ако използвате EF Core и записите са в таблицата Users, филтрирайте по Role
            return await dbContext.Users
                 .Where(u => u.Role == Role.Doctor)
                 .Select(u => new Doctor
                 {
                     Id = u.Id,
                     FirstName = u.FirstName,
                     LastName = u.LastName,
                     // Други свойства, ако има
                 })
                 .ToListAsync();
        }
    }
}
