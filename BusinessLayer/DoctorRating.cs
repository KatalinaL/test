using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DoctorRating
    {
        public string Id { get; set; }
        public string DoctorId { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get; set; }
        // Чужд ключ към пациента, който дава оценката
        public string PatientId { get; set; }
        public Patient Patient { get; set; }

        // Оценка от 1 до 5 звезди
        public int Stars { get; set; }

        // Опционален коментар
        public string Comment { get; set; }

        // Дата и час на оценката
        public DateTime RatedAt { get; set; }
        public Specialty DoctorSpecialty { get; set; }
    }
}
