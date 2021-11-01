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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MdyBloolDonation.Views
{
    /// <summary>
    /// Interaction logic for StaffInfoView.xaml
    /// </summary>
    public partial class StaffInfoView : UserControl
    {
        DonationRecordController donController = new DonationRecordController();
        string error = "";
        List<Staff> stafflist = new List<Staff>();
        List<Role> rolelist = new List<Role>();
        private Staff selStaff = new Staff();
        private bool isNew = true;
        public StaffInfoView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearData();
        }

        private void ClearData()
        {
            grdStaffList.ItemsSource = null;
            stafflist = donController.getStaffList("%", out error);
            grdStaffList.ItemsSource = stafflist;

            grdRolelist.ItemsSource = null;
            rolelist = donController.getRoleList("%", out error);
            grdRolelist.ItemsSource = rolelist;

            cb_role.ItemsSource = rolelist;
            cb_role.SelectedValuePath = "name";
            cb_role.DisplayMemberPath = "name";
            cb_role.SelectedIndex = 0;

            txt_name.Text = "";
            txt_address.Text = "";
            txt_email.Text = "";
            txt_password.Text = "";
            txt_phoneno.Text = "";
            isNew = true;
        }

        private void grdStaffList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdStaffList.SelectedItem != null)
            {
                selStaff = (Staff)grdStaffList.SelectedItem;
                if (selStaff.name != null)
                {
                    isNew = false;
                    EditFunction(selStaff);
                }
            }
        }

        private void EditFunction(Staff selStaff)
        {
            txt_name.Text = selStaff.name;
            txt_address.Text = selStaff.address;
            txt_email.Text = selStaff.email;
            txt_password.Text = selStaff.password;
            txt_phoneno.Text = selStaff.phoneno;

            int index = rolelist.FindIndex(x => x.name == selStaff.role);
            cb_role.ItemsSource = rolelist;
            cb_role.SelectedValuePath = "name";
            cb_role.DisplayMemberPath = "name";
            cb_role.SelectedIndex = index;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (txt_name.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Valid Staff Name", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_name.Focus();
                return;
            }
            if (cb_role.SelectedValue == null)
            {
                MessageBox.Show("Please select role", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                cb_role.Focus();
                return;
            }
            if (txt_password.Text.Trim() == "")
            {
                MessageBox.Show("Please fill password to Login", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_password.Focus();
                return;
            }
            selStaff.name = txt_name.Text.ToString();
            selStaff.role = cb_role.SelectedValue.ToString();
            selStaff.email = txt_email.Text.ToString();
            selStaff.password = txt_password.Text.ToString();
            selStaff.phoneno = txt_phoneno.Text.ToString();
            selStaff.address = txt_address.Text.ToString();
            selStaff.isactive = true;
            if (isNew)
                selStaff.createddate = DateTime.Now;
            bool result = donController.saveStaff(selStaff, out error);
            if (result)
            {
                MessageBox.Show("Save Successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearData();
                return;
            }
            else
            {
                MessageBox.Show("Try Again!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (selStaff.name != null)
            {
                bool ret = donController.deleteStaff(selStaff.name, out error);
                if (ret)
                {
                    MessageBox.Show("Successfully Delete", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearData();
                    return;
                }
                else
                {
                    MessageBox.Show("Failed to delete", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);                    
                    return;
                }
            }
        }

        private void btn_role_add_Click(object sender, RoutedEventArgs e)
        {
            CommonFactory.selRole = new Role();
            AddRole add = new AddRole();
            add.ShowDialog();
            ClearData();
        }

        private void grdRolelist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdRolelist.SelectedItem != null)
            {
                Role role = (Role)grdRolelist.SelectedItem;
                if (role.name != null)
                {
                    CommonFactory.selRole = role;
                    AddRole add = new AddRole();
                    add.ShowDialog();
                    ClearData();
                }
            }
        }
    }
}
