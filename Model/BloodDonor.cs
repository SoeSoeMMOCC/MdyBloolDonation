using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdyBloolDonation.Model
{
    public class BloodDonor
    {
        public string d_id { get; set; }
        public string d_name { get; set; }
        public DateTime startdate { get; set; }
        public DateTime lastdate { get; set; }
        public string address { get; set; }
        public string phoneno { get; set; }
        public string email { get; set; }
        public int times { get; set; }
        public string times_name { get; set; }
        public string bloodtype_id { get; set; }
        public string gender_id { get; set; }
        public string bloodtype { get; set; }
        public string gender { get; set; }
        public bool isactive { get; set; }
        public DateTime ts { get; set; }
        public int age { get; set; }
        public DateTime birthdate { get; set; }
    }
}
