using myspecialtycoffee.Model;
using myspecialtycoffee.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace myspecialtycoffee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        InvoiceViewModel invoiceViewModel = new InvoiceViewModel();

        public Dashboard()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);

        }

        private async void BtnAddUser_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new CreateNewUsers());
        }

        private async void BtnChangePass_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePassword());
        }

        private async void BtnLogOut_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Exit", "Do you wan't to exit the App?", "Yes", "No");
            if (answer)
            {
                await Navigation.PushAsync(new Login());
            }
            else
            {

                // User choose No
            }

        }

        private async void onFrameSaleReporter_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Sales());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

       
    }
}