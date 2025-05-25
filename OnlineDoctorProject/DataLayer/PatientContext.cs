using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineDoctorProject.DataLayer
{
    public class PatientContext : IDb<Patient, string>
    {
        private readonly MedicalDbContext dbContext;

        public PatientContext(MedicalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Patient item)
        {
            try
            {
                dbContext.Patients.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Patient> ReadAsync(string key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Patient> query = dbContext.Patients;

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

        public async Task<List<Patient>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Patient> query = dbContext.Patients;

                if (useNavigationalProperties)
                {
                    query = query.Include(a => a.Appointments);

                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Patient item, bool useNavigationalProperties = false)
        {
            try
            {
                dbContext.Patients.Update(item);
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
                Patient patientFromDb = await ReadAsync(key, false, false);

                if (patientFromDb is null)
                {
                    throw new ArgumentException("Patient with that Id does not exist!");
                }

                dbContext.Patients.Remove(patientFromDb);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
