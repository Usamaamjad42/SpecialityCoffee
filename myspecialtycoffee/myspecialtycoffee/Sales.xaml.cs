using myspecialtycoffee.Helper;
using myspecialtycoffee.Model;
using myspecialtycoffee.ViewModel;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
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
    public partial class Sales : ContentPage
    {
        

        InvoiceViewModel invoiceViewModel = new InvoiceViewModel();

        string txtAmounts;

        public Sales()
        {
            try
            {
                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjAyNTcwQDMxMzcyZTM0MmUzMGhoYXhiUXdPU1hDQ2JzTWprUUYzMFZoNzM1eXd6MDRuam5CS0llbUFxTG89");
                InitializeComponent();



                //NavigationPage.SetHasBackButton(this, false);
            }
            catch (Exception ex)
            {
                DisplayAlert("Error!", ex.ToString(), "Ok");
            }
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
            txtAmounts = invoiceViewModel.getAmounts();
            string[] words = txtAmounts.Split(' ');

            txt_totalVat.Text = words[0] + " AED";
            txt_totalAmount.Text = words[1] + " AED";
            txt_totalWithVatAmount.Text = words[2] + " AED";

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

        private void SearchAny_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = SearchAny.Text.Trim();


            var filterInvoices = invoiceViewModel.searchData(keyword);
            if (filterInvoices != null)
            {
                

                BCinvoiceViewModel.InvoicesInfo.Clear();

                foreach (var invoice in filterInvoices)
                {
                    BCinvoiceViewModel.InvoicesInfo.Add(new Model.InvoiceInfo(invoice.invnoCLD, invoice.salesManCLD, invoice.totalAmountCLD, invoice.totalWithVatCLD, invoice.dateCLD, invoice.vatCLD, invoice.paymentTypeCLD, invoice.orderTypeCLD, invoice.custIDCLD));

                }

                txtAmounts = invoiceViewModel.getAmounts();
                string[] words = txtAmounts.Split(' ');

                txt_totalVat.Text = words[0] + " AED";
                txt_totalAmount.Text = words[1] + " AED";
                txt_totalWithVatAmount.Text = words[2] + " AED";
                

            }
        }

        private async void BtnSearchDateWise_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("No Internet", "", "OK");
                return;
            }

            var popupAlert = new PopupView();
            var result = await popupAlert.Show(); //wait till user taps/selects option 
            if (result)
            {
                MessagingCenter.Subscribe<DateDetailsModel>(this, "PopUpDateData", (value) =>
                {
                    var FromData = value.fdate;
                    var ToData = value.tdate;
                    GetDateRange(FromData, ToData);
                });
            }
            //PopupNavigation.Instance.PushAsync(new PopupView());
        }

        private void GetDateRange(DateTime StartingDate, DateTime EndingDate)
        {
            if (StartingDate > EndingDate)
            {
                Navigation.PushPopupAsync(new AlertPopup("E", "Error!, StartingDate can't be greater then EndingDate."));
                return;
            }

            EndingDate = EndingDate.AddDays(1);

            string str_StartingDate = StartingDate.ToString("M/d/yy");
            string str_EndingDate = EndingDate.ToString("M/d/yy");

            LoadDataDateWise(str_StartingDate, str_EndingDate);

        }

        public async void LoadDataDateWise(string StartingDate, string EndingDate)
        {
            
            var dateFilterInvoices = await invoiceViewModel.searchDateWise(StartingDate, EndingDate);

            if (dateFilterInvoices != null && dateFilterInvoices.Count() != 0)
            {
                BCinvoiceViewModel.InvoicesInfo.Clear();

                foreach (var invoice in dateFilterInvoices)
                {
                    BCinvoiceViewModel.InvoicesInfo.Add(new Model.InvoiceInfo(invoice.invnoCLD, invoice.salesManCLD, invoice.totalAmountCLD, invoice.totalWithVatCLD, invoice.dateCLD, invoice.vatCLD, invoice.paymentTypeCLD, invoice.orderTypeCLD, invoice.custIDCLD));

                }

                txtAmounts = invoiceViewModel.getAmounts();
                string[] words = txtAmounts.Split(' ');

                txt_totalVat.Text = words[0] + " AED";
                txt_totalAmount.Text = words[1] + " AED";
                txt_totalWithVatAmount.Text = words[2] + " AED";
            }
            else
            {
                await Navigation.PushPopupAsync(new AlertPopup("E", "Error!, Data Not Found."));
            }
        }

        private async void ShowAllSales_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Alert", "Are you sure, you want to load all the records?", "Yes", "No");
            if (answer)
            {
                var allInvoices = await invoiceViewModel.allSalesRecords();

                if (allInvoices != null && allInvoices.Count() != 0)
                {


                    BCinvoiceViewModel.InvoicesInfo.Clear();

                    foreach (var invoice in allInvoices)
                    {
                        BCinvoiceViewModel.InvoicesInfo.Add(new Model.InvoiceInfo(invoice.invnoCLD, invoice.salesManCLD, invoice.totalAmountCLD, invoice.totalWithVatCLD, invoice.dateCLD, invoice.vatCLD, invoice.paymentTypeCLD, invoice.orderTypeCLD, invoice.custIDCLD));

                    }

                    txtAmounts = invoiceViewModel.getAmounts();
                    string[] words = txtAmounts.Split(' ');

                    txt_totalVat.Text = words[0] + " AED";
                    txt_totalAmount.Text = words[1] + " AED";
                    txt_totalWithVatAmount.Text = words[2] + " AED";


                }
                else { await DisplayAlert("Alert", "There is no record", "Ok"); }
            }
            else
            {

                // User choose No
            }
        }
    }
}