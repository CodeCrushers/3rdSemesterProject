using InternalClientSide.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace InternalClientSide.Controllers {
    class AccountController {

        public Account Account { get; set; }
        private HttpClient HttpClient = new HttpClient();
        private string baseurl = "https://localhost:44346/api/";


        public void CreateAccount(Account account) {
            var json = new JavaScriptSerializer().Serialize(account);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = HttpClient.PostAsync("https://localhost:44346/api/account", stringContent);
        }

        public Account GetAccount(string email) {
            string fullUrl = baseurl + "account/" + email + "/";
            try {
            var response = HttpClient.GetAsync(fullUrl).Result;
            response.EnsureSuccessStatusCode();
                Account = response.Content.ReadAsAsync<Account>().Result;
                
            }
            catch (Exception e) {


            }

            return Account;
            
        }

        public void ChangeAccount(Account account) {
            string fullUrl = baseurl + "account";
            var json = new JavaScriptSerializer().Serialize(account);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = HttpClient.PutAsync(fullUrl, stringContent );


        }
    }
}
