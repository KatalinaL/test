using BusinessLayer;
using OnlineDoctorProject.DataLayer;

namespace ServiceLayer
{
    public class DoctorManager
    {
        private readonly DoctorContext doctorContext;

        public DoctorManager(DoctorContext doctorContext)
        {
            this.doctorContext = doctorContext;
        }

        public async Task CreateAsync(Doctor doctor)
        {
            await doctorContext.CreateAsync(doctor);
        }

        public async Task<Doctor> ReadAsync(string key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await doctorContext.ReadAsync(key, useNavigationalProperties);
        }

        public async Task<List<Doctor>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await doctorContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Doctor doctor, bool useNavigationalProperties = false)
        {
            await doctorContext.UpdateAsync(doctor, useNavigationalProperties);
        }

        public async Task DeleteAsync(string key)
        {
            await doctorContext.DeleteAsync(key);
        }


    }
}
