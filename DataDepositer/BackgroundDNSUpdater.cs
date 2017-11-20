/**
 *  RuDiCon Soft (c) 2017
 * 
 *  Class for Dynamic DNS update/check.
 *  
 *  @created 2017-09-15 Artem Nikolaev 
 * 
 */
 using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace DataDepositer
{
    class BackgroundDNSUpdater
    {
        private bool _continue;
        private Thread worker;

        public string ServiceUrl;
        public string Domain;
        public string Username;
        public string Password;
        public int UpdateInterval;

        public string LastErrorMessage;
        public EventHandler ErrorCallback;
        public EventHandler SuccessCallback;

        public BackgroundDNSUpdater()
        {
            UpdateInterval = 60;
            worker = new Thread(DoWork);
        }

        public void Start()
        {
            _continue = true;
            worker.Start();
        }

        public void Stop()
        {
            _continue = false;
            worker.Abort();
        }

        public bool IsRunning()
        {
            return worker.IsAlive;
        }

        public void DoWork()
        {
            Debug.WriteLine("Thread starting");
            while (_continue)
            {
                try
                {
                    Debug.WriteLine("Updating " + Domain + " with public IP " + getPublicIPAddress());

                    updateIP();
                    SuccessCallback(this, null);

                    Debug.WriteLine("OK");

                    Thread.Sleep(UpdateInterval * 1000);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error : " + ex.Message);
                    if (ErrorCallback != null)
                    {
                        Debug.WriteLine(ex.Message);
                        LastErrorMessage = ex.Message;
                        ErrorCallback(this, null);
                    }
                    Thread.Sleep(2000);
                }
            }
            Debug.WriteLine("Thread stopped");
        }

        private string getPublicIPAddress()
        {
            string addr = httpGet("http://ipv4.icanhazip.com");  // TODO
            return addr.Replace("\n", "");
        }

        private void updateIP()
        {
            string ipAddr = getPublicIPAddress();
            string url = "http://" + ServiceUrl + "/nic/update?system=dyndns&hostname=" + Domain + "&myip=" + getPublicIPAddress();
            httpGet(url, new NetworkCredential(Username, Password));
        }

        private string httpGet(string url, NetworkCredential credentials = null)
        {
            WebClient request = new WebClient();
            if (credentials != null)
            {
                request.Credentials = credentials;
            }
            return request.DownloadString(url);
        }
    }
}