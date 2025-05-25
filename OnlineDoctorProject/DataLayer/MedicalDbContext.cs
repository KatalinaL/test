using BusinessLayer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineDoctorProject.DataLayer
{
    public class MedicalDbContext : IdentityDbContext<User>
    {

       
        public MedicalDbContext() : base()
        {

        }

        public MedicalDbContext(DbContextOptions<MedicalDbContext> options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Studio 2014 Home
                //optionsBuilder.UseSqlServer("Server=DESKTOP-9UR8KMA\\SQLEXPRESS;Database=MedicalCityCentreDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;");

                // Studio 2019 Laptop
                //optionsBuilder.UseSqlServer("Server=DESKTOP-RV1J29O;Database=MedicalCityCentreDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;");

                // Studio 2014 Laptop ; 
                //optionsBuilder.UseSqlServer("Server=DESKTOP-RV1J29O\\SQLEXPRESS;Database=db_aa956b_medicaldbcenter;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;");

                // 0nL!n3:
                //optionsBuilder.UseSqlServer("Data Source=SQL6030.site4now.net;Initial Catalog=db_aa956b_medicaldbcenter;User Id=db_aa956b_medicaldbcenter_admin;Password=SmarterVanko005");
                //loval DB
                optionsBuilder.UseSqlServer("Server=DESKTOP-QO0SHG7\\SQLEXPRESS;Database=MedicalCityCentreDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;");
            }
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

       
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<DoctorRating> DoctorRatings { get; set; }
        
        public DbSet<BusinessLayer.ReportProblem> ReportProblems { get; set; }

        public DbSet<Article> Articles { get; set; }
    }
}
