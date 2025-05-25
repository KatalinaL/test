using BusinessLayer;
using OnlineDoctorProject.DataLayer;

namespace ServiceLayer
{
    public class AppointmentManager
    {
        private readonly AppointmentContext appointmentContext;

        public AppointmentManager(AppointmentContext appointmentContext)
        {
            this.appointmentContext = appointmentContext;
        }

        public async Task<Appointment> GetAppointmentByDateTime(DateTime date, TimeOnly startTime, TimeOnly endTime, string doctorId)
        {
            return await appointmentContext.GetAppointmentByDateTime(date, startTime, endTime, doctorId);
        }

        public async Task CreateAsync(Appointment item)
        {
            await appointmentContext.CreateAsync(item);

        }

        public async Task<Appointment> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await appointmentContext.ReadAsync(key, useNavigationalProperties, isReadOnly);
        }

        public async Task<ICollection<Appointment>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await appointmentContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Appointment item, bool useNavigationalProperties = false)
        {
            await appointmentContext.UpdateAsync(item, useNavigationalProperties);
        }

        public async Task DeleteAsync(int key)
        {
            await appointmentContext.DeleteAsync(key);
        }
    }
}
