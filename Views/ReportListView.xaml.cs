using MdyBloolDonation.Controllers;
using MdyBloolDonation.Model;
using Microsoft.Reporting.WinForms;
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
    /// Interaction logic for ReportListView.xaml
    /// </summary>
    public partial class ReportListView : UserControl
    {
        DonationRecordController donController = new DonationRecordController();
        List<Gender> genderlist = new List<Gender>();
        List<BloodType> bloodtypelist = new List<BloodType>();
        string error = "";
        public ReportListView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            #region "Window loaded assign"
            genderlist = donController.getGenderList("%", out error);
            Gender g = new Gender
            {
                gender = "[ALL]"
            };
            genderlist.Insert(0, g);
            cb_gender.ItemsSource = genderlist;
            cb_gender.SelectedValuePath = "gender";
            cb_gender.DisplayMemberPath = "gender";
            cb_gender.SelectedIndex = 0;
            bloodtypelist = donController.getBloodTypeList("%", out error);
            BloodType b = new BloodType
            {
                bloodtype = "[ALL]"
            };
            bloodtypelist.Insert(0, b);
            cb_bloodtype.ItemsSource = bloodtypelist;
            cb_bloodtype.SelectedValuePath = "bloodtype";
            cb_bloodtype.DisplayMemberPath = "bloodtype";
            cb_bloodtype.SelectedIndex = 0;

            db_date.SelectedDate = DateTime.Now;
            txt_id.Text = "";
            txt_id.Focus();
            this.rpt_donorlist.Reset();
            this.rpt_donorlist.LocalReport.DataSources.Clear();
            #endregion
        }

        private void txt_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txt_id.Text.Trim() != "")
                {
                    txt_id.Text = String.Concat("D", (txt_id.Text.ToString()).PadLeft(10, '0'));
                }
                else
                {
                    txt_id.Text = "";
                }
            }
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            string gender = (cb_gender.SelectedValue.ToString() == "[ALL]") ? "%" : cb_gender.SelectedValue.ToString();
            string bloodtype = (cb_bloodtype.SelectedValue.ToString() == "[ALL]") ? "%" : cb_bloodtype.SelectedValue.ToString();
            string id = (txt_id.Text.Trim() == "") ? "%" : txt_id.Text.ToString();
            DateTime date = Convert.ToDateTime(db_date.SelectedDate);
            List<DonorReport1> replist = donController.getAvailableDonorList(id, date, gender, bloodtype, out error);
            ReportDataSource reportDataSource1 = new ReportDataSource();
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = replist;

            this.rpt_donorlist.Reset();
            this.rpt_donorlist.LocalReport.DataSources.Clear();
            this.rpt_donorlist.LocalReport.DataSources.Add(reportDataSource1);
            this.rpt_donorlist.LocalReport.ReportEmbeddedResource = "MdyBloolDonation.Reports.DonorListPreview.rdlc";
            this.rpt_donorlist.ZoomMode = ZoomMode.PageWidth;
            this.rpt_donorlist.RefreshReport();
        }
    }
}
