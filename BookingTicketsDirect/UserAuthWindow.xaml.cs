using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingTicketsDirect
{
    /// <summary>
    /// Логика взаимодействия для UserAuthWindow.xaml
    /// </summary>
    public partial class UserAuthWindow : Window
    {
        public UserAuthWindow()
        {
            InitializeComponent();
        }

        private void SendAuthData(object sender, RoutedEventArgs e)
        {
            Connection.userAuthData.Item1 = HttpUtility.UrlEncode(emailTextBox.Text);
            Connection.userAuthData.Item2 = HttpUtility.UrlEncode(passwordTextBox.Password);
            MessageBox.Show("Сохранено!");
            this.Close();
        }
    }
}
