using myspecialtycoffee.Animations;
using myspecialtycoffee.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace myspecialtycoffee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateNewUsers : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public CreateNewUsers()
        {
            InitializeComponent();

            txtUsername.Completed += (object sender, EventArgs e) =>
            {
                txtPassword.Focus();
            };

        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();

            /////////////
            await Task.Run(async () =>
             {
                 await ViewAnimations.FadeAnimY(Logo);
                 await ViewAnimations.FadeAnimY(MainStack);

             });
            /////////////////

            var allPersons = await firebaseHelper.GetAllUsers();
            lstPersons.ItemsSource = allPersons;
        }

        private async void BtnCreateUser_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("No Internet", "", "OK");
                return;
            }

            try
            {
                var isValid = true;

                if (string.IsNullOrEmpty(txtUsername.Text))
                {
                    isValid = false;
                }
                else if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    isValid = false;
                }
                
                if (isValid)
                {
                    
                    await firebaseHelper.AddUser(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                    txtUsername.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    await DisplayAlert("Success", "Users Added Successfully", "OK");

                    var allPersons = await firebaseHelper.GetAllUsers();
                    lstPersons.ItemsSource = allPersons;

                }
                else
                {
                    await DisplayAlert("Error!", "Fields can't be empty ", "OK");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error: ", ex.ToString(), "OK");
            }
        }
    }
}