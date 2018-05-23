using EAGetMail;
using System;
using System.IO;
using System.Linq;
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