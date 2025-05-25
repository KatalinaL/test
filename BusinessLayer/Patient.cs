using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Patient : User
    {


        
        public ICollection<Appointment> Appointments { get; set; }
        public Patient()
        {
            
            Appointments = new List<Appointment>();
        }
        public Patient(string fname, string secname, string lname, string username, DateTime birthdate, string email, string phone) : base(fname, secname, lname, username, birthdate, email, phone)
        {
            
            Appointments = new List<Appointment>();
        }


    }

}
