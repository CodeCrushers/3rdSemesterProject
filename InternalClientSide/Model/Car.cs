using InternalClientSide.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalClientSide.Model {
    public class Car : INotifyPropertyChanged {

        public string Name { get; set; }
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public static Car GetCar() {
            Car car = new Car() {
                Name = "Avensis",
                Id = 1,
                SerialNumber = "XD123XD",
                Manufacturer = Manufacturer.TOYOTA
            };
            return car;
        }

        public static List<Car> GetCars() {
            List<Car> cars = new List<Car>();
            cars.Add(new Car() {
                Name = "Avensis",
                Id = 1,
                SerialNumber = "XD123XD",
                Manufacturer = Manufacturer.TOYOTA
            });
            cars.Add(new Car() {
                Name = "Model S",
                Id = 2,
                SerialNumber = "XD456XD",
                Manufacturer = Manufacturer.TESLA
            });
            cars.Add(new Car() {
                Name = "Fiesta",
                Id = 3,
                SerialNumber = "XD789XD",
                Manufacturer = Manufacturer.FORD
            });
            return cars;
        }

    }

}
