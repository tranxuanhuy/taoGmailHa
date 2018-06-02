using EAGetMail;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

namespace taoGmailHa
{
    public class MyThread10
    {
        

        public static int getMailConfirm(string pvas, int reset = 0, int numMail = 0)
        {
            if (numMail == 0)
            {
                numMail = File.ReadAllLines("pvas.txt").Count();
            }
            //System.Threading.Thread.Sleep(120000);
            int stt = int.Parse(pvas.Split('\t')[4]);
            bool outlook = pvas.Contains("@outlook.com");
            if (outlook)
            {
                getMailOutlook(stt, numMail, reset);
            }
            else
            {
                getMailGoogle(stt, numMail, reset);
            }
            return stt;
        }

        internal void Thread1()
        {
            Thread thr = Thread.CurrentThread;
            string threadPerTotalthread = thr.Name;


            Demo w = new Demo();
            for (int i = 0; i < 1; i++)
            {

                try
                {
                    string emailCreatedandpass=IndividualThread(threadPerTotalthread);

                    
                        w.WriteToFileThreadSafe(emailCreatedandpass, "adsLog1.txt");
                    
                }
                catch (Exception e)
                {

                    Console.WriteLine("{0} Second exception caught.", e);
                }
            }
        }

        public static string IndividualThread(string threadPerTotalthread)
        {
            int time = 5000;
            FirefoxProfileManager profileManager = new FirefoxProfileManager();
            FirefoxProfile profile = profileManager.GetProfile("default");
            ChangeUAFirefox(profile);

            IWebDriver driver1 = null;

            IWebDriver driver = new FirefoxDriver(profile);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            //lay code
            var code = "";
            string emailAndpass=createGmailaccount(driver, driver1, out code, threadPerTotalthread);

            //LoginGmail(driver, wait);

            sendConfirmation(driver);
            code = verifyConfirmation(threadPerTotalthread);
            addMailAfterConfirmation(driver, code);

            Console.WriteLine("a");
            driver.Quit();
            //driver1.Quit();
            return emailAndpass;
        }

        public static void ChangeUAFirefox(FirefoxProfile profile)
        {
            var userAgent = ReadRandomLineOfFile("useragentswitcher.txt");
            profile.SetPreference("general.useragent.override", userAgent);
        }

        private static string createGmailaccount(IWebDriver driver, IWebDriver driver1, out string verCode,string threadPerTotalthread)
        {
            int time;
            string mail, pass;

            WebDriverWait wait;
            //ChromeOptions options = new ChromeOptions();
            //options.AddArgument("--disable-popup-blocking");
            //options.AddArguments("--disable-notifications");

            //var userAgent = ReadRandomLineOfFile("useragentswitcher.txt");
            //options.AddArgument("--user-agent="+ userAgent);
            //IWebDriver driver = new ChromeDriver(@"C:\", options); //<-Add your path

            time = 5000;
            mail = ReadRandomLineOfFile("usaname.txt");
            System.Threading.Thread.Sleep(1000);
            mail += ReadRandomLineOfFile("usaname.txt") + Path.GetRandomFileName().Replace(".", "");
            pass = "B1nbin!@#2";
            pass = GeneratePassword(10, 3);
            FirefoxProfileManager profileManager = new FirefoxProfileManager();
            //FirefoxProfile profile = profileManager.GetProfile(File.ReadAllLines("config.txt")[3].Split('=')[1]);
            //FirefoxProfile profile = profileManager.GetProfile("posting");

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            //string pvas = File.ReadLines("pvas.txt").First();



            driver.Navigate().GoToUrl("https://accounts.google.com/signUp?service=mail");
            
            driver.FindElement(By.Id("firstName")).SendKeys(ReadRandomLineOfFile("usaname.txt"));
            
            driver.FindElement(By.Id("lastName")).SendKeys(ReadRandomLineOfFile("usaname.txt"));
            //
            driver.FindElement(By.Id("username")).SendKeys(mail);
            
            driver.FindElement(By.Name("Passwd")).SendKeys(pass);
            
            driver.FindElement(By.Name("ConfirmPasswd")).SendKeys(pass);
            
            IWebElement body = driver.FindElement(By.TagName("body"));
            body.SendKeys( Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Enter);


            string phone = "+84932546722";
            string idphone = "+639102714968";
            //getNewphonenumber(time, driver1, out phone,out idphone, threadPerTotalthread);
            //getNewphonenumberAPI(time, out phone, out idphone, threadPerTotalthread);

            System.Threading.Thread.Sleep(time);
            driver.FindElement(By.Id("phoneNumberId")).SendKeys("+" + phone);
            body = driver.FindElement(By.TagName("body"));
            body.SendKeys( Keys.Tab + Keys.Tab + Keys.Tab + Keys.Enter);

            verCode = "627953";
            //getVercode(time, driver1, phone, out verCode, threadPerTotalthread);
            //getVercodeAPI(time, idphone, out verCode, threadPerTotalthread);

            //neu sdt loading mai ca 4p ko co code
            if (verCode=="none")
            {
                driver.Quit();

                //xoa sdt trong trang sms.ru, bam nut do de xoa so
                //driver1.FindElement(By.Id("fail_" + idphone)).Click() ;
                
                //driver1.Quit();
                return null;
            }
            System.Threading.Thread.Sleep(time);
            driver.FindElement(By.Id("code")).SendKeys(verCode);
            body = driver.FindElement(By.TagName("body"));
            body.SendKeys(Keys.Tab + Keys.Tab +  Keys.Enter);
            System.Threading.Thread.Sleep(time);
            driver.FindElement(By.Id("phoneNumberId")).Clear();
            driver.FindElement(By.Id("phoneNumberId")).SendKeys(Keys.Tab + "getcryptotab.com@gmail.com");

            //driver.FindElement(By.Id("month")).Click();
            //System.Threading.Thread.Sleep(time);
            driver.FindElement(By.Id("month")).SendKeys("j");
            driver.FindElement(By.Id("day")).SendKeys("10");
            driver.FindElement(By.Id("year")).SendKeys("1990");
            driver.FindElement(By.Id("gender")).SendKeys("f");
            System.Threading.Thread.Sleep(time);
            //driver.FindElement(By.Id("gender")).SendKeys(Keys.Down);
            body = driver.FindElement(By.TagName("body"));
            body.SendKeys(Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Enter);
            System.Threading.Thread.Sleep(time);


            //keo xuong policy va bam agree
            body = driver.FindElement(By.TagName("body"));
            body.SendKeys(Keys.Tab + Keys.End);
            System.Threading.Thread.Sleep(time);
            //body.SendKeys(Keys.Tab + Keys.End + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab );
            //System.Threading.Thread.Sleep(time);
            body.SendKeys(Keys.Tab + Keys.End + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Tab + Keys.Enter);
            //driver.FindElement(By.Id("phoneNumberId")).SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(time * 5);

            //agree policy
            //driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[1]/div[2]/form/div[2]/div/div/div/div[2]/div/div[1]/div/div[2]")).Click();
            //System.Threading.Thread.Sleep(time);

            ////To continue, first verify it's you
            //driver.FindElement(By.Name("password")).SendKeys(pass);
            //driver.FindElement(By.Name("password")).SendKeys(Keys.Enter);
            WriteLineEmptyFile(mail,"pvas"+ threadPerTotalthread.Split(':')[0]);
            //WriteLinePostingLog(mail);
            return mail+"\t"+pass;
        }

        private static void getVercodeAPI(int time, string idphone, out string verCode, string threadPerTotalthread)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            verCode = "";
            string responseFromServer = "STATUS_WAIT_CODE";
            while (!responseFromServer.Contains("STATUS_OK"))
            {
                try
                {
                    // Create a request for the URL. 
                    WebRequest request = WebRequest.Create("http://sms-activate.ru/stubs/handler_api.php?api_key=bA1A1324473d1Ac7447e520A92A27bf2&action=getStatus&id=" + idphone);

                    // If required by the server, set the credentials.
                    request.Credentials = CredentialCache.DefaultCredentials;
                    // Get the response.
                    WebResponse response = request.GetResponse();
                    // Display the status.
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    // Get the stream containing content returned by the server.
                    Stream dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    //Console.WriteLine(responseFromServer);
                    System.Threading.Thread.Sleep(time);
                }
                catch (Exception e)
                {

                    Console.WriteLine("{0} Second exception caught.", e);
                }

                //neu ko lay duoc code, loading mai sau 4p
                    if (stopwatch.Elapsed > TimeSpan.FromMinutes(4))
                {
                    verCode = "none";
                    return ;
                }
            }
            verCode = responseFromServer.Split(':')[1];

        }

        private static void getNewphonenumberAPI(int time, out string phone, out string idphone, string threadPerTotalthread)
        {
            phone = ""; idphone = "";
            string responseFromServer = "";
            while (!responseFromServer.Contains("ACCESS_NUMBER"))
            {
                try
                {
                    // Create a request for the URL. 
                    WebRequest request = WebRequest.Create("http://sms-activate.ru/stubs/handler_api.php?api_key=bA1A1324473d1Ac7447e520A92A27bf2&action=getNumber&service=go&country=" + ReadFileAtLine(1, "config").Split(':')[1]);

                    // If required by the server, set the credentials.
                    request.Credentials = CredentialCache.DefaultCredentials;
                    // Get the response.
                    WebResponse response = request.GetResponse();
                    // Display the status.
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    // Get the stream containing content returned by the server.
                    Stream dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    //Console.WriteLine(responseFromServer);
                    System.Threading.Thread.Sleep(time);
                }
                catch (Exception e)
                {

                    Console.WriteLine("{0} Second exception caught.", e);
                }
            }
            phone = responseFromServer.Split(':')[2];
            idphone = responseFromServer.Split(':')[1];
        }

        public static string GeneratePassword(int Length, int NonAlphaNumericChars)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            string allowedNonAlphaNum = "!@#$%^&*()_-+=[{]};:<>|./?";
            Random rd = new Random();

            if (NonAlphaNumericChars > Length || Length <= 0 || NonAlphaNumericChars < 0)
                throw new ArgumentOutOfRangeException();

            char[] pass = new char[Length];
            int[] pos = new int[Length];
            int i = 0, j = 0, temp = 0;
            bool flag = false;

            //Random the position values of the pos array for the string Pass
            while (i < Length - 1)
            {
                j = 0;
                flag = false;
                temp = rd.Next(0, Length);
                for (j = 0; j < Length; j++)
                    if (temp == pos[j])
                    {
                        flag = true;
                        j = Length;
                    }

                if (!flag)
                {
                    pos[i] = temp;
                    i++;
                }
            }

            //Random the AlphaNumericChars
            for (i = 0; i < Length - NonAlphaNumericChars; i++)
                pass[i] = allowedChars[rd.Next(0, allowedChars.Length)];

            //Random the NonAlphaNumericChars
            for (i = Length - NonAlphaNumericChars; i < Length; i++)
                pass[i] = allowedNonAlphaNum[rd.Next(0, allowedNonAlphaNum.Length)];

            //Set the sorted array values by the pos array for the rigth posistion
            char[] sorted = new char[Length];
            for (i = 0; i < Length; i++)
                sorted[i] = pass[pos[i]];

            string Pass = new String(sorted);

            return Pass;
        }

        private static void getVercode(int time, IWebDriver driver1, string phone, out string code,string threadPerTotalthread)
        {
            code = "";
            float temp;
            IWebElement body;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //lap den khi lay duoc vercode voi phone tuong ung
            do
            {
                System.Threading.Thread.Sleep(time);
                body = driver1.FindElement(By.TagName("body"));
                WriteLineEmptyFile(body.Text, "temp"+ threadPerTotalthread.Split(':')[0]);
                var bodytext = File.ReadLines("temp"+ threadPerTotalthread.Split(':')[0]);
                foreach (var bodyline in bodytext)
                {
                    //neu hang tren web dung voi so phone do, va da lay duoc so (het status loading)
                    if (bodyline.Contains(phone) && float.TryParse(bodyline.Split(' ')[5], out temp))
                    {
                        code = bodyline.Split(' ')[5];
                    }
                }

                //neu ko lay duoc code, loading mai sau 4p
                    if (stopwatch.Elapsed > TimeSpan.FromMinutes(4))
                {
                    code = "none";
                    return ;
                }
                    
                    
                
            } while (code == "");

        }

        private static void getNewphonenumber(int time, IWebDriver driver1, out string phone,out string idphone,string threadPerTotalthread)
        {
            IWebElement body;
            driver1.Navigate().GoToUrl("http://sms-activate.ru/index.php?act=getNumber");

            //click nut get phone o muc google, youtube
            driver1.FindElement(By.XPath("/html/body/div[4]/div/div[4]/div[2]/div[1]/form/table/tbody/tr[7]/td[2]/label/span[1]")).Click();
            System.Threading.Thread.Sleep(time);
            driver1.FindElement(By.CssSelector("tr.tabbed:nth-child(7) > td:nth-child(2) > label:nth-child(1) > a:nth-child(5)")).Click(); ;
            float trygetVercode;

            //lap den khi xuat hien phone moi
            do
            {
                System.Threading.Thread.Sleep(time);
                body = driver1.FindElement(By.TagName("body"));
                WriteLineEmptyFile(body.Text, "temp"+ threadPerTotalthread.Split(':')[0]);
                phone = ReadFileAtLine(File.ReadLines("temp"+ threadPerTotalthread.Split(':')[0]).Count()- int.Parse(threadPerTotalthread.Split(':')[0]), "temp"+ threadPerTotalthread.Split(':')[0]).Split(' ')[1];
                idphone = ReadFileAtLine(File.ReadLines("temp" + threadPerTotalthread.Split(':')[0]).Count() - int.Parse(threadPerTotalthread.Split(':')[0]), "temp" + threadPerTotalthread.Split(':')[0]).Split(' ')[0];

                //lap neu neu N hang cuoi cung van chua la chua co sdt moi (phai lay duoc N so loading thi moi chay multithread duoc, ko se bi chong cheo sdt)
                //cac so loading o cuoi cung, do do kiem tra so co index la lastline-(N-1) la duoc, cac so tiep theo auto la so loading
            } while (float.TryParse(ReadFileAtLine(File.ReadLines("temp"+ threadPerTotalthread.Split(':')[0]).Count()- int.Parse(threadPerTotalthread.Split(':')[1])+1, "temp"+ threadPerTotalthread.Split(':')[0]).Split(' ')[5], out trygetVercode)&& float.TryParse(phone,out trygetVercode));
        }

        private static string ReadFileAtLine(int p, string file)
        {
            return File.ReadLines(file).Skip(p - 1).First();

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

        private static void addMailAfterConfirmation(IWebDriver driver, string code)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            System.Threading.Thread.Sleep(5000);
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

        private static string verifyConfirmation(string threadPerTotalthread)
        {
            string code = "";
            while (code == "")
            {
                MyThread10.getMailGoogle(int.Parse(threadPerTotalthread.Split(':')[0]), int.Parse(threadPerTotalthread.Split(':')[1]), 2);
                //load link confirm len ff
                var alinks = File.ReadAllLines("socks.txt");
                try
                {
                    alinks = File.ReadAllLines("mailConfirm"+ threadPerTotalthread.Split(':')[0]+".txt");
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Second exception caught.", e);
                }
                foreach (string link in alinks)
                    if (link.Contains("\t"))
                    {
                        if (File.ReadLines("pvas" + threadPerTotalthread.Split(':')[0]).First().Contains(link.Split('\t')[0]))
                        {
                            code = link.Split('\t')[1];
                            break;
                        } 
                    }
            }
            return code;
        }

        private static void sendConfirmation(IWebDriver driver)
        {
            System.Threading.Thread.Sleep(5000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            driver.Navigate().GoToUrl("https://mail.google.com/mail/?ui=html&zy=h");
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName("maia-button")));
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

        public static void getMailOutlook(int stt, int num, int reset)
        {
            // Create a folder named "inbox" under current directory
            // to save the email retrieved.
            string curpath = Directory.GetCurrentDirectory();
            string mailbox = String.Format("{0}\\inbox" + stt, curpath);

            File.Delete("mailConfirm" + stt + ".txt");
            System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(curpath + "\\inbox" + stt + "\\");

            try
            {
                foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                {
                    file.Delete();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Second exception caught.", e);
            }


            // If the folder is not existed, create it.
            if (!Directory.Exists(mailbox))
            {
                Directory.CreateDirectory(mailbox);
            }

            MailServer oServer = new MailServer("pop-mail.outlook.com",
                      "tamtho123@outlook.com", "idew!jjIHH1", ServerProtocol.Pop3);
            MailClient oClient = new MailClient("TryIt");

            // If your POP3 server requires SSL connection,
            // Please add the following codes:
            oServer.SSLConnection = true;
            oServer.Port = 995;

            try
            {
                oClient.Connect(oServer);
                MailInfo[] infos = oClient.GetMailInfos();
                for (int i = infos.Length - 1; i > infos.Length - (num + 1) * 2; i--)
                {
                    MailInfo info = infos[i];
                    //Console.WriteLine("Index: {0}; Size: {1}; UIDL: {2}",
                    //    info.Index, info.Size, info.UIDL);

                    // Receive email from POP3 server
                    Mail oMail = oClient.GetMail(info);

                    //Console.WriteLine("From: {0}", oMail.From.ToString());
                    //Console.WriteLine("Subject: {0}\r\n", oMail.Subject);

                    // Generate an email file name based on date time.
                    System.DateTime d = System.DateTime.Now;
                    System.Globalization.CultureInfo cur = new
                        System.Globalization.CultureInfo("en-US");
                    string sdate = d.ToString("yyyyMMddHHmmss", cur);
                    string fileName = String.Format("{0}\\{1}{2}{3}.eml",
                        mailbox, sdate, d.Millisecond.ToString("d3"), i);

                    // Save email to local disk
                    oMail.SaveAs(fileName, true);

                    // Mark email as deleted from POP3 server.
                    //oClient.Delete(info);
                }

                // Quit and pure emails marked as deleted from POP3 server.
                oClient.Quit();
            }
            catch (Exception ep)
            {
                Console.WriteLine(ep.Message);
            }

            ParseEmailMain(stt, reset);

        }

        public static void getMailGoogle(int stt, int num, int reset)
        {
            // Create a folder named "inbox" under current directory
            // to save the email retrieved.
            string curpath = Directory.GetCurrentDirectory();
            string mailbox = String.Format("{0}\\inbox" + stt, curpath);

            File.Delete("mailConfirm" + stt + ".txt");
            System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(curpath + "\\inbox" + stt + "\\");

            try
            {
                foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                {
                    file.Delete();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Second exception caught.", e);
            }


            // If the folder is not existed, create it.
            if (!Directory.Exists(mailbox))
            {
                Directory.CreateDirectory(mailbox);
            }

            // Gmail IMAP4 server is "imap.gmail.com"
            MailServer oServer = new MailServer("imap.gmail.com",
                        "getcryptotab.com@gmail.com", "B1nbin!@#", ServerProtocol.Imap4);
            MailClient oClient = new MailClient("TryIt");

            // Set SSL connection,
            oServer.SSLConnection = true;

            // Set 993 IMAP4 port
            oServer.Port = 993;

            try
            {
                oClient.Connect(oServer);
                MailInfo[] infos = oClient.GetMailInfos();
                for (int i = infos.Length - 1; i > infos.Length - (num + 1) * 2; i--)
                {
                    MailInfo info = infos[i];
                    //Console.WriteLine("Index: {0}; Size: {1}; UIDL: {2}",
                    //    info.Index, info.Size, info.UIDL);

                    // Download email from GMail IMAP4 server
                    Mail oMail = oClient.GetMail(info);

                    //Console.WriteLine("From: {0}", oMail.From.ToString());
                    //Console.WriteLine("Subject: {0}\r\n", oMail.Subject);

                    // Generate an email file name based on date time.
                    System.DateTime d = System.DateTime.Now;
                    System.Globalization.CultureInfo cur = new
                        System.Globalization.CultureInfo("en-US");
                    string sdate = d.ToString("yyyyMMddHHmmss", cur);
                    string fileName = String.Format("{0}\\{1}{2}{3}.eml",
                        mailbox, sdate, d.Millisecond.ToString("d3"), i);

                    // Save email to local disk
                    oMail.SaveAs(fileName, true);

                    // Mark email as deleted in GMail account.
                    //oClient.Delete(info);
                }

                // Quit and pure emails marked as deleted from Gmail IMAP4 server.
                oClient.Quit();
            }
            catch (Exception ep)
            {
                Console.WriteLine(ep.Message);
            }

            ParseEmailMain(stt, reset);

        }

        public static void ParseEmail(string emlFile, int stt, int reset)
        {
            Mail oMail = new Mail("TryIt");
            oMail.Load(emlFile, false);

            // Parse Mail From, Sender
            //Console.WriteLine("From: {0}", oMail.From.ToString());

            // Parse Mail To, Recipient
            MailAddress[] addrs = oMail.To;
            //for (int i = 0; i < addrs.Length; i++)
            //{
            //    Console.WriteLine("To: {0}", addrs[i].ToString());
            //}

            // Parse Mail CC
            addrs = oMail.Cc;
            //for (int i = 0; i < addrs.Length; i++)
            //{
            //    Console.WriteLine("To: {0}", addrs[i].ToString());
            //}

            //// Parse Mail Subject
            //Console.WriteLine("Subject: {0}", oMail.Subject);

            //// Parse Mail Text/Plain body
            //Console.WriteLine("TextBody: {0}", oMail.TextBody);

            //// Parse Mail Html Body
            //Console.WriteLine("HtmlBody: {0}", oMail.HtmlBody);

            // Parse Attachments
            Attachment[] atts = oMail.Attachments;
            //for (int i = 0; i < atts.Length; i++)
            //{
            //    Console.WriteLine("Attachment: {0}", atts[i].Name);
            //}
            addrs = oMail.To;
            if (reset == 0)
            {
                if (oMail.Subject.Contains("POST/EDIT/DELETE"))
                {
                    try
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter("mailConfirm" + stt + ".txt", true))
                        {


                            for (int i = 0; i < addrs.Length; i++)
                            {

                                string[] text = oMail.TextBody.Split('/');
                                string link = "https://post.craigslist.org/u/" + text[4] + "/" + text[5].Split('\r')[0];
                                file.WriteLine(addrs[i].ToString().Split('<')[1].Split('>')[0] + "\t" + link);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0} Second exception caught.", e);
                    }
                }
            }
            else if (reset == 2)
            {
                if (oMail.Subject.Contains("Gmail Forwarding Confirmation"))
                {
                    try
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter("mailConfirm" + stt + ".txt", true))
                        {
                            for (int i = 0; i < addrs.Length; i++)
                            {
                                file.WriteLine(oMail.TextBody.Split('@')[0] + "\t" + oMail.TextBody.Split(':')[1].Substring(1, 9));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0} Second exception caught.", e);
                    }
                }
            }
            else
            {
                if (oMail.Subject.Contains("Request to Reset Account Password for"))
                {
                    if (oMail.TextBody.Contains("lang=en&cc=us"))
                    {
                        try
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter("mailConfirm" + stt + ".txt", true))
                            {


                                for (int i = 0; i < addrs.Length; i++)
                                {

                                    string[] text = oMail.TextBody.Split('=');
                                    string link = "https://accounts.craigslist.org/pass?lang=en&cc=us&ui=" + text[3].Split('&')[0] + "&ip=" + text[4].Split('\r')[0];
                                    file.WriteLine(addrs[i].ToString().Split('<')[1].Split('>')[0] + "\t" + link);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("{0} Second exception caught.", e);
                        }
                    }
                    else if (!oMail.TextBody.Contains("lang=en&cc=us"))
                    {
                        try
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter("mailConfirm" + stt + ".txt", true))
                            {


                                for (int i = 0; i < addrs.Length; i++)
                                {

                                    string[] text = oMail.TextBody.Split('=');
                                    string link = "https://accounts.craigslist.org/pass?ui=" + text[1].Split('&')[0] + "&ip=" + text[2].Split('\r')[0];
                                    file.WriteLine(addrs[i].ToString().Split('<')[1].Split('>')[0] + "\t" + link);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("{0} Second exception caught.", e);
                        }
                    }
                }
                else if (oMail.Subject.Contains("New Craigslist Account"))
                {
                    try
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter("mailConfirm" + stt + ".txt", true))
                        {


                            for (int i = 0; i < addrs.Length; i++)
                            {

                                string[] text = oMail.TextBody.Replace("lang=en&cc=us&", "").Split('=');
                                string link = "https://accounts.craigslist.org/pass?ui=" + text[1].Split('&')[0] + "&ip=" + text[2].Split('&')[0] + "&rt=P&rp=" + text[4].Split('\r')[0];
                                file.WriteLine(addrs[i].ToString().Split('<')[1].Split('>')[0] + "\t" + link);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0} Second exception caught.", e);
                    }
                }
            }

        }

        public static void ParseEmailMain(int stt, int reset)
        {
            string curpath = Directory.GetCurrentDirectory();
            string mailbox = String.Format("{0}\\inbox" + stt, curpath);

            // If the folder is not existed, create it.
            if (!Directory.Exists(mailbox))
            {
                Directory.CreateDirectory(mailbox);
            }

            // Get all *.eml files in specified folder and parse it one by one.
            string[] files = Directory.GetFiles(mailbox, "*.eml");
            for (int i = 0; i < files.Length; i++)
            {
                ParseEmail(files[i], stt, reset);
            }
        }
    }
}