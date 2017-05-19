using System;
using OpenQA.Selenium.Firefox;

namespace o2_gifts
{
    class GVars
    {
        private static Random _rnd;
        private static FirefoxDriver _firefoxDriver;

        public static Random Random
        {
            get { return _rnd; }
        }
        public static FirefoxDriver FirefoxDriver
        {
            get { return _firefoxDriver; }
        }
    }
}
