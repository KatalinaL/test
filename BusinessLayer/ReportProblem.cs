using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class ReportProblem
    {
        [Key]
        public string Id { get; set; }
        public string ReporterId { get; set; }
        public string Message { get; set; }
        public DateTime DateReported { get; set; }
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        // Конструктор, който задава стойности по подразбиране
        public ReportProblem()
        {
            Id = Guid.NewGuid().ToString();
            DateReported = DateTime.UtcNow;
        }
    }
}
