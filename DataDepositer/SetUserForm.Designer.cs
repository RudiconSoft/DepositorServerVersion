namespace DataDepositer
{
    partial class SetUserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetUserForm));
            this.btnSetUser = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPasswordRepeat = new System.Windows.Forms.TextBox();
            this.labelImage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSetUser
            // 
            this.btnSetUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetUser.Enabled = false;
            this.btnSetUser.Location = new System.Drawing.Point(16, 144);
            this.btnSetUser.Name = "btnSetUser";
            this.btnSetUser.Size = new System.Drawing.Size(75, 23);
            this.btnSetUser.TabIndex = 4;
            this.btnSetUser.Text = "Set User";
            this.btnSetUser.UseVisualStyleBackColor = true;
            this.btnSetUser.Click += new System.EventHandler(this.btnSetUser_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "User name :";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(83, 10);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(125, 20);
            this.tbUserName.TabIndex = 1;
            this.tbUserName.TextChanged += new System.EventHandler(this.tbUserName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Confirm :";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(83, 53);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(125, 20);
            this.tbPassword.TabIndex = 2;
            this.tbPassword.TextChanged += new System.EventHandler(this.tbPassword_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(166, 144);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password :";
            // 
            // tbPasswordRepeat
            // 
            this.tbPasswordRepeat.Location = new System.Drawing.Point(83, 96);
            this.tbPasswordRepeat.Name = "tbPasswordRepeat";
            this.tbPasswordRepeat.PasswordChar = '*';
            this.tbPasswordRepeat.Size = new System.Drawing.Size(125, 20);
            this.tbPasswordRepeat.TabIndex = 3;
            this.tbPasswordRepeat.TextChanged += new System.EventHandler(this.tbPasswordRepeat_TextChanged);
            // 
            // labelImage
            // 
            this.labelImage.Image = global::DataDepositer.Properties.Resources.red_invalid1;
            this.labelImage.Location = new System.Drawing.Point(217, 46);
            this.labelImage.Name = "labelImage";
            this.labelImage.Size = new System.Drawing.Size(32, 32);
            this.labelImage.TabIndex = 6;
            // 
            // SetUserForm
            // 
            this.AcceptButton = this.btnSetUser;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(253, 175);
            this.Controls.Add(this.labelImage);
            this.Controls.Add(this.tbPasswordRepeat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSetUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Set USER ...";
            this.Load += new System.EventHandler(this.setUserForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSetUser;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPasswordRepeat;
        private System.Windows.Forms.Label labelImage;
    }
}