using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public enum Role
    {
        [Display(Name = "Избери роля")]
        Administrator,
        [Display(Name = "Специалист")]
        Doctor,
        [Display(Name = "Лаборант")]
        LabSpecialist,
        [Display(Name = "Пациент")]
        Patient
    }
}
