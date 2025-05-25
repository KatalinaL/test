using BusinessLayer;
using OnlineDoctorProject.DataLayer;

namespace ServiceLayer
{
    public class PatientManager
    {
        private readonly PatientContext patientContext;

        public PatientManager(PatientContext patientContext)
        {
            this.patientContext = patientContext;
        }

        public async Task CreateAsync(Patient patient)
        {
            await patientContext.CreateAsync(patient);
        }

        public async Task<Patient> ReadAsync(string key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await patientContext.ReadAsync(key, useNavigationalProperties);
        }

        public async Task<ICollection<Patient>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await patientContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Patient patient, bool useNavigationalProperties = false)
        {
            await patientContext.UpdateAsync(patient, useNavigationalProperties);
        }

        public async Task DeleteAsync(string key)
        {
            await patientContext.DeleteAsync(key);
        }
    }
}
