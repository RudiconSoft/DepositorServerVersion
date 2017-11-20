namespace DataDepositer
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.tbStorageSize = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCreateINIFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Local Storage size (Mb) :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbStorageSize
            // 
            this.tbStorageSize.Location = new System.Drawing.Point(139, 13);
            this.tbStorageSize.Name = "tbStorageSize";
            this.tbStorageSize.Size = new System.Drawing.Size(73, 20);
            this.tbStorageSize.TabIndex = 1;
            this.tbStorageSize.Text = "1000";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(13, 346);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(199, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCreateINIFile
            // 
            this.btnCreateINIFile.Location = new System.Drawing.Point(12, 294);
            this.btnCreateINIFile.Name = "btnCreateINIFile";
            this.btnCreateINIFile.Size = new System.Drawing.Size(200, 23);
            this.btnCreateINIFile.TabIndex = 3;
            this.btnCreateINIFile.Text = "Create INI file";
            this.btnCreateINIFile.UseVisualStyleBackColor = true;
            this.btnCreateINIFile.Click += new System.EventHandler(this.btnCreateINIFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Use port (13027 default):";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(139, 61);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(73, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "13027";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 381);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCreateINIFile);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbStorageSize);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbStorageSize;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCreateINIFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
    }
}