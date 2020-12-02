using InternalClientSide.Model;
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

namespace InternalClientSide {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private List<Page> pages;

        public MainWindow() {
            InitializeComponent();
            pages = new List<Page>();
            pages.Add(new MainPage());
            pages.Add(new CarPage());
            pages.Add(new AccountPage());
            ContentFrame.DataContext = new MainPage();
        }

        private void OpenMainPage(object sender, MouseButtonEventArgs e) {
            ContentFrame.DataContext = pages[0];
        }

        private void OpenCarPage(object sender, MouseButtonEventArgs e) {
            ContentFrame.DataContext = pages[1];
            
        }

        private void OpenAccountPage(object sender, MouseButtonEventArgs e) {
            ContentFrame.DataContext = pages[2];
        }

    }
}
