using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BusinessLayer
{
    public class User : IdentityUser
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your first name.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = "";

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your middle name.")]
        [Display(Name = "Middle Name")]
        public string SecondName { get; set; } = "";

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your last name.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = "";
        public Specialty DoctorSpecialty { get; set; } = Specialty.Празно;
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime Birthdate { get; set; }

        [Required]
        public Role Role { get; set; }
        public User()
        {

        }
        public User(string username)
        {
            Id = Guid.NewGuid().ToString();
            UserName = username;
        }
        public User(string fname, string secname, string lname, string username, DateTime birthdate, string email, string phone)
        {
            FirstName = fname;
            SecondName = secname;
            LastName = lname;
            UserName = username;
            Birthdate = birthdate;
            Email = email;
            PhoneNumber = phone;
        }
    }
}
