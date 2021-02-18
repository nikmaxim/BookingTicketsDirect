using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingTicketsDirect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Person> Persons = new ObservableCollection<Person>();

        public MainWindow()
        {
            InitializeComponent();
            Persons.Add(new Person()
            {
                Id = Persons.Count + 1,
                CarriageStr = "1",
                PlaceStr = "008А",
                LastName = "Иванов",
                FirstName = "Иван",
                DocumentTypes = new List<string> { "Полный", "Детский" /*"Студенческий", "Льготный" */},
                DocumentTypesStr = "Полный",
                ServiceBeddingStr = 0,
                ServiceWaterStr = 0
            });
            tabControl.ItemsSource = Persons;
        }

        private void ShowPassengersTab(object sender, RoutedEventArgs e)
        {
            while (this.Width <= 1040)
                this.Width += 10;
            tabControl.Visibility = Visibility.Visible;
        } //Correct
        private void HidePassengersTab(object sender, RoutedEventArgs e)
        {
            tabControl.Visibility = Visibility.Collapsed;
            while (this.Width >= 460)
                this.Width -= 10;
        } //Correct

        private void SendAuthDataFunc(object sender, RoutedEventArgs e)
        {
            UserAuthWindow userAuthWindow = new UserAuthWindow();
            userAuthWindow.Show();
        }

        private void AuthenticationFunc(object sender, RoutedEventArgs e)
        {
            Connection.Authentication();
        } //Correct
        private void DeauthenticationFunc(object sender, RoutedEventArgs e)
        {
            Connection.Deauthentication();
        } //Correct
        private void OpenBrowserFunc(object sender, RoutedEventArgs e)
        {
            LocalWebBrowser localWeb = new LocalWebBrowser("https://booking.uz.gov.ua/ru/?from=2200001&to=2208001&date=2020-08-22&time=00%3A00&url=train-list");
            localWeb.Show();
        } //Correct
        private void OpenCabinetFunc(object sender, RoutedEventArgs e)
        {
            LocalWebBrowser localWeb = new LocalWebBrowser("https://booking.uz.gov.ua/ru/cabinet/actual/");
            localWeb.Show();
        } //Correct
        private void OpenCartFunc(object sender, RoutedEventArgs e)
        {
            LocalWebBrowser localWeb = new LocalWebBrowser("https://booking.uz.gov.ua/ru/cart/");
            localWeb.Show();
        } //Correct
        private void ExitFunc(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        } //Correct

        //Second header
        private void OpenCookiesFormFunc(object sender, RoutedEventArgs e)
        {
            CookiesForm cookiesForm = new CookiesForm();
            cookiesForm.Show();
        } //Correct
        private void AddTabItemFunc(object sender, RoutedEventArgs e)
        {
            Persons.Add(new Person()
            {
                Id = Persons.Count + 1,
                LastName = "Фамилия",
                FirstName = "Имя",
                DocumentTypes = new List<string> { "Полный", "Детский" /*"Студенческий", "Льготный" */},
                DocumentTypesStr = "Полный",
                ServiceBeddingStr = 0,
                ServiceWaterStr = 0
            });
        } //Correct
        private void RemoveTabItemFunc(object sender, RoutedEventArgs e)
        {
            if (Persons.Count <= 1)
                return;
            Persons.Remove(Persons.Last());
        } //Correct
        private void UpdateDataBinding(object sender, RoutedEventArgs e)
        {
            tabControl.ItemsSource = null;
            tabControl.ItemsSource = Persons;

            MessageBox.Show("Привязка успешно обновлена!");
        }

        private void AddCart(object sender, RoutedEventArgs e)
        {
            string basicData = "";
            char tempClass = ' ';
            foreach (var person in Persons)
            {
                if (availableTrainPlaceResult.Text.First() == 'С')
                    tempClass = availableTrainPlaceResult.Text.Last();
                else if (availableTrainPlaceResult.Text.First() == 'К' || availableTrainPlaceResult.Text.First() == 'П')
                    tempClass = 'Б';
                else if (availableTrainPlaceResult.Text.First() == 'Л')
                    tempClass = 'В';

                int c = person.Id - 1;
                if (c > 0)
                    basicData += "&";
                basicData += $"places[{c}][ord]={c}" +                                                                       //1
                    $"&places[{c}][from]={Connection.idTrains.FirstOrDefault(x => x.Value == departurePlaceTb.Text).Key}" +  //2
                    $"&places[{c}][to]={Connection.idTrains.FirstOrDefault(x => x.Value == destinationPlaceTb.Text).Key}" +  //3
                    $"&places[{c}][train]={idTrainResult.Text}" +                                                    //4
                    $"&places[{c}][date]={departureDatePlaceTb.Text}" +                                                      //5
                    $"&places[{c}][wagon_num]={person.CarriageStr}" +                                                        //6
                    $"&places[{c}][wagon_class]={tempClass}" +                                                               //7
                    $"&places[{c}][wagon_type]={availableTrainPlaceResult.Text.First()}" +                                   //8                                                                                    
                    $"&places[{c}][wagon_railway]={wagonRailwayResult.Text}" +                                               //9 Данные неизвестны. Неавтоматизировано.                                                                                                   
                    $"&places[{c}][charline]={person.PlaceStr.Last()}" +                                                     //10
                    $"&places[{c}][firstname]={person.FirstName}" +                                                          //11
                    $"&places[{c}][lastname]={person.LastName}";                                                             //12

                if (availableTrainPlaceResult.Text.First() == 'Л')
                    basicData += $"&places[{c}][bedding]=1";
                else if (availableTrainPlaceResult.Text.First() != 'С')
                    basicData += $"&places[{c}][bedding]={person.ServiceBeddingStr}";                                        //13 Спальное белье. Для Л, П и К. 0 - нету, 1 -  есть.     
                else basicData += $"&places[{c}][bedding]=0";

                if (availableTrainPlaceResult.Text.First() == 'С' && person.ServiceWaterStr == 1)
                    basicData += $"&places[{c}][services][]=В";                                                              //14 Вода включена. Для C1

                if (person.DocumentTypesStr == "Детский")
                    basicData += $"&places[{c}][child]={person.DocumentInfo}";                                               //15
                else basicData += $"&places[{c}][child]=";

                basicData += $"&places[{c}][student]=" +                                                                     //16 Сейчас проезд по студаку невозможен
                $"&places[{c}][reserve]=0" +                                                                                 //17 Данные статичные, не меняются. 
                $"&places[{c}][place_num]={person.PlaceStr[0]}{person.PlaceStr[1]}{person.PlaceStr[2]}" +                    //18
                $"&places[{c}][log_bonus]=5" +                                                                               //19 Данные статичные, не меняются.
                $"&user=nivmaxi@gmail.com";                                                                                  //20
            }
            Connection.AddCart(basicData);

            MessageBox.Show("Успешно!");
        }
    }
}
