using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDepositer
{
    public partial class StorageForm : Form
    {
        //List<StorageItem> StorageList = new List<StorageItem>();
        //List<FileInfo> SendList = new List<FileInfo>();
        //List<FileInfo> AssembleList = new List<FileInfo>();

        public StorageForm()
        {
            InitializeComponent();
        }

        private void lvLocal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvSend_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void StorageForm_Load(object sender, EventArgs e)
        {
            StorageViewInit();
            SendViewInit();
            //AssembleViewInit();
            lvLocal.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void ViewInit<T>(string[] columns, ListView lv, List<T> list)
        {
            //string[] columns = { "File Name", "Description", "Size", "Chunks", "Status" };
            //List<ColumnHeader> columnHeaders = new List<ColumnHeader>(); // columns

            //string[] columns = new Config().GetLocalStorageViewColumns();
            foreach (var item in columns)
            {
                lv.Columns.Add(item);
            }

            lv.View = View.Details;
            foreach (var item in Vault.StorageList)
            {
                //ListViewItem lvi = new ListViewItem(item.ToViewStrings);
                ListViewItem lvi = new ListViewItem(item.OriginName);
                //lvi.ToolTipText = item.Description;
                lvi.Name = item.Name;
                lvi.SubItems.Add(item.Description);
                lvi.SubItems.Add(Convert.ToString(item.Size));
                lvi.SubItems.Add(Convert.ToString(item.Chunks));
                lvi.SubItems.Add("OK!");


                lvLocal.Items.Add(lvi);
                //lvSend.Items.Add((ListViewItem) lvi.Clone());
                //lvAssemble.Items.Add((ListViewItem)lvi.Clone());
            }
        }

        private void SendViewInit()
        {
            //string[] columns = { "File Name", "Description", "Size", "Chunks", "Status" , "Sended / Remain"};
            //List<ColumnHeader> columnHeaders = new List<ColumnHeader>(); // columns

            string[] columns = new Config().GetLocalSendViewColumns();
            foreach (var item in columns)
            {
                lvSend.Columns.Add(item);
            }

            lvSend.View = View.Details;
            foreach (var item in Vault.SendList)
            {
                //ListViewItem lvi = new ListViewItem(item.ToViewStrings);
                ListViewItem lvi = new ListViewItem(item.OriginName);
                //lvi.ToolTipText = item.Description;
                lvi.Name = item.Name;
                lvi.SubItems.Add(item.Description);
                lvi.SubItems.Add(Convert.ToString(item.Size));
                lvi.SubItems.Add(Convert.ToString(item.Chunks));
                lvi.SubItems.Add(Convert.ToString(item.SendedChunks));


                lvSend.Items.Add(lvi);
            }
        }

        private void AssembleViewInit()
        {
            throw new NotImplementedException();
        }

        private void StorageViewInit()
        {
            //string[] columns = { "File Name", "Description", "Size", "Chunks", "Status" };
            //List<ColumnHeader> columnHeaders = new List<ColumnHeader>(); // columns

            string[] columns = new Config().GetLocalStorageViewColumns();
            foreach (var item in columns)
            {
                lvLocal.Columns.Add(item);
            }

            lvLocal.View = View.Details;
            foreach (var item in Vault.StorageList)
            {
                //ListViewItem lvi = new ListViewItem(item.ToViewStrings);
                ListViewItem lvi = new ListViewItem(item.OriginName);
                //lvi.ToolTipText = item.Description;
                lvi.Name = item.Name;
                lvi.SubItems.Add(item.Description);
                lvi.SubItems.Add(Convert.ToString(item.Size));
                lvi.SubItems.Add(Convert.ToString(item.Chunks));
                lvi.SubItems.Add("OK!");

                lvLocal.Items.Add(lvi);
            }
        }

        internal void InitLists(List<StorageItem> storageList, List<SendItem> sendList, List<StorageItem> assembleList)
        {
            //StorageList = storageList;
            //SendList = sendList;
            //AssembleList = assembleList;
        }

        private void tabControlStorage_Selected(object sender, TabControlEventArgs e)
        {
        }

        private void tpLocalList_Click(object sender, EventArgs e)
        {
            
        }

        private void lvAssemble_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
