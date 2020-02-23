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
    public partial class ChangePassword : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public ChangePassword()
        {
            InitializeComponent();

            txtUsername.Completed += (object sender, EventArgs e) =>
            {
                txtOldPassword.Focus();
            };

            txtOldPassword.Completed += (object sender, EventArgs e) =>
            {
                txtNewPassword.Focus();
            };

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

        private async void BtnChangePassword_Clicked(object sender, EventArgs e)
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
                else if (string.IsNullOrEmpty(txtOldPassword.Text))
                {                
                    isValid = false;
                }
                else if(string.IsNullOrEmpty(txtNewPassword.Text))
                {                  
                               
                    isValid = false;
                }

                if (isValid)
                {
                    var loginModel = await firebaseHelper.GetAuthentication(txtUsername.Text.Trim(), txtOldPassword.Text.Trim());
                    if (loginModel != null)
                    {
                        await firebaseHelper.UpdatePassword(txtUsername.Text.Trim(), txtOldPassword.Text.Trim(), txtNewPassword.Text.Trim());
                        txtUsername.Text = string.Empty;
                        txtOldPassword.Text = string.Empty;
                        txtNewPassword.Text = string.Empty;
                        await DisplayAlert("Success", "Password Updated Successfully", "OK");

                    }
                    else
                    {
                        await DisplayAlert("Failed", "Invalid username or password", "OK");
                    }
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
