using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Vendor
{
    class PaymentColumn
    {
        public string rowNumber;
        public string batchID;
        public string vendorID;
        public string vendorName;
        public string description;
        public string date;
        public string bankName;
        public string beneficiaryName;
        public string accountNumber;
        public string amount;
        public string currency;
        public string type;
        public string bankCode;

        public PaymentColumn(string rowNumber, string batchID, string vendorID, string vendorName, string description, string date, string bankName,
                             string beneficiaryName, string accountNumber, string amount, string currency, string type,
                             string bankCode)
        {
            this.rowNumber = rowNumber;
            this.batchID = batchID;
            this.vendorID = vendorID;
            this.vendorName = vendorName;
            this.description = description;
            this.date = date;
            this.bankName = bankName;
            this.beneficiaryName = beneficiaryName;
            this.accountNumber = accountNumber;
            this.amount = amount;
            this.currency = currency;
            this.type = type;
            this.bankCode = bankCode;
        }

        public string ROWNUMBER
        {
            get { return rowNumber; }
            set { rowNumber = value; }
        }

        public string BATCHID
        {
            get { return batchID; }
            set { batchID = value; }
        }

        public string VENDORID
        {
            get { return vendorID; }
            set { vendorID = value; }
        }

        public string VENDORNAME
        {
            get { return vendorName; }
            set { vendorName = value; }
        }

        public string DESCRIPTION
        {
            get { return description; }
            set { description = value; }
        }

        public string DATE
        {
            get { return date; }
            set { date = value; }
        }

        public string BANKNAME
        {
            get { return bankName; }
            set { bankName = value; }
        }

        public string BENEFICIARYNAME
        {
            get { return beneficiaryName; }
            set { beneficiaryName = value; }
        }

        public string ACCOUNTNUMBER
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }

        public string AMOUNT
        {
            get { return amount; }
            set { amount = value; }
        }

        public string CURRENCY
        {
            get { return currency; }
            set { currency = value; }
        }

        public string TYPE
        {
            get { return type; }
            set { type = value; }
        }

        public string BANKCODE
        {
            get { return bankCode; }
            set { bankCode = value; }
        }
    }
}
