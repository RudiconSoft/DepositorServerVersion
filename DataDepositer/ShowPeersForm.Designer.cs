namespace DataDepositer
{
    partial class ShowPeersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbPeers = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbPeers
            // 
            this.lbPeers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPeers.FormattingEnabled = true;
            this.lbPeers.Location = new System.Drawing.Point(-1, 0);
            this.lbPeers.Name = "lbPeers";
            this.lbPeers.Size = new System.Drawing.Size(680, 407);
            this.lbPeers.TabIndex = 0;
            this.lbPeers.SelectedIndexChanged += new System.EventHandler(this.lbPeers_SelectedIndexChanged);
            // 
            // ShowPeersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 475);
            this.Controls.Add(this.lbPeers);
            this.Name = "ShowPeersForm";
            this.ShowIcon = false;
            this.Text = "Show Peers";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbPeers;
    }
}