using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineDoctorProject.DataLayer
{
    public class ProblemReportContext
    {
        private readonly MedicalDbContext _dbContext;

        private DbSet<ReportProblem> ProblemReports => _dbContext.Set<ReportProblem>();

        public ProblemReportContext(MedicalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddProblemReportAsync(ReportProblem report)
        {
            ProblemReports.Add(report);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<ReportProblem>> GetProblemsForPatientAsync(string patientId)
        {
            return await ProblemReports
                .Include(p => p.Doctor) // Зареждаме лекаря (User)
                .Where(p => p.ReporterId == patientId)
                .ToListAsync();
        }

        public async Task<List<ReportProblem>> GetAllProblemReportsAsync()
        {
            return await ProblemReports.Include(p => p.Doctor).ToListAsync();
        }

        public async Task<bool> AssignDoctorAsync(string problemReportId, string doctorId)
        {
            var report = await ProblemReports.FirstOrDefaultAsync(r => r.Id == problemReportId);
            if (report == null) return false;

            report.DoctorId = doctorId;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<ReportProblem?> GetProblemByIdAsync(string problemId)
        {
            return await ProblemReports.FirstOrDefaultAsync(p => p.Id == problemId);
        }

        public async Task UpdateProblemAsync(ReportProblem problem)
        {
            _dbContext.ReportProblems.Update(problem);
            await _dbContext.SaveChangesAsync();
        }
    }
}