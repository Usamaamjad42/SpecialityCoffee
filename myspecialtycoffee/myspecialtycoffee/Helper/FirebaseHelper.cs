using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using myspecialtycoffee.Model;
using System.Collections.Generic;

namespace myspecialtycoffee.Helper
{
    class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://myspecialtycoffee-777.firebaseio.com/");



        /// <summary>
        /// SuperUser data
        /// </summary>
        /// <returns></returns>

        public async Task<List<LoginModel>> GetAllOwners()
        {

            return (await firebase
              .Child("SuperUser")
              .OnceAsync<LoginModel>()).Select(item => new LoginModel
              {
                  Username = item.Object.Username,
                  Password = item.Object.Password
              }).ToList();
        }

        public async Task<LoginModel> GetAuthentication(string Username, string Password)
        {
            var allPersons = await GetAllOwners();
            await firebase
              .Child("SuperUser")
              .OnceAsync<LoginModel>();
            return allPersons.Where(a => a.Username == Username && a.Password == Password).FirstOrDefault();
        }


        public async Task UpdatePassword(string Username, string Password, string newPassword)
        {
            var toUpdatePass = (await firebase
              .Child("SuperUser")
              .OnceAsync<LoginModel>()).Where(a => a.Object.Username == Username && a.Object.Password == Password).FirstOrDefault();

            await firebase
              .Child("SuperUser")
              .Child(toUpdatePass.Key)
              .PutAsync(new LoginModel() { Username = Username, Password = newPassword });
        }

        /// <summary>
        /// Users data
        /// </summary>
        /// <returns></returns>

        public async Task<List<LoginModel>> GetAllUsers()
        {


            return (await firebase
              .Child("Users")
              .OnceAsync<LoginModel>()).Select(item => new LoginModel
              {
                  Username = item.Object.Username,
                  Password = item.Object.Password
              }).ToList();
        }

        public async Task<LoginModel> GetUsersAuthentication(string Username, string Password)
        {
            var allPersons = await GetAllUsers();
            await firebase
              .Child("Users")
              .OnceAsync<LoginModel>();
            return allPersons.Where(a => a.Username == Username && a.Password == Password).FirstOrDefault();
        }

        public async Task AddUser(string Username, string Password)
        {

            await firebase
              .Child("Users")
              .PostAsync(new LoginModel() { Username = Username, Password = Password });
        }

        /// <summary>
        /// Sales data
        /// </summary>
        /// <returns></returns>

        public async Task<List<InvoiceModel>> GetAllSales()
        {

            return (await firebase
              .Child("Sales")
              //.OrderBy("DateCLD")
              //.StartAt("1/27/20")
              //.EndAt("1/31/20")
              .OnceAsync<InvoiceModel>()).Select(item => new InvoiceModel
              {
                  invnoCLD = item.Object.invnoCLD,
                  salesManCLD = item.Object.salesManCLD,
                  totalAmountCLD = item.Object.totalAmountCLD,
                  totalWithVatCLD = item.Object.totalWithVatCLD,
                  dateCLD = item.Object.dateCLD,
                  vatCLD = item.Object.vatCLD,
                  paymentTypeCLD = item.Object.paymentTypeCLD,
                  orderTypeCLD = item.Object.orderTypeCLD,
                  custIDCLD = item.Object.custIDCLD

              }).ToList();
        }

        public async Task<List<InvoiceModel>> GetSalesDateWise(string StartingDate, string EndingDate)
        {

            return (await firebase
              .Child("Sales")
              .OrderBy("DateCLD")
              .StartAt(StartingDate)
              .EndAt(EndingDate)
              .OnceAsync<InvoiceModel>()).Select(item => new InvoiceModel
              {
                  invnoCLD = item.Object.invnoCLD,
                  salesManCLD = item.Object.salesManCLD,
                  totalAmountCLD = item.Object.totalAmountCLD,
                  totalWithVatCLD = item.Object.totalWithVatCLD,
                  dateCLD = item.Object.dateCLD,
                  vatCLD = item.Object.vatCLD,
                  paymentTypeCLD = item.Object.paymentTypeCLD,
                  orderTypeCLD = item.Object.orderTypeCLD,
                  custIDCLD = item.Object.custIDCLD

              }).ToList();
        }

    }
}
