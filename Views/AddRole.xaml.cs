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
    /// Interaction logic for AddRole.xaml
    /// </summary>
    public partial class AddRole : Window
    {
        DonationRecordController doncontroller = new DonationRecordController();
        string error = "";
        public AddRole()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            bool ret = doncontroller.deleteRole(CommonFactory.selRole.name, out error);
            if (ret)
            {
                Role role = new Role
                {
                    name = txt_name.Text.ToString(),
                    isactive = true
                };
                ret = doncontroller.saveRole(role, out error);
                if (ret)
                {
                    MessageBox.Show("Successfully Save", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Try Again", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (CommonFactory.selRole.name != null)
            {
                txt_name.Text = CommonFactory.selRole.name;
                btn_delete.IsEnabled = true;
            }
            else
            {
                txt_name.Text = "";
                btn_delete.IsEnabled = false;
            }
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            bool ret = doncontroller.deleteRole(CommonFactory.selRole.name, out error);
            if (ret)
            {
                MessageBox.Show("Successfully Delete", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Try Again", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
