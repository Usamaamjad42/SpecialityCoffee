using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace myspecialtycoffee.Model
{
    public class InvoiceInfo : INotifyPropertyChanged
    {
        private string invnoCLD;
        private string salesManCLD;
        private double totalAmountCLD;
        private double totalWithVatCLD;
        private string dateCLD;
        private double vatCLD;
        private string paymentTypeCLD;
        private string orderTypeCLD;
        private string custIDCLD;

        public string InvnoCLD
        {
            get { return invnoCLD; }
            set
            {
                this.invnoCLD = value;
                RaisePropertyChanged("InvnoCLD");
            }
        }

        public string SalesManCLD
        {
            get { return salesManCLD; }
            set
            {
                this.salesManCLD = value;
                RaisePropertyChanged("SalesManCLD");
            }
        }

        public double TotalAmountCLD
        {
            get { return totalAmountCLD; }
            set
            {
                this.totalAmountCLD = value;
                RaisePropertyChanged("TotalAmountCLD");
            }
        }

        public double TotalWithVatCLD
        {
            get { return totalWithVatCLD; }
            set
            {
                this.totalWithVatCLD = value;
                RaisePropertyChanged("TotalWithVatCLD");
            }
        }

        public string DateCLD
        {
            get { return dateCLD; }
            set
            {
                this.dateCLD = value;
                RaisePropertyChanged("DateCLD");
            }
        }

        public double VatCLD
        {
            get { return vatCLD; }
            set
            {
                this.vatCLD = value;
                RaisePropertyChanged("VatCLD");
            }
        }

        public string PaymentTypeCLD
        {
            get { return paymentTypeCLD; }
            set
            {
                this.paymentTypeCLD = value;
                RaisePropertyChanged("PaymentTypeCLD");
            }
        }

        public string OrderTypeCLD
        {
            get { return orderTypeCLD; }
            set
            {
                this.orderTypeCLD = value;
                RaisePropertyChanged("OrderTypeCLD");
            }
        }

        public string CustIDCLD
        {
            get { return custIDCLD; }
            set
            {
                this.custIDCLD = value;
                RaisePropertyChanged("CustIDCLD");
            }
        }

        public InvoiceInfo(string invnoCLD, string salesManCLD, double totalAmountCLD, double totalWithVatCLD, string dateCLD, double vatCLD, string paymentTypeCLD,string orderTypeCLD, string custIDCLD)
        {
            this.InvnoCLD = invnoCLD;
            this.SalesManCLD = salesManCLD;
            this.TotalAmountCLD = totalAmountCLD;
            this.TotalWithVatCLD = totalWithVatCLD;

            ////////Substring for split Date////////
            dateCLD = dateCLD.Substring(0, 8).Trim();
            this.DateCLD = dateCLD.Trim();

            this.VatCLD = vatCLD;
            this.PaymentTypeCLD = paymentTypeCLD;
            this.OrderTypeCLD = orderTypeCLD;
            this.CustIDCLD = custIDCLD;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

       
    }
}
