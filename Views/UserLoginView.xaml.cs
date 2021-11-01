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
    /// Interaction logic for UserLoginView.xaml
    /// </summary>
    public partial class UserLoginView : Window
    {
        public bool status = true;
        DonationRecordController doncontroller = new DonationRecordController();
        string error = "";
        public UserLoginView()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            string staff_name = txt_name.Text.ToString().Trim();
            string staff_pass = txt_password.Password.ToString();
            List<Staff> stafflist = doncontroller.getLoginStaffList(staff_name, staff_pass, out error);
            if (stafflist.Count() > 0)
            {
                MessageBox.Show("Welcome,  " + staff_name, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                status = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect User Name or Password !", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
