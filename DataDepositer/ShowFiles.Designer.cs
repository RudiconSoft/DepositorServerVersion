namespace DataDepositer
{
    partial class ShowFiles
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
            this.FileList = new System.Windows.Forms.ListView();
            this.FilenameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FullPathHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SizeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // FileList
            // 
            this.FileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FilenameColumnHeader,
            this.FullPathHeader,
            this.SizeHeader});
            this.FileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileList.FullRowSelect = true;
            this.FileList.GridLines = true;
            this.FileList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.FileList.Location = new System.Drawing.Point(0, 0);
            this.FileList.Name = "FileList";
            this.FileList.Size = new System.Drawing.Size(744, 476);
            this.FileList.TabIndex = 5;
            this.FileList.UseCompatibleStateImageBehavior = false;
            this.FileList.View = System.Windows.Forms.View.Details;
            this.FileList.SelectedIndexChanged += new System.EventHandler(this.FileList_SelectedIndexChanged);
            // 
            // FilenameColumnHeader
            // 
            this.FilenameColumnHeader.Text = "Filename";
            this.FilenameColumnHeader.Width = 80;
            // 
            // FullPathHeader
            // 
            this.FullPathHeader.Text = "Storage path";
            this.FullPathHeader.Width = 160;
            // 
            // SizeHeader
            // 
            this.SizeHeader.Text = "Size";
            this.SizeHeader.Width = 80;
            // 
            // ShowFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 476);
            this.Controls.Add(this.FileList);
            this.Name = "ShowFiles";
            this.Text = "ShowFiles";
            this.Load += new System.EventHandler(this.ShowFiles_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView FileList;
        private System.Windows.Forms.ColumnHeader FilenameColumnHeader;
        private System.Windows.Forms.ColumnHeader FullPathHeader;
        private System.Windows.Forms.ColumnHeader SizeHeader;
    }
}