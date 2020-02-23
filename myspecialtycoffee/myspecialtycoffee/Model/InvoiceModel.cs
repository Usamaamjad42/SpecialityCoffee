using System;
using System.Collections.Generic;
using System.Text;

namespace myspecialtycoffee.Model
{
    public class InvoiceModel
    {
        public string invnoCLD { get; set; }
        public string salesManCLD { get; set; }
        public double totalAmountCLD { get; set; }
        public double totalWithVatCLD { get; set; }
        public string dateCLD { get; set; } 
        public double vatCLD { get; set; }
        public string paymentTypeCLD { get; set; }
        public string orderTypeCLD { get; set; }
        public string custIDCLD { get; set; }
    }
}
