using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
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
    /// Логика взаимодействия для LocalWebBrowser.xaml
    /// </summary>
    public partial class LocalWebBrowser : Window
    {
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool InternetSetCookie(string lpszUrlName, string lpszCookieName, string lpszCookieData);

        public LocalWebBrowser(string uri)
        {
            InitializeComponent();
            NavigateTo(uri);
        }

        public void NavigateTo(string uri)
        {
            if (Connection.cookiesCollection == null)
            {
                MessageBox.Show("Cookies пустые. Пожалуйста, проведите аутентификацыю!");
                return;
            }
            foreach (Cookie cookie in Connection.cookiesCollection)
                InternetSetCookie("https://booking.uz.gov.ua/ru/", null, cookie.ToString());
            webBrowserLocal.Navigate(uri);
        }
    }
}
