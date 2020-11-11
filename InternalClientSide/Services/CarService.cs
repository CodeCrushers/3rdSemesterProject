using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InternalClientSide.Services {
    public class CarService {

        static readonly string restUrl = "https://localhost:44346/api/Car";

        static HttpClient _client;

        public CarService() {
            _client = new HttpClient();
        }

        public async Task<string> GetCar() {
            string strServiceText;
            string useRestUrl = restUrl;
            return null;
        }

    }
}
