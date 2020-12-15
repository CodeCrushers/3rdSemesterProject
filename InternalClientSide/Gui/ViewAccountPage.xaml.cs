using InternalClientSide.Controllers;
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

        private AccountController accountController;
        private BookingController bookingController;
        public ViewAccountPage() {
            InitializeComponent();
            AccountBookings.DataContext = null;
            accountController = new AccountController();
            bookingController = new BookingController();
        }

        private void ChangeAccount(object sender, RoutedEventArgs e) {
            Account account = accountController.Account;
            if (account != null) {
                accountController.Account.Name = GetText(CurrentNameInput.Document);
                accountController.Account.Phone = GetText(CurrentPhoneInput.Document);
                accountController.Account.Email  = GetText(CurrentEmailInput.Document);
                accountController.ChangeAccount(accountController.Account);
            }
        }

        private void ChangeBooking(object sender, RoutedEventArgs e) {
            Booking booking = bookingController.Booking;
            if (booking != null) {
                bookingController.Booking.Name = GetText(CurrentCarRegistrationInput.Document);
                bookingController.Booking.Phone = GetText(CurrentDateInput.Document);
                bookingController.Booking.Email = GetText(CurrentStartInput.Document);
                bookingController.Booking.Email = GetText(CurrentEndInput.Document);
                bookingController.Booking.Email = GetText(CurrentPaymentInput.Document);
                bookingController.Booking.Email = GetText(CurrentPayedInput.Document);
                bookingController.ChangeBooking(bookingController.Booking);
            }
        }

        private string GetText(FlowDocument flowDocument) {
            var pointerStart = flowDocument.ContentStart;
            var pointerEnd = flowDocument.ContentEnd;
            string text = new TextRange(pointerStart, pointerEnd).Text;

            if (text.EndsWith("\r\n")) {
                text = new TextRange(pointerStart, pointerEnd.GetPositionAtOffset(-2)).Text;
            }
            return text;
        }

        private void GetAccount(object sender, RoutedEventArgs e) {
            string email = GetText(EmailInput.Document);
            Account account = accountController.GetAccount(email);
            if (account != null) {
                CurrentNameInput.AppendText(account.Name);
                CurrentEmailInput.AppendText(account.Email);
                CurrentPhoneInput.AppendText(account.Phone);
            }
            List<Booking> bookings = GetBookings(account.Id);
            AccountBookings.DataContext = bookings;
            

            //Console.WriteLine(response.Result.Content.ReadAsAsync<IEnumrable<Account>>);
        }

        private List<Booking> GetBookings(string id) {
            return bookingController.GetBookings(id);
        }

    }
}
