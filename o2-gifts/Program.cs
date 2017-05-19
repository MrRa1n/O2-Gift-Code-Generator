using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace o2_gifts
{
    class Program
    {
        static Meths mtd = new Meths();

        static void Main(string[] args)
        {
            mtd.startMethod();
        }
    }
}
