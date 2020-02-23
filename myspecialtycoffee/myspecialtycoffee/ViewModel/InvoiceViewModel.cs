using myspecialtycoffee.Helper;
using myspecialtycoffee.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace myspecialtycoffee.ViewModel
{
    public class InvoiceViewModel : INotifyPropertyChanged
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        static double totalVat = 0.0;
        static double totalAmount = 0.0;
        static double totalWithAmount = 0.0;

        private static List<InvoiceModel> allInvoices;



        public ObservableCollection<InvoiceInfo> InvoicesInfo { get; set; }
        public ObservableCollection<InvoiceInfo> InvoiceInfoCollection
        {
            get { return InvoicesInfo; }
            set
            {
                this.InvoicesInfo = value;
                RaisedOnPropertyChanged("InvoiceInfoCollection");
            }
        }



        public InvoiceViewModel()
        {
            InvoicesInfo = new ObservableCollection<InvoiceInfo>();
            this.GenerateInvoices();
        }

        public async void GenerateInvoices()
        {

            allInvoices = await firebaseHelper.GetAllSales();

            if (allInvoices != null)
            {
                totalVat = 0.0;
                totalAmount = 0.0;
                totalWithAmount = 0.0;

                foreach (var invoice in allInvoices)
                {
                    InvoicesInfo.Add(new InvoiceInfo(invoice.invnoCLD, invoice.salesManCLD, invoice.totalAmountCLD, invoice.totalWithVatCLD, invoice.dateCLD, invoice.vatCLD, invoice.paymentTypeCLD, invoice.orderTypeCLD, invoice.custIDCLD));

                    totalVat += invoice.vatCLD;
                    totalAmount += invoice.totalAmountCLD;
                    totalWithAmount += invoice.totalWithVatCLD;
                }
            }

        }

        /// <summary>
        /// Search data using Contains() Method
        /// </summary>
        public List<InvoiceModel> searchData(string searchTerm)
        {
            List<InvoiceModel> filterInvoices = new List<InvoiceModel>();

            if (allInvoices != null)
            {
                totalVat = 0.0;
                totalAmount = 0.0;
                totalWithAmount = 0.0;

                foreach (var invoice in allInvoices)
                {
                    if (invoice.invnoCLD.ToLower().Contains(searchTerm.ToLower()) || invoice.salesManCLD.ToLower().Contains(searchTerm.ToLower()) || invoice.paymentTypeCLD.ToLower().Contains(searchTerm.ToLower()) || invoice.orderTypeCLD.ToLower().Contains(searchTerm.ToLower()) || invoice.custIDCLD.ToLower().Contains(searchTerm.ToLower()))
                    {
                        filterInvoices.Add(invoice);

                        totalVat += invoice.vatCLD;
                        totalAmount += invoice.totalAmountCLD;
                        totalWithAmount += invoice.totalWithVatCLD;
                    }
                }
            }
            return filterInvoices;
        }

        public async Task<List<InvoiceModel>> searchDateWise(string StartingDate, string EndingDate)
        {
            

            List<InvoiceModel> temp_allInvoices = await firebaseHelper.GetSalesDateWise(StartingDate, EndingDate); ;

            if (temp_allInvoices != null)
            {
                if (temp_allInvoices.Count == 0)
                {
                    return temp_allInvoices;
                }
                else
                {
                    allInvoices.Clear();

                    allInvoices = temp_allInvoices;

                    totalVat = 0.0;
                    totalAmount = 0.0;
                    totalWithAmount = 0.0;

                    foreach (var invoice in allInvoices)
                    {
                        //InvoicesInfo.Add(new InvoiceInfo(invoice.invnoCLD, invoice.salesManCLD, invoice.totalAmountCLD, invoice.totalWithVatCLD, invoice.dateCLD, invoice.vatCLD, invoice.paymentTypeCLD, invoice.orderTypeCLD, invoice.custIDCLD));

                        totalVat += invoice.vatCLD;
                        totalAmount += invoice.totalAmountCLD;
                        totalWithAmount += invoice.totalWithVatCLD;
                    }
                }
            }

            return allInvoices;
        }

        public async Task<List<InvoiceModel>> allSalesRecords()
        {
            

            List<InvoiceModel> temp_allInvoices = await firebaseHelper.GetAllSales();



            if (temp_allInvoices != null)
            {
                if (temp_allInvoices.Count == 0)
                {
                    return temp_allInvoices;
                }
                else
                {
                    allInvoices.Clear();

                    allInvoices = temp_allInvoices;

                    totalVat = 0.0;
                    totalAmount = 0.0;
                    totalWithAmount = 0.0;

                    foreach (var invoice in allInvoices)
                    {
                        // InvoicesInfo.Add(new InvoiceInfo(invoice.invnoCLD, invoice.salesManCLD, invoice.totalAmountCLD, invoice.totalWithVatCLD, invoice.dateCLD, invoice.vatCLD, invoice.paymentTypeCLD, invoice.orderTypeCLD, invoice.custIDCLD));

                        totalVat += invoice.vatCLD;
                        totalAmount += invoice.totalAmountCLD;
                        totalWithAmount += invoice.totalWithVatCLD;
                    }
                }
            }

            return allInvoices;
        }

        public string getAmounts()
        {
            string txtAmounts = totalVat.ToString("0.##") + " " + totalAmount.ToString("0.##") + " " + totalWithAmount.ToString("0.##");
            return txtAmounts.Trim();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }

    }
}
