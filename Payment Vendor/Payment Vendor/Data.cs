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
        public String BKNAME;
        public String BANKADDRESS;
        public String BANKCITY;
        public String BANKCODE;
        public String BKBENE;
        public String BKACCT;
        public String CURRENCY;
        public String AMOUNT;
        public String DESCRIPTION;
        public String DESCRIPTION2;
        public String EMAIL;
        public String TRANSACTIONTYPE;
        public String RESIDENTSTATUS;
        public String CITIZENSTATUS;
            }
}
