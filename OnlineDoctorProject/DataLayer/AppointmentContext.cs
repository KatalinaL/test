using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineDoctorProject.DataLayer
{
    public class AppointmentContext : IDb<Appointment, int>
    {
        private readonly MedicalDbContext dbContext;

        public AppointmentContext(MedicalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Appointment> GetAppointmentByDateTime(DateTime date, TimeOnly startTime, TimeOnly endTime, string doctorId)
        {
            try
            {
                // Query for the appointment with the specified date, time, and doctor
                IQueryable<Appointment> query = dbContext.Appointments
                    .Where(a => a.Date.Date == date.Date && a.StartTime == startTime && a.EndTime == endTime && a.DoctorId == doctorId);

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateAsync(Appointment item)
        {
            try
            {
                dbContext.Appointments.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Appointment> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Appointment> query = dbContext.Appointments;

                if (useNavigationalProperties)
                {
                    query = query.Include(a => a.Doctor)
                                 .Include(a => a.Patient);

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

        public async Task<List<Appointment>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Appointment> query = dbContext.Appointments;

                if (useNavigationalProperties)
                {
                    query = query.Include(a => a.Doctor)
                                 .Include(a => a.Patient);

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

        public async Task UpdateAsync(Appointment item, bool useNavigationalProperties = false)
        {
            try
            {
                dbContext.Appointments.Update(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                Appointment appointmentFromDb = await ReadAsync(key, false, false);

                if (appointmentFromDb is null)
                {
                    throw new ArgumentException("Appointment with that Id does not exist!");
                }

                dbContext.Appointments.Remove(appointmentFromDb);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
