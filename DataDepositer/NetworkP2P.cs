using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.PeerToPeer;
using System.ServiceModel;
using System.Configuration;
using System.Windows.Forms;
using System.ServiceModel.Description;
using System.Diagnostics;
using System.Threading;

namespace DataDepositer
{
    public class NetworkP2P
    {
        public string NetworkName;
        public string Comment;
        public string Username;
        public int Port { get; private set; } = 13027;

        private Config Config;
        private PeerName peerName;
        private PeerNameRegistration pnReg;
        private PeerNameResolver resolver;
        private P2PService localService;
        private ServiceHost host;
        internal object mainform = null;
//        private string endpointuriformat = "net.tcp://{0}:{1}/P2PService/DataDepositer";
        private string endpointuriformat = "net.tcp://[{0}]:{1}/P2PService/DataDepositer";
        private string endpointuriformat4 = "net.tcp://{0}:{1}/P2PService/DataDepositer";
        private string endpointuriformat6 = "net.tcp://[{0}]:{1}/P2PService/DataDepositer";
        private Helper h = new Helper();
        private string guid = "2F4611C8-B0DA-454C-92BE-C821838B7684";
        // {2F4611C8-B0DA-454C-92BE-C821838B7684}
        //static const GUID <<имя>> = { 0x2f4611c8, 0xb0da, 0x454c, { 0x92, 0xbe, 0xc8, 0x21, 0x83, 0x8b, 0x76, 0x84 } };


        public NetworkP2P(string networkName, Config config)
        {
            NetworkName = networkName;
            Config = config;
        }

        public NetworkP2P(string networkName, int port)
        {
            NetworkName = networkName;
            Port = port;
        }

        public void SetTeredo()
        {
            string arguments = "int ipv6 set teredo client teredo.remlab.net";
            //string arguments = "interface ip set address \"Local Area Connection\" static 192.168.106.181 255.255.255.0";
            ProcessStartInfo procStartInfo = new ProcessStartInfo("netsh", arguments);

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;

            Process.Start(procStartInfo);
            Thread.Sleep(3000); // 3 sec
        }

        internal void Init()
        {
            // @need refactor
            string port = ConfigurationManager.AppSettings["port"];
            Port = Convert.ToInt32(port);
            string username = ConfigurationManager.AppSettings["username"];
            Username = username;

            string machineName = Environment.MachineName;
            string serviceUrl = null;

            //  Getting URL-adress of service with IPv4 
            //  and port from config.
            foreach (IPAddress address in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                //if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 && !address.IsIPv6LinkLocal)
                    {
                        serviceUrl = string.Format(endpointuriformat6, address, port);
                    break;
                }
            }

            // Check for null
            if (serviceUrl == null)
            {
                Logger.Log.Info("Not defined Endpoint address for WCF.", "Networking Error");
                MessageBox.Show("Not defined Endpoint address for WCF.", "Networking Error", MessageBoxButtons.OK);
            }

            // Регистрация и запуск службы WCF
            localService = new P2PService(mainform, username); // @TODO Need refactor

            host = new ServiceHost(localService, new Uri(serviceUrl));
            NetTcpBinding binding = Helper.GetStreamBinding();

            //NetTcpBinding binding = new NetTcpBinding();
            //binding.Security.Mode = SecurityMode.None;

            ///////
            ///////
            //// Stream add
            //////
            //////
            //binding.TransferMode = TransferMode.Streamed;
            //binding.MaxReceivedMessageSize = 20134217728; // 20 GB
            //binding.MaxBufferPoolSize = 1024 * 1024; // 1 MB
            //binding.ReceiveTimeout = new TimeSpan(1, 0, 0);
            //binding.SendTimeout = new TimeSpan(1, 0, 0);
            //////
            //////
            //////
            //////

            host.AddServiceEndpoint(typeof(IP2PService), binding, serviceUrl);
            try
            {
                host.Open();
            }
            catch (AddressAlreadyInUseException)
            {
                Logger.Log.Info("Can`t start listening, port is busy.", "WCF Error");
                MessageBox.Show("Can`t start listening, port is busy.", "WCF Error", MessageBoxButtons.OK);
                Application.Exit();
            }

            // Creates a secure (not spoofable) PeerName
            peerName = new PeerName(Application.ProductName, PeerNameType.Secured);
            //peerName = new PeerName(Application.ProductName + guid, PeerNameType.Unsecured);
            //peerName = new PeerName(Application.ProductName, PeerNameType.Unsecured);
            //peerName = new PeerName( Application.ProductName, PeerNameType.Secured);
            //peerName = new PeerName(Application.ProductName + "1234567890", PeerNameType.Secured);
            pnReg = new PeerNameRegistration();
            pnReg.PeerName = peerName;
            pnReg.Cloud = Cloud.Available;

            //pnReg.Port = (int) Config.Port;
            pnReg.Port = Port;

            // OPTIONAL

            // The properties set below are optional.  
            // You can register a PeerName without setting these properties
            // but this properties can help identify PC
            //pnReg.Comment = "up to 39 unicode char comment";
            pnReg.Comment = username.Substring(0, Math.Min(39, username.Length));
            pnReg.Data = Encoding.UTF8.GetBytes("A data blob associated with the name"); // @TODO Add binary data for Peer2Peer Netwwork

            // OPTIONAL
            // The properties below are also optional, but will not be set (ie. are commented out) for this example

            //pnReg.IPEndPointCollection = // a list of all {IPv4/v6 address, port} pairs to associate with the peername
            //pnReg.Cloud = //the scope in which the name should be registered (local subnet, internet, etc)
            pnReg.Cloud = Cloud.Global; // resolve in Global Network - 
            //pnReg.Cloud = Cloud.Available; // resolve in Global Network - 
        }

        internal void Init2()
        {
            // @need refactor
            string port = ConfigurationManager.AppSettings["port"];
            Port = Convert.ToInt32(port);
            string username = ConfigurationManager.AppSettings["username"];
            Username = username;

            string machineName = Environment.MachineName;
            string serviceUrl = null;
            List<Uri> Uris = new List<Uri>();

            // Регистрация и запуск службы WCF
            localService = new P2PService(mainform, username); // @TODO Need refactor
            host = new ServiceHost(localService);

            //  Getting URL-adress of service with IPv4 
            //  and port from config.
            foreach (IPAddress address in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    serviceUrl = string.Format(endpointuriformat4, address, port);
                }
                else 
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    serviceUrl = string.Format(endpointuriformat6, address, port);
                }

                // Check for null
                if (serviceUrl == null)
                {
                    Logger.Log.Info("Not defined Endpoint address for WCF.", "Networking Error");
                    MessageBox.Show("Not defined Endpoint address for WCF.", "Networking Error", MessageBoxButtons.OK);
                }


                //host = new ServiceHost(localService, new Uri(serviceUrl));
                //Uris.Add(new Uri(serviceUrl));
                NetTcpBinding binding = Helper.GetStreamBinding();

                //NetTcpBinding binding = new NetTcpBinding();
                //binding.Security.Mode = SecurityMode.None;

                ///////
                ///////
                //// Stream add
                //////
                //////
                //binding.TransferMode = TransferMode.Streamed;
                //binding.MaxReceivedMessageSize = 20134217728; // 20 GB
                //binding.MaxBufferPoolSize = 1024 * 1024; // 1 MB
                //binding.ReceiveTimeout = new TimeSpan(1, 0, 0);
                //binding.SendTimeout = new TimeSpan(1, 0, 0);
                //////
                //////
                //////
                //////

                host.AddServiceEndpoint(typeof(IP2PService), binding, serviceUrl);
                //host.AddServiceEndpoint(new ServiceEndpoint(binding, new EndpointAddress(serviceUrl)));
            }

            host = new ServiceHost(typeof(IP2PService), Uris.ToArray());
            //host.AddServiceEndpoint(typeof(IP2PService), Helper.GetStreamBinding(), Uris.ToArray());

            try
            {
                host.Open();
            }
            catch (AddressAlreadyInUseException)
            {
                Logger.Log.Info("Can`t start listening, port is busy.", "WCF Error");
                MessageBox.Show("Can`t start listening, port is busy.", "WCF Error", MessageBoxButtons.OK);
                Application.Exit();
            }

            // Creates a secure (not spoofable) PeerName
            peerName = new PeerName(Application.ProductName, PeerNameType.Secured);
            pnReg = new PeerNameRegistration();
            pnReg.PeerName = peerName;

            pnReg.Port = (int)Config.Port;

            // OPTIONAL

            // The properties set below are optional.  
            // You can register a PeerName without setting these properties
            // but this properties can help identify PC
            //pnReg.Comment = "up to 39 unicode char comment";
            pnReg.Comment = username.Substring(0, Math.Min(39, username.Length));
            pnReg.Data = Encoding.UTF8.GetBytes("A data blob associated with the name"); // @TODO Add binary data for Peer2Peer Netwwork

            // OPTIONAL
            // The properties below are also optional, but will not be set (ie. are commented out) for this example

            //pnReg.IPEndPointCollection = // a list of all {IPv4/v6 address, port} pairs to associate with the peername
            //pnReg.Cloud = //the scope in which the name should be registered (local subnet, internet, etc)
            //pnReg.Cloud = Cloud.Global; // resolve in Global Network - 
            pnReg.Cloud = Cloud.Available; // resolve in Global Network - 
        }

        internal void Stop()
        {
            //Stop the publish for others to resolve
            Logger.Log.Info("Stop P2P.");
            pnReg.Stop();
        }

        internal void Start()
        {
            //Starting the registration means the name is published for others to resolve
            Logger.Log.Info("Start P2P.");
            pnReg.Start();
        }

        public void ResolveAsync()
        {
            // Create resolver.
            resolver = new PeerNameResolver();
            resolver.ResolveProgressChanged +=
                    new EventHandler<ResolveProgressChangedEventArgs>(resolver_ResolveProgressChanged);
            resolver.ResolveCompleted +=
                new EventHandler<ResolveCompletedEventArgs>(resolver_ResolveCompleted);

            // Prepare for new peer adding
            Vault.Peers.Clear();

            // Solve peer names async
            //resolver.ResolveAsync(new PeerName("0.P2P Sample"), 1);
            //PeerName peername = new PeerName(Application.ProductName, PeerNameType.Unsecured);
            resolver.ResolveAsync(peerName, Cloud.Available);


        }

        public void resolver_ResolveCompleted(object sender, ResolveCompletedEventArgs e)
        {
            // error if cloud is empty
            //if (Vault.Peers.Count == 0)
            //{
            //    Vault.Peers.Add(
            //       new PeerEntry
            //       {
            //           DisplayString = "Peers not found",
            //       });
            //}
            //RefreshButton.IsEnabled = true;
        }

        public void resolver_ResolveProgressChanged(object sender, ResolveProgressChangedEventArgs e)
        {
            PeerNameRecord peer = e.PeerNameRecord;

            foreach (IPEndPoint ep in peer.EndPointCollection)
            {
                //if (ep.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    if (ep.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        try
                    {
                        string endpointUrl = string.Format(endpointuriformat, ep.Address, ep.Port);
                        NetTcpBinding binding = new NetTcpBinding();
                        //binding.Security.Mode = SecurityMode.None;
                        binding.Security.Mode = SecurityMode.None;

                        /////
                        /////
                        // Stream add
                        ////
                        ////
                        binding.TransferMode = TransferMode.Streamed;
                        binding.MaxReceivedMessageSize = 20134217728; // 20 GB
                        binding.MaxBufferPoolSize = 1024 * 1024; // 1 MB
                        ////
                        ////
                        ////
                        ////

                        IP2PService serviceProxy = ChannelFactory<IP2PService>.CreateChannel(
                            binding, new EndpointAddress(endpointUrl));

                        PeerEntry entry = new PeerEntry
                        {
                            PeerName = peer.PeerName,
                            ServiceProxy = serviceProxy,
                            DisplayString = serviceProxy.GetName(),
                        };

                        //Vault.Peers.Add(peer);
                        Logger.Log.Info(endpointUrl);
                        Logger.Log.Info(entry.PeerName);
                        Logger.Log.Info(entry.Comment);
                        Logger.Log.Info("\t Endpoint:{0}", ep);

                        Vault.Peers.Add(entry);
                    }
                    catch (EndpointNotFoundException)
                    {
                        //Vault.Peers.Add(
                        //   new PeerEntry
                        //   {
                        //       PeerName = peer.PeerName,
                        //       DisplayString = "Unknown Peer",
                        //   });
                    }
                }
            }
        }


        internal void Resolve()
        {
            Logger.Log.Info("Start P2P resolver...");
            // create a resolver object to resolve a peername
            resolver = new PeerNameResolver();

            // resolve the PeerName - this is a network operation and will block until the resolve completes
            PeerNameRecordCollection results = resolver.Resolve(peerName);

            foreach (PeerNameRecord record in results)
            {
                PeerEntry peer = new PeerEntry();
                if (record.Comment != null)
                {
                    peer.Comment = record.Comment;
                }

                if (record.Data != null)
                {
                    peer.Data = System.Text.Encoding.ASCII.GetString(record.Data);
                }

                //peer.Endpoints = record.EndPointCollection;
                //foreach (IPEndPoint endpoint in record.EndPointCollection)
                //{
                //    Console.WriteLine("\t Endpoint:{0}", endpoint);
                //    Console.WriteLine();
                //}
                Vault.Peers.Add(peer);

                Logger.Log.Info("Peers Resolver");
                Logger.Log.Info(peer.PeerName);
                Logger.Log.Info(peer.Comment);
                foreach (IPEndPoint endpoint in record.EndPointCollection)
                {
                    Logger.Log.Info("\t Endpoint:{0}", endpoint);
                }
                Logger.Log.Info("End P2P resolver...");
            }
        }

        internal void Resolve2_old()
        {
            Logger.Log.Info("Start P2P resolver...");
            // create a resolver object to resolve a peername
            resolver = new PeerNameResolver();

            // resolve the PeerName - this is a network operation and will block until the resolve completes
            PeerNameRecordCollection results = resolver.Resolve(peerName, 100); // Max 100 records

            foreach (PeerNameRecord record in results)
            {
                foreach (IPEndPoint ep in record.EndPointCollection)
                {
                    //if (ep.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    if (ep.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 && !ep.Address.IsIPv6LinkLocal)
                    {
                        try
                        {
                            string endpointUrl = string.Format(endpointuriformat, ep.Address, ep.Port);
                            NetTcpBinding binding = Helper.GetStreamBinding();

                            //NetTcpBinding binding = new NetTcpBinding();
                            ////binding.Security.Mode = SecurityMode.None;
                            //binding.Security.Mode = SecurityMode.None;
                            ////EndpointAddress epa = new EndpointAddress()
                            ///////
                            ///////
                            //// Stream add
                            //////
                            //////
                            //binding.TransferMode = TransferMode.Streamed;
                            //binding.MaxReceivedMessageSize = 20134217728; // 20 GB
                            //binding.MaxBufferPoolSize = 1024 * 1024; // 1 MB
                            //////
                            //////
                            //////
                            //////
                            IP2PService serviceProxy = ChannelFactory<IP2PService>.CreateChannel(
                                binding, new EndpointAddress(endpointUrl));
                            //IP2PService serviceProxy = ChannelFactory<IP2PService>.CreateChannel(
                            //    binding, new EndpointAddress(ep.Address.ToString() + ":" + Port.ToString()));

                            //PeerEntry entry = new PeerEntry
                            //{
                            //    PeerName = record.PeerName,
                            //    ServiceProxy = serviceProxy,
                            //    DisplayString = serviceProxy.GetName(),
                            //};

                            PeerEntry peer = new PeerEntry();
                            peer.PeerName = record.PeerName;
                            peer.ServiceProxy = serviceProxy;
                            peer.DisplayString = serviceProxy.GetName();
                            if (record.Comment != null)
                            {
                                peer.Comment = record.Comment;
                            }

                            if (record.Data != null)
                            {
                                peer.Data = System.Text.Encoding.ASCII.GetString(record.Data);
                            }


                            //Vault.Peers.Add(peer);
                            Logger.Log.Info(endpointUrl);
                            Logger.Log.Info(peer.PeerName);
                            Logger.Log.Info(peer.Comment);
                            Logger.Log.Info("\t Endpoint:{0}", ep);

                            Vault.Peers.Add(peer);
                        }
                        catch (EndpointNotFoundException e)
                        {
                            //Vault.Peers.Add(
                            //   new PeerEntry
                            //   {
                            //       PeerName = peer.PeerName,
                            //       DisplayString = "Unknown Peer",
                            //   });
                            Logger.Log.Info(e.Message);
                        }
                    }
                }
            }
        }

        internal void Resolve2()
        {
            Logger.Log.Info("Start P2P resolver...");
            // create a resolver object to resolve a peername
            resolver = new PeerNameResolver();
            string endpointUrl = null;

            // resolve the PeerName - this is a network operation and will block until the resolve completes
            //PeerNameRecordCollection results = resolver.Resolve(peerName, 100); // Max 100 records
            PeerNameRecordCollection results = resolver.Resolve(peerName, Cloud.Available, 100); // Max 100 records

            foreach (PeerNameRecord record in results)
            {
                foreach (IPEndPoint ep in record.EndPointCollection)
                {
                    if (ep.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        endpointUrl = string.Format(endpointuriformat4, ep.Address, ep.Port);
                    }
                    else
                    if (ep.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        //if ((ep.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6) && !ep.Address.IsIPv6LinkLocal)
                    {
                        endpointUrl = string.Format(endpointuriformat6, ep.Address, ep.Port);
                    }

                    try
                    {
                        NetTcpBinding binding = Helper.GetStreamBinding();
                        IP2PService serviceProxy = ChannelFactory<IP2PService>.CreateChannel(
                            binding, new EndpointAddress(endpointUrl));

                        PeerEntry peer = new PeerEntry();
                        peer.PeerName = record.PeerName;
                        peer.ServiceProxy = serviceProxy;
                        peer.DisplayString = serviceProxy.GetName();
                        if (record.Comment != null)
                        {
                            peer.Comment = record.Comment;
                        }

                        if (record.Data != null)
                        {
                            peer.Data = System.Text.Encoding.ASCII.GetString(record.Data);
                        }
                        Logger.Log.Info(string.Format("SUCCESS Connect:{0} \t\t {1} ", endpointUrl, peer.Comment));

                        Vault.Peers.Add(peer);
                    }
                    catch (EndpointNotFoundException e)
                    {
                        Logger.Log.Debug(e.Message);
                    }

                }
            }
        }

    }
}


//public class NetworkP2P
//{
//    private P2PService localService;
//    private string serviceUrl;
//    private ServiceHost host;
//    private PeerName peerName;
//    private PeerNameRegistration peerNameRegistration;
//    private ListBox PeerList;
//    private Button RefreshButton;


//    private void Window_Loaded(object sender, EventArgs e)
//    {
//        // Получение конфигурационной информации из app.config
//        string port = ConfigurationManager.AppSettings["port"];
//        string username = ConfigurationManager.AppSettings["username"];
//        string machineName = Environment.MachineName;
//        string serviceUrl = null;

//        // Установка заголовка окна
//        //this.Title = string.Format("P2P приложение - {0}", username);

//        //  Получение URL-адреса службы с использованием адресаIPv4 
//        //  и порта из конфигурационного файла
//        foreach (IPAddress address in Dns.GetHostAddresses(Dns.GetHostName()))
//        {
//            if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
//            {
//                serviceUrl = string.Format("net.tcp://{0}:{1}/P2PService", address, port);
//                break;
//            }
//        }

//        // Выполнение проверки, не является ли адрес null
//        if (serviceUrl == null)
//        {
//            // Отображение ошибки и завершение работы приложения
//            MessageBox.Show("Не удается определить адрес конечной точки WCF.", "Networking Error",
//                MessageBoxButtons.OK);
//            //Application.Current.Shutdown();
//        }

//        // Регистрация и запуск службы WCF
//        localService = new P2PService(this, username);
//        host = new ServiceHost(localService, new Uri(serviceUrl));
//        NetTcpBinding binding = new NetTcpBinding();
//        binding.Security.Mode = SecurityMode.None;
//        host.AddServiceEndpoint(typeof(IP2PService), binding, serviceUrl);
//        try
//        {
//            host.Open();
//        }
//        catch (AddressAlreadyInUseException)
//        {
//            // Отображение ошибки и завершение работы приложения
//            MessageBox.Show("Не удается начать прослушивание, порт занят.", "WCF Error",
//               MessageBoxButtons.OK);
//            //Application.Current.Shutdown();
//        }

//        // Создание имени равноправного участника (пира)
//        peerName = new PeerName("P2P Sample", PeerNameType.Unsecured);

//        // Подготовка процесса регистрации имени равноправного участника в локальном облаке
//        peerNameRegistration = new PeerNameRegistration(peerName, int.Parse(port));
//        peerNameRegistration.Cloud = Cloud.AllLinkLocal;

//        // Запуск процесса регистрации
//        peerNameRegistration.Start();
//    }

//    internal void Init()
//    {
//        throw new NotImplementedException();
//    }

//    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
//    {
//        // Остановка регистрации
//        peerNameRegistration.Stop();

//        // Остановка WCF-сервиса
//        host.Close();
//    }

//    private void RefreshButton_Click(object sender, EventArgs e)
//    {
//        // Создание распознавателя и добавление обработчиков событий
//        PeerNameResolver resolver = new PeerNameResolver();
//        resolver.ResolveProgressChanged +=
//            new EventHandler<ResolveProgressChangedEventArgs>(resolver_ResolveProgressChanged);
//        resolver.ResolveCompleted +=
//            new EventHandler<ResolveCompletedEventArgs>(resolver_ResolveCompleted);

//        // Подготовка к добавлению новых пиров
//        PeerList.Items.Clear();
//        //RefreshButton.IsEnabled = false;
//        RefreshButton.Enabled = false;

//        // Преобразование незащищенных имен пиров асинхронным образом
//        resolver.ResolveAsync(new PeerName("0.P2P Sample"), 1);
//    }

//    void resolver_ResolveCompleted(object sender, ResolveCompletedEventArgs e)
//    {
//        // Сообщение об ошибке, если в облаке не найдены пиры
//        if (PeerList.Items.Count == 0)
//        {
//            PeerList.Items.Add(
//               new PeerEntry
//               {
//                   DisplayString = "Пиры не найдены.",
//                   ButtonsEnabled = false
//               });
//        }
//        // Повторно включаем кнопку "обновить"
//        RefreshButton.Enabled = true;
//    }

//    void resolver_ResolveProgressChanged(object sender, ResolveProgressChangedEventArgs e)
//    {
//        PeerNameRecord peer = e.PeerNameRecord;

//        foreach (IPEndPoint ep in peer.EndPointCollection)
//        {
//            if (ep.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
//            {
//                try
//                {
//                    string endpointUrl = string.Format("net.tcp://{0}:{1}/P2PService", ep.Address, ep.Port);
//                    NetTcpBinding binding = new NetTcpBinding();
//                    binding.Security.Mode = SecurityMode.None;
//                    IP2PService serviceProxy = ChannelFactory<IP2PService>.CreateChannel(
//                        binding, new EndpointAddress(endpointUrl));
//                    PeerList.Items.Add(
//                       new PeerEntry
//                       {
//                           PeerName = peer.PeerName,
//                           ServiceProxy = serviceProxy,
//                           DisplayString = serviceProxy.GetName(),
//                           ButtonsEnabled = true
//                       });
//                }
//                catch (EndpointNotFoundException)
//                {
//                    PeerList.Items.Add(
//                       new PeerEntry
//                       {
//                           PeerName = peer.PeerName,
//                           DisplayString = "Неизвестный пир",
//                           ButtonsEnabled = false
//                       });
//                }
//            }
//        }
//    }

//    private void PeerList_Click(object sender, EventArgs e)
//    {
//        // Убедимся, что пользователь щелкнул по кнопке с именем MessageButton
//        //if (((Button)e.OriginalSource).Name == "MessageButton")
//        {
//            // Получение пира и прокси, для отправки сообщения
//            //PeerEntry peerEntry = ((Button)e.OriginalSource).DataContext as PeerEntry;
//            PeerEntry peerEntry = new PeerEntry() as PeerEntry;// (e.ToString) as PeerEntry;
//            if (peerEntry != null && peerEntry.ServiceProxy != null)
//            {
//                try
//                {
//                    peerEntry.ServiceProxy.SendCommand("Привет друг!", ConfigurationManager.AppSettings["username"]);
//                }
//                catch (CommunicationException)
//                {

//                }
//            }
//        }
//    }

//internal void DisplayMessage(string message, string from)
//{
//    // Показать полученное сообщение (вызывается из службы WCF)
//    MessageBox.Show(message, string.Format("Сообщение от {0}", from),
//        MessageBoxButtons.OK);
//}

//}
