using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACCPAC.Advantage;
using System.Net;
using Microsoft.Office.Interop.Excel;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Linq;
using System.IO;
using System.Globalization;

namespace BankPaymentMonthly
{
    public partial class BankPaymentMonthly : Form
    {
        private ACCPAC.Advantage.View csQry;
        private Session session;
        private DBLink mDBLinkCmpRW;
        private List<Tuple<string, string, double>> BankSubTotal = new List<Tuple<string, string, double>>();
        private const int Keysize = 256;
        private const int DerivationIterations = 1000;
        private string passphrase = "sage300accpac";
        private string passfile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/CB Bank Payment/Pass.txt";
        private string recipientfile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/CB Bank Payment/Recipient.txt";



        public BankPaymentMonthly()
        {
            InitializeComponent();
        }

        private void Connect()
        {
            session = new Session();

            session.Init("", "XX", "XX1000", "63A");
            session.Open("ADMIN", "ADMS4G3COM1", "KMKDAT", DateTime.Today, 0);
            mDBLinkCmpRW = session.OpenDBLink(DBLinkType.Company, DBLinkFlags.ReadWrite);

            csQry = mDBLinkCmpRW.OpenView("CS0120");
        }

        private void BankPaymentMonthly_Load(object sender, EventArgs e)
        {
            try
            {
                Connect();

                string StringSql = @"SELECT b.BANKCODE, a.BANKNAME, b.FISCYR, b.PERIOD, b.DATE, 
                                            b.REFERENCE, b.TEXTDESC, b.BK2GLCURSR, b.BANKAMOUNT, 
                                            b.BATCHID, b.ENTRYNO, c.LEGALNAME
                                   FROM CBBANK a 
                                   INNER JOIN CBPJHD b ON a.BANKCODE = b.BANKCODE
                                   INNER JOIN CSCOM c ON b.AUDTORG = c.ORGID
                                   WHERE  b.BANKAMOUNT < 0 AND
                                          MONTH(CONVERT(varchar, b.DATE,  103)) = MONTH(GETDATE()) AND
                                          YEAR(CONVERT(varchar, b.DATE,  103)) = YEAR(GETDATE())
                                   ORDER BY b.BANKCODE, b.DATE";


                csQry.Browse(StringSql, true);
                csQry.InternalSet(256);

                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/CB Bank Payment/Monthly");
                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/CB Bank Payment/Monthly/CB Bank Payment KMKDAT Monthly " + DateTime.Now.ToString("yyyy'-'MM'-'dd'_'HH'-'mm'-'ss") + ".xlsx";
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                object misValue = System.Reflection.Missing.Value;
                Workbook xlWorkBook = xlApp.Workbooks.Add(misValue);


                //-----------------------------SHEET 1---------------------------------------------------------------------
                Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Name = "BANK PAYMENT";

                xlWorkSheet.Cells[1, 1].EntireRow.Font.Bold = true;
                xlWorkSheet.Cells[1, 1] = "'" + "BANK CODE"; //BANKCODE
                xlWorkSheet.Cells[1, 2] = "'" + "BANK NAME"; //BANKNAME
                xlWorkSheet.Cells[1, 3] = "'" + "YEAR"; //FISCYR
                xlWorkSheet.Cells[1, 4] = "'" + "PRD"; //PERIOD
                xlWorkSheet.Cells[1, 5] = "'" + "DATE"; //DATE
                xlWorkSheet.Cells[1, 6] = "'" + "REFERENCE"; //REFERENCE
                xlWorkSheet.Cells[1, 7] = "'" + "BATCH NO"; //BATCHID
                xlWorkSheet.Cells[1, 8] = "'" + "ENTRY NO"; //ENTRYNO
                xlWorkSheet.Cells[1, 9] = "'" + "DESCRIPTION";//TEXTDESC
                xlWorkSheet.Cells[1, 10] = "'" + "CURRENCY";//BK2GLCURSR
                xlWorkSheet.Cells[1, 11] = "'" + "BANK AMOUNT";//BANKAMOUNT


                int numrow = 2;
                double tempAmount = 0;
                string IDTemp = "";
                string NameTemp = "";

                while (csQry.Fetch(false))
                {
                    DateTime transactionDate = DateTime.ParseExact(csQry.Fields[4].Value.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    double amount = double.Parse(csQry.Fields[8].Value.ToString()) * -1;

                    if (csQry.Fields[0].Value.ToString() != IDTemp && IDTemp != "")
                    {
                        xlWorkSheet.Cells[numrow, 1].EntireRow.Interior.Color = XlRgbColor.rgbLightSkyBlue;
                        xlWorkSheet.Cells[numrow, 1].EntireRow.Font.Bold = true;
                        xlWorkSheet.Cells[numrow, 1] = "'" + IDTemp;
                        xlWorkSheet.Cells[numrow, 1].Font.Bold = false;
                        xlWorkSheet.Cells[numrow, 2] = "'" + NameTemp;
                        xlWorkSheet.Cells[numrow, 2].Font.Bold = false;
                        xlWorkSheet.Cells[numrow, 10] = "Sub Total";
                        xlWorkSheet.Cells[numrow, 11] = tempAmount;
                        BankSubTotal.Add(new Tuple<string, string, double>(IDTemp, NameTemp, tempAmount));
                        tempAmount = 0;
                        numrow = numrow + 2;
                    }

                    xlWorkSheet.Cells[numrow, 1] = "'" + csQry.Fields[0].Value.ToString();
                    xlWorkSheet.Cells[numrow, 2] = "'" + csQry.Fields[1].Value.ToString();
                    xlWorkSheet.Cells[numrow, 3] = "'" + csQry.Fields[2].Value.ToString();
                    xlWorkSheet.Cells[numrow, 4] = "'" + csQry.Fields[3].Value.ToString();
                    xlWorkSheet.Cells[numrow, 5] = "'" + transactionDate.ToString("MM/dd/yyyy");
                    xlWorkSheet.Cells[numrow, 6] = "'" + csQry.Fields[5].Value.ToString();
                    xlWorkSheet.Cells[numrow, 7] = "'" + csQry.Fields[9].Value.ToString();
                    xlWorkSheet.Cells[numrow, 8] = "'" + csQry.Fields[10].Value.ToString();
                    xlWorkSheet.Cells[numrow, 9] = "'" + csQry.Fields[6].Value.ToString();
                    xlWorkSheet.Cells[numrow, 10] = "'" + csQry.Fields[7].Value.ToString();
                    xlWorkSheet.Cells[numrow, 11] = amount;

                    IDTemp = csQry.Fields[0].Value.ToString();
                    NameTemp = csQry.Fields[1].Value.ToString();
                    tempAmount = tempAmount + amount;
                    numrow++;
                }
                xlWorkSheet.Cells[numrow, 1].EntireRow.Interior.Color = XlRgbColor.rgbLightSkyBlue;
                xlWorkSheet.Cells[numrow, 1].EntireRow.Font.Bold = true;
                xlWorkSheet.Cells[numrow, 1] = "'" + IDTemp;
                xlWorkSheet.Cells[numrow, 1].Font.Bold = false;
                xlWorkSheet.Cells[numrow, 2] = "'" + NameTemp;
                xlWorkSheet.Cells[numrow, 2].Font.Bold = false;
                xlWorkSheet.Cells[numrow, 10] = "Sub Total";
                xlWorkSheet.Cells[numrow, 11] = tempAmount;
                BankSubTotal.Add(new Tuple<string, string, double>(IDTemp, NameTemp, tempAmount));

                xlWorkSheet.Columns.AutoFit();

                //----------------------------------SHEET 2----------------------------------------------------------

                Worksheet xlWorkSheet2 = (Worksheet)xlWorkBook.Worksheets.get_Item(2);
                xlWorkSheet2.Name = "SUMMARY";

                xlWorkSheet2.Cells[1, 1].EntireRow.Font.Bold = true;
                xlWorkSheet2.Cells[1, 1] = "'" + "BANK ID";
                xlWorkSheet2.Cells[1, 2] = "'" + "BANK NAME";
                xlWorkSheet2.Cells[1, 3] = "'" + "TOTAL AMOUNT";

                int numrow2 = 2;
                foreach (Tuple<string, string, double> tuple in BankSubTotal)
                {
                    xlWorkSheet2.Cells[numrow2, 1] = "'" + tuple.Item1;
                    xlWorkSheet2.Cells[numrow2, 2] = "'" + tuple.Item2;
                    xlWorkSheet2.Cells[numrow2, 3] = tuple.Item3;
                    numrow2++;
                }

                xlWorkSheet2.Columns.AutoFit();

                xlWorkBook.SaveAs(filePath);

                xlWorkBook.Close();
                xlApp.Quit();

                //SendEmail(filePath);

                session.Dispose();
                System.Windows.Forms.Application.Exit();
            }
            catch (SessionException)
            {
                MessageBox.Show("Please connect to Sage Server first", "Connection Error");
                System.Windows.Forms.Application.Exit();
            }
            catch (Exception except)
            {
                MessageBox.Show(except.ToString());
                System.Windows.Forms.Application.Exit();
            }
        }

        private void SendEmail(string fileLocation)
        {
            string line;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("afif.hantriono@kmkonline.co.id");

            StreamReader file = new StreamReader(recipientfile);
            while ((line = file.ReadLine()) != null)
            {
                mail.To.Add(line);
            }
            file.Close();

            mail.Subject = "CB Bank Payment KMKDAT Monthly " + DateTime.Now.ToString("yyyy'-'MM'-'dd'_'HH'-'mm'-'ss");
            mail.Body = "";

            Attachment attachment;
            attachment = new Attachment(fileLocation);
            attachment.ContentDisposition.FileName = "CB Bank Payment KMKDAT Monthly " + DateTime.Now.ToString("yyyy'-'MM'-'dd'_'HH'-'mm'-'ss") + ".xlsx";
            mail.Attachments.Add(attachment);

            string pass = File.ReadLines(passfile).First();

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new NetworkCredential("afif.hantriono@kmkonline.co.id", Decrypt(pass, passphrase));
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }

        private static string Encrypt(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        private static string Decrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
    
}
