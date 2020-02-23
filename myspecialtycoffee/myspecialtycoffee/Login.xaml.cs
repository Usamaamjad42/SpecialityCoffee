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
    public partial class Login : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public Login()
        {
            InitializeComponent();

            

            txtUsername.Completed += (object sender, EventArgs e) =>
            {
                txtPassword.Focus();
            };

            try
            {
                this.BindingContext = this;
                this.IsBusy = false;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error: ", ex.ToString(), "OK");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () =>
            {
                await ViewAnimations.FadeAnimY(Logo);
                await ViewAnimations.FadeAnimY(MainStack);

            });
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            btnLogin.IsEnabled = false;
            txtUsername.IsEnabled = false;
            txtPassword.IsEnabled = false;
            this.IsBusy = true;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                EnOrDis();
                await DisplayAlert("No Internet", "", "OK");
                return;
            }

            try
            {
                var isValid = true;
                if (string.IsNullOrEmpty(txtUsername.Text))
                {
                    EnOrDis();
                    await DisplayAlert("Error!", "username or password can't be empty ", "OK");
                    isValid = false;
                }
                else if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    EnOrDis();
                    await DisplayAlert("Error!", "username or password can't be empty ", "OK");
                    isValid = false;
                }

                if (isValid)
                {
                    var loginModel = await firebaseHelper.GetAuthentication(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                    if (loginModel != null)
                    {
                        //txtId.Text = person.PersonId.ToString();
                        //txtName.Text = person.Name;
                        //await DisplayAlert("Success", "Login Successfull", "OK");
                        EnOrDis();
                        txtUsername.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                        await Navigation.PushAsync(new Dashboard());

                    }
                    else
                    {
                        loginModel = await firebaseHelper.GetUsersAuthentication(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                        if (loginModel != null)
                        {
                            EnOrDis();
                            txtUsername.Text = string.Empty;
                            txtPassword.Text = string.Empty;
                            await Navigation.PushAsync(new UserDashboard());
                        }
                        else
                        {
                            EnOrDis();
                            await DisplayAlert("Failed", "Invalid username or password", "OK");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                EnOrDis();
                await DisplayAlert("Error: ", ex.ToString(), "OK");
            }


        }

        public void EnOrDis()
        {
            this.IsBusy = false;
            txtUsername.IsEnabled = true;
            txtPassword.IsEnabled = true;
            btnLogin.IsEnabled = true;
        }

        protected override bool OnBackButtonPressed()
        {
             Navigation.PopToRootAsync();
            return base.OnBackButtonPressed();
        }

    }
}