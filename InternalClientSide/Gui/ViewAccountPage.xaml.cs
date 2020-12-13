using InternalClientSide.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace InternalClientSide.Gui {
    /// <summary>
    /// Interaction logic for ViewAccountPage.xaml
    /// </summary>
    public partial class ViewAccountPage : Page {

        private HttpClient HttpClient;
        private string baseurl = "https://localhost:44346/api/";
        public ViewAccountPage() {
            InitializeComponent();
            AccountBookings.DataContext = null;
            HttpClient = new HttpClient();
        }

        private void ChangeAccountButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            string name = GetText(CurrentNameInput.Document);
            string phoneNumber = GetText(CurrentPhoneInput.Document);
            string email = GetText(CurrentEmailInput.Document);

        }

        private string GetText(FlowDocument flowDocument) {
            var pointerStart = flowDocument.ContentStart;
            var pointerEnd = flowDocument.ContentEnd;
            return new TextRange(pointerStart, pointerEnd).Text;
        }

        private void GetAccount(object sender, RoutedEventArgs e) {
            string email = GetText(EmailInput.Document);
            string fullUrl = baseurl + "account?email=" + email;
            var response = HttpClient.GetAsync(fullUrl).Result;
            response.EnsureSuccessStatusCode();
            Account account = response.Content.ReadAsAsync<Account>().Result;

            Console.WriteLine(account.Email);
            Console.WriteLine(account.Name);
            Console.WriteLine(account.Phone);
            //Console.WriteLine(response.Result.Content.ReadAsAsync<IEnumrable<Account>>);
        }
    }
}
