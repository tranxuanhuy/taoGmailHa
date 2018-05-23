using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taoGmailHa
{
    class Program
    {
        static void Main(string[] args)
        {
            int time = 5000;
            FirefoxProfileManager profileManager = new FirefoxProfileManager();
            FirefoxProfile profile = profileManager.GetProfile("default");
            //IWebDriver driver = new FirefoxDriver();
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            //createGmailaccount(driver);

            //LoginGmail(driver, wait);

            //sendConfirmation(driver);
            //string code = verifyConfirmation();
            //addMailAfterConfirmation(driver, code);

            IWebDriver driver1 = new FirefoxDriver(profile);
            driver1.Navigate().GoToUrl("http://sms-activate.ru/");
            driver1.FindElement(By.XPath("/html/body/div[4]/div/div[4]/div[2]/div[1]/form/table/tbody/tr[7]/td[2]/label/span[1]")).Click();
            System.Threading.Thread.Sleep(time);
            var buttonLikeList = driver1.FindElements(By.CssSelector("tr.tabbed:nth-child(7) > td:nth-child(2) > label:nth-child(1) > a:nth-child(5)"));
            Console.WriteLine("a");



            //driver.FindElement(By.Id("BirthDay")).SendKeys("12");
            //System.Threading.Thread.Sleep(time);
            //driver.FindElement(By.Id("BirthYear")).SendKeys("1990");
            //System.Threading.Thread.Sleep(time);
            ////driver.FindElement(By.Id("RecoveryPhoneNumber")).SendKeys(phone);
            //System.Threading.Thread.Sleep(time);
            //driver.FindElement(By.Id("SkipCaptcha")).Click();
            //System.Threading.Thread.Sleep(time);
            //driver.FindElement(By.Id("TermsOfService")).Click();
            //System.Threading.Thread.Sleep(time);
            //driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[1]/div/form/div[5]/fieldset/label[1]/span/div/div[1]")).Click();
            //System.Diagnostics.Process cmd = System.Diagnostics.Process.Start("pressCreateGmail.exe");
            //cmd.WaitForExit();
            ////driver.FindElement(By.Id(":7")).Click();
            //System.Threading.Thread.Sleep(time);
            //driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[1]/div/form/div[6]/label/div/div")).Click();
            //cmd = System.Diagnostics.Process.Start("pressCreateGmail.exe");
            //cmd.WaitForExit();
            ////driver.FindElement(By.Id(":f")).Click();
            //System.Threading.Thread.Sleep(time);
            //driver.FindElement(By.Id("TermsOfService")).Submit();

            ////sang trang 2 confirm sdt
            //System.Threading.Thread.Sleep(time);
            //wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Id("signupidvinput")));
            //System.Threading.Thread.Sleep(time);
            //driver.FindElement(By.Id("signupidvmethod-sms")).Click();
            //System.Threading.Thread.Sleep(time);
            //driver.FindElement(By.XPath("/html/body/div[1]/div[2]/form/div[2]/input")).Click();

            //WriteLinePostingLog(mail + "@gmail.com\t" + pass);
        }

        private static void LoginGmail(IWebDriver driver, WebDriverWait wait)
        {
            string pvas = File.ReadLines("pvas.txt").First();
            try
            {
                //vao email lay link confirmation
                driver.Navigate().GoToUrl("https://accounts.google.com/ServiceLogin?service=mail&continue=https://mail.google.com/mail/#identifier");
                //driver.FindElement(By.Id("Email")).SendKeys(pvas.Split('\t').Last());
                driver.FindElement(By.Id("identifierId")).SendKeys(pvas);
                driver.FindElement(By.Id("identifierId")).SendKeys(Keys.Enter);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Second exception caught.", e);
            }
            try
            {
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("password")));


                {
                    driver.FindElement(By.Name("password")).SendKeys("B1nbin!@#");
                }
                driver.FindElement(By.Name("password")).SendKeys(Keys.Enter);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Second exception caught.", e);
            }
        }

        private static void createGmailaccount(IWebDriver driver)
        {
            int time;
            string mail, pass;
            
            WebDriverWait wait;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-popup-blocking");
            options.AddArguments("--disable-notifications");

            //var userAgent = ReadRandomLineOfFile("useragentswitcher.txt");
            //options.AddArgument("--user-agent="+ userAgent);
            //IWebDriver driver = new ChromeDriver(@"C:\", options); //<-Add your path

            time = 5000;
            mail = ReadRandomLineOfFile("usaname.txt");
            System.Threading.Thread.Sleep(1000);
            mail += ReadRandomLineOfFile("usaname.txt") + Path.GetRandomFileName().Replace(".", "");
            pass = "B1nbin!@#";
            FirefoxProfileManager profileManager = new FirefoxProfileManager();
            //FirefoxProfile profile = profileManager.GetProfile(File.ReadAllLines("config.txt")[3].Split('=')[1]);
            //FirefoxProfile profile = profileManager.GetProfile("posting");
            
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            //string pvas = File.ReadLines("pvas.txt").First();



            driver.Navigate().GoToUrl("https://accounts.google.com/signUp?service=mail");
            System.Threading.Thread.Sleep(time);
            driver.FindElement(By.Id("firstName")).SendKeys(ReadRandomLineOfFile("usaname.txt"));
            System.Threading.Thread.Sleep(time);
            driver.FindElement(By.Id("lastName")).SendKeys(ReadRandomLineOfFile("usaname.txt"));
            //System.Threading.Thread.Sleep(time);
            driver.FindElement(By.Id("username")).SendKeys(mail);
            System.Threading.Thread.Sleep(time);
            driver.FindElement(By.Name("Passwd")).SendKeys(pass);
            System.Threading.Thread.Sleep(time);
            driver.FindElement(By.Name("ConfirmPasswd")).SendKeys(pass);
            System.Threading.Thread.Sleep(time);
            driver.FindElement(By.Name("ConfirmPasswd")).SendKeys(Keys.Enter);


            string phone = "+639102714968";
            System.Threading.Thread.Sleep(time);
            driver.FindElement(By.Id("phoneNumberId")).SendKeys(phone);
            driver.FindElement(By.Id("phoneNumberId")).SendKeys(Keys.Enter);

            string verCode = "929193";
            System.Threading.Thread.Sleep(time);
            driver.FindElement(By.Id("code")).SendKeys(verCode);
            driver.FindElement(By.Id("code")).SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(time);

            //driver.FindElement(By.Id("month")).Click();
            //System.Threading.Thread.Sleep(time);
            driver.FindElement(By.Id("month")).SendKeys("j");
            driver.FindElement(By.Id("day")).SendKeys("10");
            driver.FindElement(By.Id("year")).SendKeys("1990");
            driver.FindElement(By.Id("gender")).SendKeys("f");
            System.Threading.Thread.Sleep(time);
            //driver.FindElement(By.Id("gender")).SendKeys(Keys.Down);
            driver.FindElement(By.Id("year")).SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(time);


            //keo xuong policy va bam agree
            IWebElement body = driver.FindElement(By.TagName("body"));
            body.SendKeys(Keys.Tab+Keys.End);
            body.SendKeys(Keys.Tab + Keys.End + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab );
            body.SendKeys(Keys.Tab + Keys.End + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Enter);
            //driver.FindElement(By.Id("phoneNumberId")).SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(time*4);

            //agree policy
            //driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[1]/div[2]/form/div[2]/div/div/div/div[2]/div/div[1]/div/div[2]")).Click();
            //System.Threading.Thread.Sleep(time);

            ////To continue, first verify it's you
            //driver.FindElement(By.Name("password")).SendKeys(pass);
            //driver.FindElement(By.Name("password")).SendKeys(Keys.Enter);
            WriteLineEmptyFile(mail);
            WriteLinePostingLog(mail);
        }

        private static void addMailAfterConfirmation(IWebDriver driver, string code)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            driver.Navigate().GoToUrl("https://accounts.google.com/ServiceLogin?service=mail&continue=https://mail.google.com/mail/#identifier");
            try
            {
                driver.Navigate().GoToUrl("https://mail.google.com/mail/?ui=html&zy=h");
            }
            catch (System.Exception e)
            {
                Console.WriteLine("{0} Second exception caught.", e);
            }
            System.Threading.Thread.Sleep(5000);
            driver.FindElement(By.LinkText("Settings")).Click();
            driver.FindElement(By.LinkText("Forwarding and POP/IMAP")).Click();
            //nhap code confirm
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("fwvc")));
            driver.FindElement(By.Name("fwvc")).SendKeys(code);
            driver.FindElement(By.Name("fwvc")).Submit();
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("/html/body/table[3]/tbody/tr/td[2]/table[1]/tbody/tr/td[2]/div/table/tbody/tr[2]/td[2]/form[1]/span[2]/input[1]")));
            driver.FindElement(By.XPath("/html/body/table[3]/tbody/tr/td[2]/table[1]/tbody/tr/td[2]/div/table/tbody/tr[2]/td[2]/form[1]/span[2]/input[1]")).Click();
            driver.FindElement(By.XPath("/html/body/table[3]/tbody/tr/td[2]/table[1]/tbody/tr/td[2]/div/table/tbody/tr[2]/td[2]/form[1]/input[2]")).Click();

        }

        private static string verifyConfirmation()
        {
            string code = "";
            while (code == "")
            {
                MyThread10.getMailGoogle(1, 0, 2);
                //load link confirm len ff
                var alinks = File.ReadAllLines("socks.txt");
                try
                {
                    alinks = File.ReadAllLines("mailConfirm1.txt");
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Second exception caught.", e);
                }
                foreach (string link in alinks)
                    if (File.ReadLines("pvas.txt").First().Contains(link.Split('\t')[0]))
                    {
                        code = link.Split('\t')[1];
                        break;
                    }
            }
            return code;
        }

        private static void sendConfirmation(IWebDriver driver)
        {
            System.Threading.Thread.Sleep(5000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            driver.Navigate().GoToUrl("https://mail.google.com/mail/?ui=html&zy=h");
            try
            {
                driver.FindElement(By.ClassName("maia-button")).Submit();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("{0} Second exception caught.", e);
            }
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.LinkText("Settings")));
            driver.FindElement(By.LinkText("Settings")).Click();
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.LinkText("Forwarding and POP/IMAP")));
            driver.FindElement(By.LinkText("Forwarding and POP/IMAP")).Click();
            string code = "";
            var elements = driver.PageSource.Split('/');
            foreach (string element in elements)
            {
                if (element.Length == 13)
                {
                    code = element;
                    break;
                }
            }
            driver.Navigate().GoToUrl("https://mail.google.com/mail/u/0/h/" + code + "/?v=prufw");
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("/html/body/div[2]/form/input[1]")));
            driver.FindElement(By.XPath("/html/body/div[2]/form/input[1]")).SendKeys("getcryptotab.com@gmail.com");
            driver.FindElement(By.XPath("/html/body/div[2]/form/input[1]")).Submit();
            if (driver.FindElement(By.TagName("body")).Text.Contains("You already have the forwarding address"))
                return;
            try
            {
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("nvp_bu_pfwd")));
            }
            catch (System.Exception e)
            {
                Console.WriteLine("{0} Second exception caught.", e);
                return;
            }
            driver.FindElement(By.Name("nvp_bu_pfwd")).Submit();
        }

        public static void WriteLinePostingLog(string content = "", string filewrite = "adsLog.txt")
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filewrite, true))
            {

                file.WriteLine(content);

            }
        }

        public static void WriteLineEmptyFile(string content = "", string filewrite = "pvas.txt")
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filewrite, false))
            {

                file.WriteLine(content.ToLower());

            }
        }

        public static string ReadRandomLineOfFile(string file)
        {
            string[] lines = File.ReadAllLines(file); //i hope that the file is not too big
            Random rand = new Random();
            return lines[rand.Next(lines.Length)];
        }
    }
}
