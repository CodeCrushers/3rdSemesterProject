﻿using System;
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
using System.Net.Http;
using InternalClientSide.Model;
using System.Web.Script.Serialization;
using InternalClientSide.Controllers;

namespace InternalClientSide.Gui {
    /// <summary>
    /// Interaction logic for CreateAccountPage.xaml
    /// </summary>
    public partial class CreateAccountPage : Page {
        private StringBuilder stringBuilder;
        private AccountController accountController;

        public CreateAccountPage() {
            InitializeComponent();
            stringBuilder = new StringBuilder();
            accountController = new AccountController();
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

        private void CreateUserButton(object sender, RoutedEventArgs e) {
            string name = GetText(UsernameInput.Document);
            string password = PasswordInputPass.Password;
            string phoneNumber = GetText(PhoneInput.Document);
            string email = GetText(EmailInput.Document);
            Account account = new Account {
                Name = name,
                Password = password,
                Phone = phoneNumber,
                Email = email
            };
            accountController.CreateAccount(account);
            
        }


    }
}
