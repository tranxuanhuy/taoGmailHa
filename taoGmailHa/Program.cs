using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace taoGmailHa
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int j = 0; j < 6; j++)
            {
                MyThread10[] thr = new MyThread10[100];
                Thread[] tid = new Thread[100];
                int numberThread = 3;
                for (int i = 0; i < numberThread; i++)
                {
                    thr[i] = new MyThread10();
                    tid[i] = new Thread(new ThreadStart(thr[i].Thread1));
                    tid[i].Name = i + ":" + numberThread;
                    tid[i].Start();
                }

                for (int i = 0; i < numberThread; i++)
                {
                    tid[i].Join();
                } 
            }
        }
            

        }

        

       
    }

