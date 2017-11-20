using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDepositer
{
    public partial class ShowFiles : Form
    {
        internal RemoteListInfo files = new RemoteListInfo();
        internal PeerEntry peer = new PeerEntry();

        public ShowFiles()
        {
            InitializeComponent();
        }

        private void RefreshFileList()
        {
            //StorageFileInfo[] files = null;

            //using (FileRepositoryServiceClient client = new FileRepositoryServiceClient())
            //{
            //    files = client.List(null);
            //}


            FileList.Items.Clear();

            int width = FileList.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;

            float[] widths = { .2f, .6f, .2f };

            for (int i = 0; i < widths.Length; i++)
                FileList.Columns[i].Width = (int)((float)width * widths[i]);


            foreach (var file in files.FileList)
            {
                ListViewItem item = new ListViewItem(Path.GetFileName(file.VirtualPath));

                item.SubItems.Add(file.VirtualPath);

                float fileSize = (float)file.Size / 1024.0f;
                string suffix = "Kb";

                if (fileSize > 1000.0f)
                {
                    fileSize /= 1024.0f;
                    suffix = "Mb";
                }
                if (fileSize > 1000.0f)
                {
                    fileSize /= 1024.0f;
                    suffix = "Gb";
                }
                item.SubItems.Add(string.Format("{0:0.0} {1}", fileSize, suffix));

                FileList.Items.Add(item);
            }
        }

        private void ShowFiles_Load(object sender, EventArgs e)
        {
            RefreshFileList();
        }

        //private void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        private void FileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (peer.ServiceProxy != null)
            {
                if (FileList.SelectedItems.Count == 0)
                {
                    MessageBox.Show("You must select a file to download");
                }
                else
                {
                    ListViewItem item = FileList.SelectedItems[0];

                    // Strip off 'Root' from the full path
                    string path = item.SubItems[1].Text;

                    // Ask where it should be saved
                    SaveFileDialog dlg = new SaveFileDialog()
                    {
                        RestoreDirectory = true,
                        OverwritePrompt = true,
                        Title = "Save as...",
                        FileName = Path.GetFileName(path)
                    };

                    dlg.ShowDialog(this);

                    if (!string.IsNullOrEmpty(dlg.FileName))
                    {
                        // Get the file from the server
                        using (FileStream output = new FileStream(dlg.FileName, FileMode.Create))
                        {
                            Stream downloadStream;

                            //using (FileRepositoryServiceClient client = new FileRepositoryServiceClient())
                            //{
                            //    downloadStream = client.GetFile(path);
                            //}
                            DownloadRequest dr = new DownloadRequest();
                            dr.FileName = path;
                            RemoteFileInfo rfi =  peer.ServiceProxy.DownloadFile(dr);
                            downloadStream = rfi.FileByteStream;
                            downloadStream.CopyTo(output);
                        }

                        // Process.Start(dlg.FileName);
                    }
                }

            }
        }
    }
}
