﻿using InternalClientSide.Model;
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
    /// Interaction logic for CarPage.xaml
    /// </summary>
    public partial class CarPage : Page {
        public CarPage() {
            InitializeComponent();
            //carsGrid.DataContext = Car.GetCars();
        }
    }
}
