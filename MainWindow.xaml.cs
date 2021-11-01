using MdyBloolDonation.Views;
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

namespace MdyBloolDonation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();
            UserLoginView userlogin = new UserLoginView();
            userlogin.ShowDialog();
            if (userlogin.status)
            {
                MainPageView mainPageView = new MainPageView();
                gridcontent.Content = mainPageView;
                this.Show();
            }
                
        }

        //private void gridHeader_MouseDown(object sender, MouseButtonEventArgs e)
        //{

        //}

        //private void btnPower_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
