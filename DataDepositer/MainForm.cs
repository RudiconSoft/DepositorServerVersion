using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.Threading;
using System.Net.PeerToPeer;
using System.Windows;
using System.Net;
using System.ServiceModel;
using Shared;
using System.Windows.Threading;

namespace DataDepositer
{
    public partial class MainForm : Form
    {
        SetUserForm formSetUser = new SetUserForm();
        StorageForm formStorage = new StorageForm();
        SettingsForm formSettings = new SettingsForm();

        BackgroundWorker bgwFileSender = new BackgroundWorker(); // Thread for File send
        BackgroundWorker bgwFileReciever = new BackgroundWorker(); // Thread for File recieve

        UserData user = new UserData();
        FileData file = new FileData();
        bool isFileSelected = false;
        bool isUserDefined = false;


        //IniFile INI = new IniFile("config.ini");
        IniFile INI = null;// new IniFile("config.ini");
        Config config = new Config();
        private NetworkP2P network;
        //public string ConfigName = "config.ini";

        // 2017-11-23 Added Artem Nikolaev
        Client client = new Client();

    //    #region P2P UDP hole punch 
    //    public MainWindow()
    //    {
    //        InitializeComponent();

    //        client.OnServerConnect += Client_OnServerConnect;
    //        client.OnServerDisconnect += Client_OnServerDisconnect;
    //        client.OnResultsUpdate += Client_OnResultsUpdate;
    //        client.OnClientAdded += Client_OnClientAdded;
    //        client.OnClientUpdated += Client_OnClientUpdated;
    //        client.OnClientRemoved += Client_OnClientRemoved;
    //        client.OnClientConnection += Client_OnClientConnection;
    //        client.OnMessageReceived += Client_OnMessageReceived;
    //    }

    //    private void btnConnect_Click(object sender, RoutedEventArgs e)
    //    {
    //        client.ConnectOrDisconnect();
    //    }

    //    private void Client_OnServerConnect(object sender, EventArgs e)
    //    {
    //        btnConnect.Content = "Disconnect";
    //        chkUPnP.IsEnabled = false;
    //    }

    //    private void Client_OnServerDisconnect(object sender, EventArgs e)
    //    {
    //        Dispatcher.Invoke(delegate
    //        {
    //            btnConnect.Content = "Connect";
    //            lstClients.Items.Clear();
    //            chkUPnP.IsEnabled = true;

    //            for (int c = 0; c < ChatWindows.Count - 1; c++)
    //                ChatWindows[c].Close();
    //        });
    //    }

    //    private void Client_OnResultsUpdate(object sender, string e)
    //    {
    //        try
    //        {
    //            Dispatcher.Invoke(delegate
    //            {
    //                txtResults.Text += e + '\n';
    //                txtResults.CaretIndex = txtResults.Text.Length;
    //                txtResults.ScrollToEnd();
    //            });
    //        }
    //        catch
    //        {

    //        }
    //    }

    //    private void Client_OnClientAdded(object sender, ClientInfo e)
    //    {
    //        Dispatcher.Invoke(delegate
    //        {
    //            lstClients.Items.Add(e);
    //        });
    //    }

    //    private void Client_OnClientUpdated(object sender, ClientInfo e)
    //    {
    //        Dispatcher.Invoke(delegate
    //        {
    //            foreach (ClientInfo CI in lstClients.Items)
    //                if (CI.ID == e.ID)
    //                    CI.Update(e);

    //            RefreshDetails();
    //        });
    //    }

    //    private void Client_OnClientRemoved(object sender, ClientInfo e)
    //    {
    //        int i = -1;
    //        ChatWindow Chat = null;

    //        foreach (ClientInfo CI in lstClients.Items)
    //            if (CI.ID == e.ID)
    //                i = lstClients.Items.IndexOf(CI);

    //        foreach (ChatWindow CW in ChatWindows)
    //            if (CW.ID == e.ID)
    //                Chat = CW;

    //        Dispatcher.Invoke(delegate
    //        {
    //            if (i != -1)
    //                lstClients.Items.RemoveAt(i);

    //            if (Chat != null)
    //                Chat.Close();

    //            RefreshDetails();
    //        });
    //    }

    //    private void Client_OnClientConnection(object sender, IPEndPoint e)
    //    {
    //        Dispatcher.Invoke(delegate
    //        {
    //            ChatWindow chat = ChatWindows.FirstOrDefault(C => C.RemoteEP.Equals(e));

    //            if (chat == null)
    //            {
    //                chat = new ChatWindow(client, ((ClientInfo)sender).Name, e, ((ClientInfo)sender).ID);
    //                ChatWindows.Add(chat);
    //                chat.Closed += delegate { ChatWindows.Remove(chat); };
    //                chat.Show();
    //            }
    //            else
    //            {
    //                chat.Focus();
    //                chat.BringIntoView();
    //            }

    //            chat.txtMessage.Focus();
    //        });
    //    }

    //    private void Client_OnMessageReceived(object sender, MessageReceivedEventArgs e)
    //    {
    //        Dispatcher.Invoke(delegate
    //        {
    //            ChatWindow chat = ChatWindows.FirstOrDefault(C => C.RemoteEP.Equals((IPEndPoint)sender));

    //            if (chat == null)
    //            {
    //                chat = new ChatWindow(client, e.clientInfo.Name, e.EstablishedEP, e.clientInfo.ID);
    //                ChatWindows.Add(chat);
    //                chat.Closed += delegate { ChatWindows.Remove(chat); };
    //                chat.Show();
    //            }
    //            else
    //            {
    //                chat.Focus();
    //                chat.BringIntoView();
    //            }

    //            chat.ReceiveMessage(e.message);
    //        });
    //    }

    //    private void btnConnectClient_Click(object sender, RoutedEventArgs e)
    //    {
    //        //if (lstClients.SelectedItem != null)
    //        //{
    //        //    ClientInfo CI = (ClientInfo)lstClients.SelectedItem;
    //        //    client.ConnectToClient(CI);
    //        //}
    //    }

    //    private void lstClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //    {
    //        RefreshDetails();
    //    }

    //    public void RefreshDetails()
    //    {
    //        //if (lstClients.SelectedItem != null)
    //        //{
    //        //    ClientInfo CI = (ClientInfo)lstClients.SelectedItem;

    //        //    lblName.Content = "Name: " + CI.Name;
    //        //    lblUPnP.Content = "UPnP Enabled: " + CI.UPnPEnabled;
    //        //    lblExtEP.Content = "Ext EP: " + (CI.ExternalEndpoint != null ? CI.ExternalEndpoint.ToString() : "None");
    //        //    lblIntEP.Content = "Int EP: " + (CI.InternalEndpoint != null ? CI.InternalEndpoint.ToString() : "None");
    //        //    lblConType.Content = "Method: " + CI.ConnectionType.ToString();

    //        //    lblIPs.Content = "Int IPs: ";

    //        //    foreach (IPAddress IP in CI.InternalAddresses)
    //        //        lblIPs.Content += IP + ", ";

    //        //    btnConnectClient.IsEnabled = (CI.ID != client.LocalClientInfo.ID);
    //        //}
    //        //else
    //        //{
    //        //    lblName.Content = "Name: ";
    //        //    lblUPnP.Content = "UPnP Enabled: ";
    //        //    lblExtEP.Content = "Ext EP: ";
    //        //    lblIntEP.Content = "Int EP: ";
    //        //    lblConType.Content = "Type: ";
    //        //    lblIPs.Content = "Int IPs: ";
    //        //}
    //    }

    //    private void btnClear_Click(object sender, RoutedEventArgs e)
    //    {
    //        txtResults.Clear();
    //    }

    //    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    //    {
    //        client.ClearUpUPnP();

    //        for (int c = 0; c < ChatWindows.Count; c++)
    //            ChatWindows[c].Close();
    //    }

    //    private void chkUPnP_Checked(object sender, RoutedEventArgs e)
    //    {
    //        client.UPnPEnabled = (bool)chkUPnP.IsChecked;
    //    }
    //}

    //#endregion

        public MainForm(string configname)
        {
            InitializeComponent();
            INI = new IniFile(configname);
            InitConfig();
            //InitP2P();

            // init internal storages with
            InitLists();
            InitBackgroundWorkers();
            timerNetworkCheck.Start();
            //bgwNetwork.RunWorkerAsync();
        }

        private void InitBackgroundWorkers()
        {
            // Init file sender
            this.bgwFileSender.DoWork += new DoWorkEventHandler(this.bgwFileSender_DoWork);
            this.bgwFileSender.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwFileSender_RunWorkerCompleted);

            // Init file reciever

        }

        private void InitP2P()
        {
            network = new NetworkP2P(Application.ProductName, config);
            network.mainform = this;
            network.Init();
            Logger.Log.Info("Start P2P Network...");
            network.Start();
        }

        private void ResolveP2P()
        {
            //network.Resolve();
        }

        #region Lists init.

        // init lists with data.
        private void InitLists()
        {
            InitStorageList();
            InitSendList();
            //InitAssembleList();
        }

        private void InitAssembleList()
        {
            throw new NotImplementedException();
        }

        private void InitSendList()
        {
            //FileInfo fi 
            if (File.Exists(config.SendFolder + "List.xml"))
            {
                // Serialize list.xml
                ReadSendList();
            }
            else
            {
                // create new xml list from files in StorageFolder
                CreateNewSendList();
            }
        }

        private void CreateNewSendList()
        {
            DirectoryInfo di = new DirectoryInfo(config.SendFolder);
            if (di.Exists)
            {
                FileManipulator fm = new FileManipulator();
                foreach (var f in di.GetFiles())
                {
                    try
                    {
                        // get header from file
                        STORED_FILE_HEADER sfh = fm.GetHeaderFromFile(f.FullName);

                        // fill SendItem from header
                        string originname = f.Name.Substring(0, f.Name.IndexOf(".part") - 5); // if string not found file name ignored
                        SendItem si = new SendItem(sfh.FileName, originname, sfh.Description, sfh.OriginSize, 
                            (uint)sfh.ChunksQty, (uint)sfh.ChunkNum, sfh.MD5Chunk, sfh.MD5Origin, 0);

                        // add Send item into List
                        Vault.SendList.Add(si);

                    }
                    catch (Exception e)
                    {
                        Logger.Log.Error("Error in SendList creation process.");
                        Logger.Log.Error(e.Message);
                    }
                }

                // Serialize StorageList
                XmlSerializer formatter = new XmlSerializer(typeof(List<SendItem>));
                using (FileStream fs = new FileStream(config.SendFolder + "List.xml", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, Vault.SendList);
                }
            }
            else
            {
                di.Create(); // just create empty StoredFolder 
            }
        }

        private void ReadSendList()
        {
            // Deserialize SendList
            XmlSerializer formatter = new XmlSerializer(typeof(List<SendItem>));
            using (FileStream fs = new FileStream(config.SendFolder + "List.xml", FileMode.Open))
            {
                Vault.SendList = (List<SendItem>)formatter.Deserialize(fs);
            }
        }

        private void InitStorageList()
        {
            if (File.Exists(config.StorageFolder + "List.xml"))
            {
                // Serialize list.xml
                ReadStorageList();
            }
            else
            {
                // create new xml list from files in SendFolder
                CreateNewStorageList();
            }

        }

        private void ReadStorageList()
        {
            // Deserialize StorageList
            XmlSerializer formatter = new XmlSerializer(typeof(List<StorageItem>));
            using (FileStream fs = new FileStream(config.StorageFolder + "List.xml", FileMode.Open))
            {
                Vault.StorageList = (List<StorageItem>) formatter.Deserialize(fs); 
            }
        }

        private void CreateNewStorageList()
        {
            DirectoryInfo di = new DirectoryInfo(config.StorageFolder);
            if (di.Exists)
            {
                FileManipulator fm = new FileManipulator();
                foreach (var f in di.GetFiles())
                {
                    try
                    {
                        // get header from file
                        STORED_FILE_HEADER sfh = fm.GetHeaderFromFile(f.FullName);

                        // fill StorageItem from header
                        string originname = f.Name.Substring(0, f.Name.IndexOf(".part") - 5); // if string not found file name ignored
                        StorageItem si = new StorageItem(sfh.FileName, originname, sfh.Description, sfh.OriginSize, (uint) sfh.ChunksQty, (uint) sfh.ChunkNum, sfh.MD5Chunk, sfh.MD5Origin);

                        // add Storage item into List
                        Vault.StorageList.Add(si);

                    }
                    catch (Exception e )
                    {
                        Logger.Log.Error("Error in StoredList creation process.");
                        Logger.Log.Error(e.Message);
                        //throw;
                    }
                }

                // Serialize StorageList
                XmlSerializer formatter = new XmlSerializer(typeof(List<StorageItem>));
                using (FileStream fs = new FileStream(config.StorageFolder + "List.xml", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, Vault.StorageList);
                }
            }
            else
            {
                di.Create(); // just create empty StoredFolder 
            }
        }
        #endregion

        #region Config init.

        private void InitConfig()
        {
            if (INI.Exists())
            {
                // read data from INI
                config.FromINI(INI);
            }
            else
            {
                // create INI file or use settings ?
                config.ToINI(INI);
            }
            
        }
        #endregion


        #region Main Form methods

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                notifyIcon.Visible = false;
            }
        }

        private void openFileDialogButton_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog.ShowDialog();

            // Get file name
            if (res == DialogResult.OK)
            {
                setFileName(openFileDialog.FileName);
            }
        }

        private void buttonSetUser_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            DialogResult res = formSetUser.ShowDialog();

            if (res != DialogResult.Cancel)
            {
                user.SetUserData(formSetUser.userName, formSetUser.password);
                labelName.Text = user.GetName();
                //isUserDefined = true;
            }
            this.Visible = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO add INI file and settings read.

        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Count() > 0)
            {
                string fn = files.First(); //
                Logger.Log.Info("Drag&Drop file : " + fn);
                setFileName(fn);
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void btnStartProcess_Click(object sender, EventArgs e)
        {
            if (isFileSelected && user.IsSet)
            {
                STORED_FILE_HEADER sh = new STORED_FILE_HEADER();
                sh.Description = tbFileDescription.Text;
                Helper helper = new Helper();
                FileManipulator fm = new FileManipulator();
                
                // pack file
                Directory.CreateDirectory(config.TempFolder);
                var fileInputName = file.GetFileName();
                byte[] buffer = File.ReadAllBytes(fileInputName);

                FileInfo fi = new FileInfo(fileInputName);
                sh.cb = (uint)Marshal.SizeOf(sh); // header size
                sh.OriginSize = (ulong) fi.Length;
                sh.MD5Origin = new Helper().GetFileMD5(fileInputName);

                var fileOutputName = config.TempFolder + "\\" + Path.GetFileName(file.GetFileName());
                using (var file = File.Open(fileOutputName, FileMode.Create))
                using (var stream = new DeflateStream(file, CompressionMode.Compress))
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(buffer);
                }

                // encrypt file
                byte[] buff = File.ReadAllBytes(fileOutputName);
                new AESEnDecryption().BinarySaveObjectWithAes(buff, fileOutputName, user.GetName(), user.GetPassword());

                // split file 
                fm.SplitFile(fileOutputName, config.SendFolder, config.Chunks, sh);

                // @TODO Add SendList filling
                DirectoryInfo di = new DirectoryInfo(config.SendFolder);


            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // log each exiting
            if (network != null)
            {
                network.Stop();
            }
            Logger.Log.Info("Quit application.");
        }
        #endregion


        // private section of methods.

        #region Private methods region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"> File name for processing </param>
        private void setFileName(string filename)
        {
            if (filename.Length > 0)
            {
                file.SetFileName(filename);
                labelFileName.Text = filename;
                isFileSelected = true;
            }
            else
            {
                Logger.Log.Error("Empty file name !!!");
                throw new Exception("Empty file name !!!");
            }
        }

        private void loadIni()
        {
            // if INI exists load data, else do nothing
            if (INI.KeyExists("SettingMainForm", "Width"))
            {
                this.Height = int.Parse(INI.Read("SettingMainForm", "Height"));
            }
            //else
            //    numericUpDown1.Value = this.MinimumSize.Height;

            if (INI.KeyExists("SettingMainForm", "Width"))
            {
                this.Width = int.Parse(INI.Read("SettingMainForm", "Width"));
            }
            //else
            //    numericUpDown2.Value = this.MinimumSize.Width;

            //if (INI.KeyExists("SettingMainForm", "Parts"))
                //textBox1.Text = INI.ReadINI("Other", "Text");

            //this.Height = int.Parse(numericUpDown1.Value.ToString());
            //this.Width = int.Parse(numericUpDown2.Value.ToString());
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            DialogResult res = formSettings.ShowDialog();

            if (res != DialogResult.Cancel)
            {
                // save settings

            }
            this.Visible = true;
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    this.Visible = false;
        //    //formStorage.InitLists(Vault.StorageList, Vault.SendList, Vault.AssembleList);
        //    DialogResult res = formStorage.ShowDialog();

        //    if (res != DialogResult.Cancel)
        //    {
        //        // @TODO implementation

        //    }
        //    this.Visible = true;

        //}

        private void btnViewStorage_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            //formStorage.InitLists(Vault.StorageList, Vault.SendList, Vault.AssembleList);
            DialogResult res = formStorage.ShowDialog();

            if (res != DialogResult.Cancel)
            {
                // @TODO implementation

            }
            this.Visible = true;

        }
        #endregion

        #region Background Workers Networker Region
        private void bgwNetwork_DoWork(object sender, DoWorkEventArgs e)
        {
            //bgwNetwork.
            Command command = e.Argument as Command;
            //CommandQueue queue = sender as CommandQueue;

            //for (int i = 0; i < command.Counter; i++)
            //{
            //    Logger.Log.Info(" bgwNetwork_DoWork  Test  " + command.Message + "  " + Convert.ToString(i));
            //    Thread.Sleep(10000); // Sleep 10 sec
            //}
            if (Vault.Peers.Count > 0)
            {
                int random = new Random().Next(0, Vault.Peers.Count - 1);
                PeerEntry entry = Vault.Peers.ElementAt(random);
                if (entry != null && entry.ServiceProxy != null)
                {
                    entry.ServiceProxy.SendCommand(command, Convert.ToString(config.Port));
                    entry.ServiceProxy.SendMessage(command.Message, Convert.ToString(config.Port));
                }
            }
            //foreach ( PeerEntry item in Vault.Peers)
            //{
            //    item.ServiceProxy.SendCommand(command, user.GetName());
            //}

            //ResolveP2P();
        }

        private void bgwNetwork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Logger.Log.Info(" bgwNetwork_DoWork  Test Complete ");
        }

        private void timerNetworkCheck_Tick(object sender, EventArgs e)
        {
            //Command command = new Command("Start on Timer", new Random().Next() % 10 + 1);
            //timerNetworkCheck.Interval = (new Random().Next() % 100 + 100) * 1000;
            //if (!bgwNetwork.IsBusy)
            //{
            //    bgwNetwork.RunWorkerAsync(command);
            //}
            //else
            //{
            //    Logger.Log.Info(" BackgroundWorker - " + bgwNetwork.ToString() + " is busy !!");
            //}
        }
        #endregion

        private void timerResolver_Tick(object sender, EventArgs e)
        {
            //if (!bgwP2PResolver.IsBusy)
            //{
            //    bgwP2PResolver.RunWorkerAsync(); // 
            //}
        }


        #region BackgroundWorker for File Sender
        private void bgwFileSender_DoWork(object sender, DoWorkEventArgs e)
        {
            Command command = e.Argument as Command;
            //CommandQueue queue = sender as CommandQueue;

        }

        private void bgwFileSender_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
        #endregion

        #region BackgroundWorker for File Reciever
        private void bgwFileReciever_DoWork(object sender, DoWorkEventArgs e)
        {
            Command command = e.Argument as Command;
            //CommandQueue queue = sender as CommandQueue;

        }

        private void bgwFileReciever_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        #endregion

        #region BackgroundWorker for CommandManager
        private void bgwCommandManager_DoWork(object sender, DoWorkEventArgs e)
        {
            Command command = e.Argument as Command;


        }
        #endregion

        #region BackgroundWorker for P2PResolver
        private void bgwP2PResolver_DoWork(object sender, DoWorkEventArgs e)
        {
            //// Создание распознавателя и добавление обработчиков событий
            //PeerNameResolver resolver = new PeerNameResolver();
            //resolver.ResolveProgressChanged +=
            //    new EventHandler<ResolveProgressChangedEventArgs>(resolver_ResolveProgressChanged);
            //resolver.ResolveCompleted +=
            //    new EventHandler<ResolveCompletedEventArgs>(resolver_ResolveCompleted);

            //// Подготовка к добавлению новых пиров
            //Vault.Peers.Clear();

            //// Преобразование незащищенных имен пиров асинхронным образом
            ////resolver.ResolveAsync(new PeerName("0.P2P Sample"), 1);
            //PeerName peername = new PeerName(Application.ProductName, PeerNameType.Unsecured);
            //resolver.ResolveAsync(peername, Cloud.Available);

            //network.ResolveAsync();
            network.Resolve2();

        }

        //private void resolver_ResolveCompleted(object sender, ResolveCompletedEventArgs e)
        //{
        //    // Сообщение об ошибке, если в облаке не найдены пиры
        //    if (Vault.Peers.Count == 0)
        //    {
        //        Vault.Peers.Add(
        //           new PeerEntry
        //           {
        //               DisplayString = "Пиры не найдены.",
        //           });
        //    }
        //    //// Повторно включаем кнопку "обновить"
        //    //RefreshButton.IsEnabled = true;
        //}

        //private void resolver_ResolveProgressChanged(object sender, ResolveProgressChangedEventArgs e)
        //{
        //    PeerNameRecord peer = e.PeerNameRecord;

        //    foreach (IPEndPoint ep in peer.EndPointCollection)
        //    {
        //        if (ep.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        //        {
        //            try
        //            {
        //                string endpointUrl = string.Format("net.tcp://{0}:{1}/P2PService", ep.Address, ep.Port);
        //                NetTcpBinding binding = new NetTcpBinding();
        //                //binding.Security.Mode = SecurityMode.None;
        //                binding.Security.Mode = SecurityMode.None;
        //                IP2PService serviceProxy = ChannelFactory<IP2PService>.CreateChannel(
        //                    binding, new EndpointAddress(endpointUrl));
        //                Vault.Peers.Add(
        //                   new PeerEntry
        //                   {
        //                       PeerName = peer.PeerName,
        //                       ServiceProxy = serviceProxy,
        //                       DisplayString = serviceProxy.GetName(),
        //                   });
        //            }
        //            catch (EndpointNotFoundException)
        //            {
        //                Vault.Peers.Add(
        //                   new PeerEntry
        //                   {
        //                       PeerName = peer.PeerName,
        //                       DisplayString = "Unknown Peer",
        //                   });
        //            }
        //        }
        //    }
        //}

        private void PeerList_Click(object sender, EventArgs e)
        {
            ////// Убедимся, что пользователь щелкнул по кнопке с именем MessageButton
            ////if (((Button)e.OriginalSource).Name == "MessageButton")
            //{
            //    // Получение пира и прокси, для отправки сообщения
            //    //PeerEntry peerEntry = ((Button)e.OriginalSource).DataContext as PeerEntry;
            //    PeerEntry peerEntry = new PeerEntry();  //as PeerEntry;
            //    if (peerEntry != null && peerEntry.ServiceProxy != null)
            //    {
            //        try
            //        {
            //            peerEntry.ServiceProxy.SendCommand("Привет друг!", "");
            //        }
            //        catch (CommunicationException)
            //        {

            //        }
            //    }
            //}
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (!bgwP2PResolver.IsBusy)
            {
                Vault.Peers.Clear();

                btnRefresh.Enabled = false;
                bgwP2PResolver.RunWorkerAsync(); // 
            }
        }

        private void bgwP2PResolver_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnRefresh.Enabled = true;
        }

        private void btnShowPeers_Click(object sender, EventArgs e)
        {
            new ShowPeersForm().Show(this);
        }

        #region Command Tester Button Region

        private void btnCommandTester_Click(object sender, EventArgs e)
        {
            if (tbCommand.Text.Length > 0)
            {
                // create command from string
                try
                {
                    switch (tbCommand.Text)
                    {
                        case "SendFile":
                        {
                                string sendfile = file.GetFileName();
                                send(sendfile);
                            break;
                        }
                        case "GetList":
                        {
                                getlist();
                                break;
                        }
                        default:
                            break;
                    }

                    if (tbCommand.Text == "SendFile")
                    {

                    }
                    else
                    {
                        // parse command string 
                        int peernum = new Random().Next(0, Vault.Peers.Count - 1);
                        PeerEntry peerEntry = Vault.Peers.ElementAt(peernum);
                        if (peerEntry != null && peerEntry.ServiceProxy != null)
                        {
                            try
                            {
                                peerEntry.ServiceProxy.SendMessage(tbCommand.Text, network.Username);

                                Command com = new Command(tbCommand.Text, Convert.ToInt32(network.Port));
                                com.xmltest();
                                peerEntry.ServiceProxy.SendCommand(com, network.Username);
                            }
                            catch (CommunicationException)
                            {

                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // if 
                }
            }
        }


        private void getlist()
        {
            int peernum = new Random().Next(0, Vault.Peers.Count - 1);
            PeerEntry peerEntry = Vault.Peers.ElementAt(peernum);

            RemoteListInfo list = peerEntry.ServiceProxy.RequestList();
            //List<StorageFileInfo> storage = new List<StorageFileInfo>();

            ShowFiles form = new ShowFiles();
            form.files = list;
            form.peer = peerEntry;

            form.ShowDialog(this);
        }

        private void send(string fileName)
        {
            //var client = new FileTransferClient.FileTransferServiceClient();
            //var client = 
            int peernum = new Random().Next(0, Vault.Peers.Count - 1);
            PeerEntry peerEntry = Vault.Peers.ElementAt(peernum);
            DownloadRequest dr = new DownloadRequest();
            dr.FileName = fileName;

            RemoteFileInfo info = peerEntry.ServiceProxy.DownloadFile(dr);
            Stream inputStream = info.FileByteStream;

            //using (var writeStream = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write))
            //string outfile = Path.GetFileName(fileName);
            string outfile = Path.GetFileName(fileName);
            using (var writeStream = new FileStream(outfile, FileMode.CreateNew, FileAccess.Write))
            {
                const int bufferSize = 2048;
                var buffer = new byte[bufferSize];

                do
                {
                    int bytesRead = inputStream.Read(buffer, 0, bufferSize);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    writeStream.Write(buffer, 0, bytesRead);
                    //progressBar1.Value = (int)(writeStream.Position * 100 / length);
                }
                while (true);

                writeStream.Close();
            }

            inputStream.Dispose();
        }
        #endregion
        
        #region P2P command from Network Region
        internal void DisplayMessage(string message, string from)
        {
            // Показать полученное сообщение (вызывается из службы WCF)
            MessageBox.Show(message, string.Format("Сообщение от {0}", from));
        }
        internal void ReciveCommand(Command command, string from)
        {
            Vault.MainQueue.Enqueue(command);
            MessageBox.Show(command.Message, string.Format("Recieve command from {0}", from), MessageBoxButtons.OK);
        }
        #endregion

        #region Working with Command TextBox
        private void tbCommand_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbCommand_Enter(object sender, EventArgs e)
        {
            
        }

        private void tbCommand_Click(object sender, EventArgs e)
        {
            tbCommand.SelectAll();
        }
        #endregion

        #region P2PService calls methods
        internal RemoteListInfo RequestList()
        {
            RemoteListInfo list = new RemoteListInfo();
            DirectoryInfo storage = new DirectoryInfo(config.StorageFolder);

            foreach (var item in storage.GetFiles())
            {
                //list.FileList.Add(item.FullName);
                StorageFileInfo sfi = new StorageFileInfo();
                sfi.VirtualPath = item.FullName;
                sfi.Size = item.Length;
                list.FileList.Add(sfi);
            }

            return list;
        }
        #endregion

    }
}
