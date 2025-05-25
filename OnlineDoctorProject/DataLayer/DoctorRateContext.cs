using Microsoft.EntityFrameworkCore;
using BusinessLayer; // Съдържа моделите (DoctorRating, Doctor и т.н.)
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OnlineDoctorProject.Components.Pages.DoctorCrud.DoctorRanking;

namespace OnlineDoctorProject.DataLayer
{
    public class DoctorRateContext
    {
        private readonly MedicalDbContext _dbContext;

        public DoctorRateContext(MedicalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DoctorRankingViewModel>> GetDoctorRankingAsync()
        {
            var query = from rating in _dbContext.DoctorRatings
                        join user in _dbContext.Users
                            on rating.DoctorId equals user.Id
                        group rating by new { user.FirstName, user.LastName, user.DoctorSpecialty } into g
                        select new DoctorRankingViewModel
                        {
                            DoctorName = g.Key.FirstName + " " + g.Key.LastName,
                            AverageStars = g.Average(r => r.Stars),
                            RatingCount = g.Count(),
                            DoctorSpecialty  = g.Key.DoctorSpecialty
                        };

            return await query
                .OrderByDescending(r => r.AverageStars)
                .ToListAsync();
        }
        public async Task<bool> AddRatingAsync(DoctorRating rating)
        {
            _dbContext.DoctorRatings.Add(rating);
            int affectedRows = await _dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }
    }
}
