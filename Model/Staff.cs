using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdyBloolDonation.Model
{
    public class Staff
    {
        public string name { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phoneno { get; set; }
        public string address { get; set; }
        public bool isactive { get; set; }
        public DateTime createddate { get; set; }
    }
}
