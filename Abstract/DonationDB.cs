using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdyBloolDonation.Abstract
{
    public class DonationDB
    {
        public IDbConnection DNDB()
        {
            SqlConnection conn = new SqlConnection(MdyBloolDonation.Properties.Settings.Default.DNDBConnection);
            return conn;
        }
    }
}
