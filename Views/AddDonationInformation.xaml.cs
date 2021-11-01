using MdyBloolDonation.Common;
using MdyBloolDonation.Controllers;
using MdyBloolDonation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MdyBloolDonation.Views
{
    /// <summary>
    /// Interaction logic for AddDonationInformation.xaml
    /// </summary>
    public partial class AddDonationInformation : Window
    {
        BloodDonor sel_donor = new BloodDonor();
        DonationRecordController drcontroller = new DonationRecordController();
        string error = "";
        public static bool state = true;
        public AddDonationInformation()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_id.Text = CommonFactory.selDonor.d_id;
            txt_id.IsEnabled = false;
            txt_name.Text = CommonFactory.selDonor.d_name;
            txt_name.IsEnabled = false;
            txt_remarks.Focus();
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            bool ret = drcontroller.saveDonationTransaction(CommonFactory.selDonor.d_id,
                DateTime.Now,txt_remarks.Text.ToString(),out error);
            if (ret)
            {
                state = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Try again, the transaction has not saved.", "Try Again", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
    }
}
