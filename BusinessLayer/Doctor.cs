using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Doctor : User
    {
        public int DoctorFloor { get; set; }
        public int DoctorRoom { get; set; }
        public byte[] ProfilePic { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public Doctor()
        {
            Appointments = new List<Appointment>();
        }
        public Doctor(string fname, string secname, string lname, string username, DateTime birthdate, string email, string phone,
            int floor, int room, Specialty specialty) : base(fname, secname, lname, username, birthdate, email, phone)
        {
            this.DoctorFloor = floor;
            this.DoctorRoom = room;
            this.DoctorSpecialty = specialty;
            Appointments = new List<Appointment>();
        }


    }
}
