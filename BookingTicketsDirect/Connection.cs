using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows;

namespace BookingTicketsDirect
{
    class Connection
    {
        internal static CookieContainer cc = new CookieContainer();
        internal static CookieCollection cookiesCollection;
        internal static List<int> totalCountWagons = new List<int>();
        internal static (string, string) userAuthData = ("", "");

        internal static Dictionary<int, string> idTrains = new Dictionary<int, string>
        {
            {2200001, "Киев"},
            {2208001, "Одесса"},
            {2204001, "Харьков"},
            {2218000, "Львов" },
            {2210700, "Днепр-Главный"},
            {2208530, "Херсон"},
            {2210800, "Запорожье 1"},
            {2210730, "Запорожье 2"}
        };

        internal static void Authentication()
        {
            if(string.IsNullOrEmpty(userAuthData.Item1) || string.IsNullOrEmpty(userAuthData.Item2))
            {
                MessageBox.Show("Данные для аутентификации отсутствуют!");
                return;
            }
            byte[] requestData = Encoding.ASCII.GetBytes($"email={userAuthData.Item1}&pwd={userAuthData.Item2}");
            var req = (HttpWebRequest)WebRequest.Create("https://booking.uz.gov.ua/ru/authorization/login/");
            req.Proxy = null;
            req.CookieContainer = cc;
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            req.ContentLength = requestData.Length;
            using (Stream s = req.GetRequestStream())
                s.Write(requestData, 0, requestData.Length);
            using (var response = (HttpWebResponse)req.GetResponse())
                cookiesCollection = response.Cookies;
            MessageBox.Show("Проведено!");
        }

        internal static void Deauthentication()
        {
            var req = (HttpWebRequest)WebRequest.Create("https://booking.uz.gov.ua/ru/authorization/logout/");
            req.Proxy = null;
            req.CookieContainer = cc;
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            using (Stream s = req.GetRequestStream())
            using (var response = (HttpWebResponse)req.GetResponse()) { }

            MessageBox.Show("Успешно!");
        }

        internal static void AddCart(string basicData)
        {
            string result = "";

            string postData = HttpUtility.UrlEncode(basicData).Replace("%3d", "=");
            postData = postData.Replace("%3d", "=");
            postData = postData.Replace("%26", "&");
            //postData = "places%5B0%5D%5Bord%5D=0&places%5B0%5D%5Bfrom%5D=2200001&places%5B0%5D%5Bto%5D=2208001&places%5B0%5D%5Btrain%5D=761%D0%A8&places%5B0%5D%5Bdate%5D=2020-08-22&places%5B0%5D%5Bwagon_num%5D=1&places%5B0%5D%5Bwagon_class%5D=1&places%5B0%5D%5Bwagon_type%5D=%D0%A1&places%5B0%5D%5Bwagon_railway%5D=41&places%5B0%5D%5Bcharline%5D=%D0%90&places%5B0%5D%5Bfirstname%5D=%D0%98%D0%B2%D0%B0%D0%BD&places%5B0%5D%5Blastname%5D=%D0%94%D0%B5%D0%BC%D1%87%D0%B5%D0%BD%D0%BA%D0%BE&places%5B0%5D%5Bbedding%5D=0&places%5B0%5D%5Bchild%5D=&places%5B0%5D%5Bstudent%5D=&places%5B0%5D%5Breserve%5D=0&places%5B0%5D%5Bplace_num%5D=011&places%5B0%5D%5Blog_bonus%5D=5&user=nivmaxi%40gmail.com";
            byte[] requestData = Encoding.ASCII.GetBytes(postData);
            var req = (HttpWebRequest)WebRequest.Create("https://booking.uz.gov.ua/ru/cart/add/");
            req.Proxy = null;
            req.CookieContainer = cc;
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            req.AllowAutoRedirect = true;
            req.ContentLength = requestData.Length;
            using (Stream s = req.GetRequestStream())
                s.Write(requestData, 0, requestData.Length);
            using (var response = (HttpWebResponse)req.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
                result = reader.ReadToEnd();
        }
    }
}
