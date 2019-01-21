using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Payment_Plan
{
    class InvoiceColumn
    {
        public string idVendor;
        public string vendorName;
        public string documentNo;
        public string description;
        public string dateDoc;
        public string dateDue;
        public string currency;
        public string originalInvoice;
        public string daysOver;
        public string balanceSource;
        public string balanceFunction;

        public InvoiceColumn(string idVendor, string vendorName, string documentNo, string description, string dateDoc,
                             string dateDue, string currency, string originalInvoice, string daysOver, string balanceSource, 
                             string balanceFunction)
        {
            this.idVendor = idVendor;
            this.vendorName  =  vendorName;
            this.documentNo = documentNo;
            this.description = description;
            this.dateDoc = dateDoc;
            this.dateDue = dateDue;
            this.currency = currency;
            this.originalInvoice = originalInvoice;
            this.daysOver = daysOver;
            this.balanceSource = balanceSource;
            this.balanceFunction = balanceFunction;
        }

        public string IDVendor
        {
            get { return idVendor; }
            set { idVendor = value; }
        }

        public string VendorName
        {
            get { return vendorName; }
            set { vendorName = value; }
        }

        public string DocumentNo
        {
            get { return documentNo; }
            set { documentNo = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string DateDoc
        {
            get { return dateDoc; }
            set { dateDoc = value; }
        }

        public string DateDue
        {
            get { return dateDue; }
            set { dateDue = value; }
        }

        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        public string OriginalInvoice
        {
            get { return originalInvoice; }
            set { originalInvoice = value; }
        }

        public string DaysOver
        {
            get { return daysOver; }
            set { daysOver = value; }
        }

        public string BalanceSource
        {
            get { return balanceSource; }
            set { balanceSource = value; }
        }

        public string BalanceFunction
        {
            get { return balanceFunction; }
            set { balanceFunction = value; }
        }
    }
}
