using RESTServices.Database;
using RESTServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTServices.LogicLayer {
    public class CarLogic {

        private readonly CarDB _carDB;

        public CarLogic() {
            this._carDB = new CarDB();
        }

        public bool CreateCar(Car entity) {
            bool result = false;
            object o = this._carDB.Create(entity);
            if (o is string) {
                result = true;
            } else if (o is bool) {
                if ((bool)o == false) {
                    result = false;
                }
            }
            return result;
        }

        public Car GetCar(string reg) {
            Car car = null;
            car = this._carDB.Get(reg);
            return car;
        }

        public IEnumerable<Car> GetAllCars() {
            IEnumerable<Car> list = null;
            list = this._carDB.GetAll();
            return list;
        }

        public bool EditCar(Car entity) {
            bool result = false;
            result = this._carDB.Update(entity);
            return result;
        }

        public bool DeleteCar(string reg) {
            bool result = false;
            object o = this._carDB.Delete(reg);
            if (o is string) {
                result = true;
            } else if (o is bool) {
                if ((bool)o == false) {
                    result = false;
                }
            }
            return result;
        }
    }
}