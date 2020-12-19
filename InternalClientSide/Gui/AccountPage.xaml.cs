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

namespace InternalClientSide.Gui {
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page {
        private List<Page> pages;
        public AccountPage() {
            InitializeComponent();
            pages = new List<Page>();
            pages.Add(new ViewAccountPage());
            AccountContent.DataContext = pages[0];
        }

        //public void createuserpage(object sender, mousebuttoneventargs e) {
        //    accountcontent.datacontext = pages[0];

        //}

        public void ViewUserPage(object sender, MouseButtonEventArgs e) {
            AccountContent.DataContext = pages[0];
        }
    }
}
