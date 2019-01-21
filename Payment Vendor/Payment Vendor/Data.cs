using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Payment_Vendor
{
    [DelimitedRecord(",")]
    public class Data
    {
        public string BKNAME;
        public string BANKADDRESS;
        public string BANKCITY;
        public string BANKCODE;
        public string BKBENE;
        public string BKACCT;
        public string CURRENCY;
        public string AMOUNT;
        public string DESCRIPTION;
        public string DESCRIPTION2;
        public string EMAIL;
        public string TRANSACTIONTYPE;
        public string RESIDENTSTATUS;
        public string CITIZENSTATUS;
            }
}
