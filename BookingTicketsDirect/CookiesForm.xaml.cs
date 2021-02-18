using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
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
    /// Логика взаимодействия для CookiesForm.xaml
    /// </summary>
    public partial class CookiesForm : Window
    {
        public CookiesForm()
        {
            InitializeComponent();
        }
        private void CloseWin(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ImportCookies(object sender, RoutedEventArgs e)
        {
            cookiesTextBox.Text = "";
            if (Connection.cookiesCollection == null)
            {
                cookiesTextBox.Text = "Невозможно импортировать куки!";
            }
            else foreach (Cookie c in Connection.cookiesCollection)
                    cookiesTextBox.Text += c.Name + "=" + c.Value + ";\n";
        }
    }
}
