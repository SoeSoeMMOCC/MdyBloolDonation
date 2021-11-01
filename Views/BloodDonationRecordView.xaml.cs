using MdyBloolDonation.Common;
using MdyBloolDonation.Controllers;
using MdyBloolDonation.Model;
using Microsoft.VisualBasic;
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
    /// Interaction logic for BloodDonationRecordView.xaml
    /// </summary>
    public partial class BloodDonationRecordView : UserControl
    {
        DonationRecordController donationcontroller = new DonationRecordController();
        List<BloodType> bloodtypelist = new List<BloodType>();
        List<Gender> genderlist = new List<Gender>();
        private BloodDonor selected_blooddonor = new BloodDonor();
        List<BloodDonor> donorlist = new List<BloodDonor>();
        private bool IsNew = true;
        private string p_did = "%";
        private string p_dname = "%";
        string error = "";

        public BloodDonationRecordView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearInputs();

        }
        #region "Edit Blood Donor"
        private void EditFunction(BloodDonor donor)
        {
            txt_id.Text = donor.d_id;
            txt_name.Text = donor.d_name; ;
            txt_id.IsEnabled = false;
            txt_age.Text = donor.age.ToString();
            txt_address.Text = donor.address;
            txt_phoneno.Text = donor.phoneno;
            txt_times.Text = donor.times.ToString();
            db_birth.SelectedDate = donor.birthdate;
            db_startdate.SelectedDate = donor.startdate;
            db_enddate.SelectedDate = donor.lastdate;

            //getting blood type and gender
            bloodtypelist = donationcontroller.getBloodTypeList("%", out error);
            int index = bloodtypelist.FindIndex(x => x.bloodtype == donor.bloodtype);
            cb_bloodtype.ItemsSource = bloodtypelist;
            cb_bloodtype.SelectedValuePath = "bloodtype";
            cb_bloodtype.DisplayMemberPath = "bloodtype";
            cb_bloodtype.SelectedIndex = index;

            genderlist = donationcontroller.getGenderList("%", out error);
            index = genderlist.FindIndex(x => x.gender == donor.gender);
            cb_gender.ItemsSource = genderlist;
            cb_gender.DisplayMemberPath = "gender";
            cb_gender.SelectedValuePath = "gender";
            cb_gender.SelectedIndex = index;
        }
        #endregion
        #region "Clear the input forms"
        public void ClearInputs()
        {
            //getting donors
            grddonorlist.ItemsSource = null;            
            donorlist = donationcontroller.getBloodDonorList(p_did, p_dname, out error);
            grddonorlist.ItemsSource = donorlist;
            //getting blood type and gender
            bloodtypelist = donationcontroller.getBloodTypeList("%", out error);
            cb_bloodtype.ItemsSource = bloodtypelist;
            cb_bloodtype.SelectedValuePath = "bloodtype";
            cb_bloodtype.DisplayMemberPath = "bloodtype";
            cb_bloodtype.SelectedIndex = 0;

            genderlist = donationcontroller.getGenderList("%", out error);
            cb_gender.ItemsSource = genderlist;
            cb_gender.DisplayMemberPath = "gender";
            cb_gender.SelectedValuePath = "gender";
            cb_gender.SelectedIndex = 0;

            db_birth.SelectedDate = DateTime.Now;
            db_startdate.SelectedDate = DateTime.Now;
            db_enddate.SelectedDate = DateTime.Now;
            txt_age.Text = "0";
            txt_times.Text = "0";
            txt_id.Text = "";
            txt_id.IsEnabled = false;
            txt_name.Text = "";
            txt_address.Text = "";
            txt_phoneno.Text = "";

            //Creating Selected Blood Donor
            selected_blooddonor = new BloodDonor();
            IsNew = true;
        }
        #endregion

        private void grddonorlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grddonorlist.SelectedItem != null)
            {
                BloodDonor donorperson = (BloodDonor)grddonorlist.SelectedItem;
                if (donorperson.d_id != null)
                {
                    IsNew = false;
                    EditFunction(donorperson);
                    selected_blooddonor = donorperson;
                }
            }
        }

        private void btn_add_donor_Click(object sender, RoutedEventArgs e)
        {
            #region "Before saving"
            if (IsNew)
            {
                if (txt_name.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Please Enter Donor Name");
                    txt_name.Focus();
                    return;
                }
                selected_blooddonor = new BloodDonor();
                string id = donationcontroller.getID(out error);
                if (id == "")
                {
                    MessageBox.Show("Donor ID is wrong, Please Try Again!");
                    return;
                }
                selected_blooddonor.d_id = id;                
            }
            if (txt_name.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Please Enter Donor Name");
                txt_name.Focus();
                return;
            }
            selected_blooddonor.d_name = txt_name.Text.ToString();
            selected_blooddonor.startdate = Convert.ToDateTime(db_startdate.SelectedDate);
            selected_blooddonor.lastdate = Convert.ToDateTime(db_enddate.SelectedDate);
            selected_blooddonor.address = txt_address.Text.ToString();
            selected_blooddonor.phoneno = txt_phoneno.Text.ToString();
            selected_blooddonor.email = "";
            int i;
            if (int.TryParse(txt_times.Text.ToString(), out i))
            {
                selected_blooddonor.times = Convert.ToInt32(txt_times.Text);
            }
            else
            {
                MessageBox.Show("Please input true value of times");
                txt_times.Focus();
                return;
            }

            selected_blooddonor.bloodtype = cb_bloodtype.SelectedValue.ToString();
            selected_blooddonor.gender = cb_gender.SelectedValue.ToString();
            selected_blooddonor.isactive = true;
            if (int.TryParse(txt_age.Text.ToString(), out i))
            {
                selected_blooddonor.age = Convert.ToInt32(txt_age.Text.ToString());
            }
            else
            {
                MessageBox.Show("Please input true value of age.");
                txt_age.Focus();
                return;
            }

            selected_blooddonor.birthdate = Convert.ToDateTime(db_birth.SelectedDate);
            #endregion
            if (selected_blooddonor.d_id != null)
            {
                bool ret = donationcontroller.saveBloodDonor(selected_blooddonor, out error);
                if(ret)
                {
                    MessageBox.Show("Successfully Save Donor Information", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Failed to save data!", "Try Again!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btn_new_Click(object sender, RoutedEventArgs e)
        {
            IsNew = true;
            ClearInputs();
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (selected_blooddonor.d_id != null)
            {
                string d_name = txt_name.Text.ToString().Trim();
                string d_id = txt_id.Text.ToString();
                BloodDonor bd = donorlist.Find(x => x.d_id == d_id && x.d_name == d_name);
                if (bd != null)
                {
                    bool red = donationcontroller.deleteDonor(d_id, out error);
                    if (red)
                    {
                        MessageBox.Show("Successfully Delete", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Try again", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }                        
                }
                else
                {
                    MessageBox.Show("Please select Donor Information to delete", "Select Donor", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            string did = "";
            string dname = "";
            did = (txt_search_id.Text.Trim() == "") ? "%" : txt_search_id.Text.ToString();
            dname = (txt_name.Text.Trim() == "") ? "%" : txt_search_name.Text.ToString();
            grddonorlist.ItemsSource = null;
            donorlist = donationcontroller.getBloodDonorList(did, dname, out error);
            grddonorlist.ItemsSource = donorlist;
        }

        private void txt_search_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txt_search_id.Text.Trim() != "")
                {
                    txt_search_id.Text = String.Concat("D", (txt_search_id.Text.ToString()).PadLeft(10,'0'));
                }
                else
                {
                    txt_search_id.Text = "";
                }
            }
        }

        private void grddonorlist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grddonorlist.SelectedItem != null)
            {
                BloodDonor donorperson = (BloodDonor)grddonorlist.SelectedItem;
                if (donorperson.d_id != null)
                {
                    DateTime curDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    DateTime lastDate = Convert.ToDateTime(donorperson.lastdate.ToShortDateString());
                    DateTime afterFour = Convert.ToDateTime(donorperson.lastdate.AddMonths(4));
                    if((curDate-lastDate).Days < (afterFour-lastDate).Days)
                    {
                        MessageBox.Show("သွေးထပ်မံလှူဒါန်း လို့ မရသေးပါ\nနောက်ဆုံး လှူဒါန်းထားသော ရက်မှာ - "+donorperson.lastdate.ToShortDateString()+"\nလေးလ တစ်ကြိမ်သာ လှူဒါန်းနိုင်ပါသည်\nကျေးဇူးတင်ပါသည်",
                            "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    CommonFactory.selDonor = donorperson;
                    AddDonationInformation addremarks = new AddDonationInformation();
                    addremarks.ShowDialog();
                    if(AddDonationInformation.state == true)
                    {
                        grddonorlist.ItemsSource = null;
                        donorlist = donationcontroller.getBloodDonorList(p_did, p_dname, out error);
                        grddonorlist.ItemsSource = donorlist;
                        CommonFactory.selDonor = donorlist.Find(x => x.d_id == CommonFactory.selDonor.d_id);
                        EditFunction(CommonFactory.selDonor);
                    }
                }
            }
        }
    }
}
