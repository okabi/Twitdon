namespace Twitdon
{
    partial class RegisterForm
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
            this.radioButtonTwitter = new System.Windows.Forms.RadioButton();
            this.radioButtonMastodon = new System.Windows.Forms.RadioButton();
            this.panelTwitter = new System.Windows.Forms.Panel();
            this.panelMastodon = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxMastodonPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMastodonEMail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxMastodonDomain = new System.Windows.Forms.TextBox();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.panelServices = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTwitterPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxTwitterEMail = new System.Windows.Forms.TextBox();
            this.panelTwitter.SuspendLayout();
            this.panelMastodon.SuspendLayout();
            this.panelServices.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonTwitter
            // 
            this.radioButtonTwitter.AutoSize = true;
            this.radioButtonTwitter.Checked = true;
            this.radioButtonTwitter.Location = new System.Drawing.Point(12, 9);
            this.radioButtonTwitter.Name = "radioButtonTwitter";
            this.radioButtonTwitter.Size = new System.Drawing.Size(59, 16);
            this.radioButtonTwitter.TabIndex = 0;
            this.radioButtonTwitter.TabStop = true;
            this.radioButtonTwitter.Text = "Twitter";
            this.radioButtonTwitter.UseVisualStyleBackColor = true;
            this.radioButtonTwitter.CheckedChanged += new System.EventHandler(this.radioButtonTwitter_CheckedChanged);
            // 
            // radioButtonMastodon
            // 
            this.radioButtonMastodon.AutoSize = true;
            this.radioButtonMastodon.Location = new System.Drawing.Point(77, 9);
            this.radioButtonMastodon.Name = "radioButtonMastodon";
            this.radioButtonMastodon.Size = new System.Drawing.Size(72, 16);
            this.radioButtonMastodon.TabIndex = 1;
            this.radioButtonMastodon.Text = "Mastodon";
            this.radioButtonMastodon.UseVisualStyleBackColor = true;
            // 
            // panelTwitter
            // 
            this.panelTwitter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTwitter.Controls.Add(this.label1);
            this.panelTwitter.Controls.Add(this.textBoxTwitterPassword);
            this.panelTwitter.Controls.Add(this.label6);
            this.panelTwitter.Controls.Add(this.label7);
            this.panelTwitter.Controls.Add(this.textBoxTwitterEMail);
            this.panelTwitter.Location = new System.Drawing.Point(0, 34);
            this.panelTwitter.Name = "panelTwitter";
            this.panelTwitter.Size = new System.Drawing.Size(341, 110);
            this.panelTwitter.TabIndex = 2;
            // 
            // panelMastodon
            // 
            this.panelMastodon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMastodon.Controls.Add(this.label5);
            this.panelMastodon.Controls.Add(this.textBoxMastodonPassword);
            this.panelMastodon.Controls.Add(this.label4);
            this.panelMastodon.Controls.Add(this.label3);
            this.panelMastodon.Controls.Add(this.textBoxMastodonEMail);
            this.panelMastodon.Controls.Add(this.label2);
            this.panelMastodon.Controls.Add(this.textBoxMastodonDomain);
            this.panelMastodon.Location = new System.Drawing.Point(0, 34);
            this.panelMastodon.Name = "panelMastodon";
            this.panelMastodon.Size = new System.Drawing.Size(341, 110);
            this.panelMastodon.TabIndex = 3;
            this.panelMastodon.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(53, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "パスワード";
            // 
            // textBoxMastodonPassword
            // 
            this.textBoxMastodonPassword.Location = new System.Drawing.Point(111, 85);
            this.textBoxMastodonPassword.Name = "textBoxMastodonPassword";
            this.textBoxMastodonPassword.PasswordChar = '●';
            this.textBoxMastodonPassword.Size = new System.Drawing.Size(218, 19);
            this.textBoxMastodonPassword.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(269, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "アカウントを作成したインスタンス情報を入力してください。";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "登録メールアドレス";
            // 
            // textBoxMastodonEMail
            // 
            this.textBoxMastodonEMail.Location = new System.Drawing.Point(111, 60);
            this.textBoxMastodonEMail.Name = "textBoxMastodonEMail";
            this.textBoxMastodonEMail.Size = new System.Drawing.Size(218, 19);
            this.textBoxMastodonEMail.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "ドメイン";
            // 
            // textBoxMastodonDomain
            // 
            this.textBoxMastodonDomain.Location = new System.Drawing.Point(111, 35);
            this.textBoxMastodonDomain.Name = "textBoxMastodonDomain";
            this.textBoxMastodonDomain.Size = new System.Drawing.Size(107, 19);
            this.textBoxMastodonDomain.TabIndex = 0;
            // 
            // buttonRegister
            // 
            this.buttonRegister.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRegister.Location = new System.Drawing.Point(254, 158);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(75, 23);
            this.buttonRegister.TabIndex = 4;
            this.buttonRegister.Text = "登録する";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // panelServices
            // 
            this.panelServices.Controls.Add(this.radioButtonTwitter);
            this.panelServices.Controls.Add(this.radioButtonMastodon);
            this.panelServices.Location = new System.Drawing.Point(0, 0);
            this.panelServices.Name = "panelServices";
            this.panelServices.Size = new System.Drawing.Size(341, 28);
            this.panelServices.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "パスワード";
            // 
            // textBoxTwitterPassword
            // 
            this.textBoxTwitterPassword.Location = new System.Drawing.Point(147, 57);
            this.textBoxTwitterPassword.Name = "textBoxTwitterPassword";
            this.textBoxTwitterPassword.PasswordChar = '●';
            this.textBoxTwitterPassword.Size = new System.Drawing.Size(182, 19);
            this.textBoxTwitterPassword.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(165, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "アカウント情報を入力してください。";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "ユーザ名 or メールアドレス";
            // 
            // textBoxTwitterEMail
            // 
            this.textBoxTwitterEMail.Location = new System.Drawing.Point(147, 32);
            this.textBoxTwitterEMail.Name = "textBoxTwitterEMail";
            this.textBoxTwitterEMail.Size = new System.Drawing.Size(182, 19);
            this.textBoxTwitterEMail.TabIndex = 9;
            // 
            // RegisterForm
            // 
            this.AcceptButton = this.buttonRegister;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 193);
            this.Controls.Add(this.panelServices);
            this.Controls.Add(this.buttonRegister);
            this.Controls.Add(this.panelTwitter);
            this.Controls.Add(this.panelMastodon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegisterForm";
            this.ShowIcon = false;
            this.Text = "アカウント情報登録 - Twitdon";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RegisterForm_FormClosed);
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.panelTwitter.ResumeLayout(false);
            this.panelTwitter.PerformLayout();
            this.panelMastodon.ResumeLayout(false);
            this.panelMastodon.PerformLayout();
            this.panelServices.ResumeLayout(false);
            this.panelServices.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonTwitter;
        private System.Windows.Forms.RadioButton radioButtonMastodon;
        private System.Windows.Forms.Panel panelTwitter;
        private System.Windows.Forms.Panel panelMastodon;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxMastodonPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMastodonEMail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxMastodonDomain;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.Panel panelServices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTwitterPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxTwitterEMail;
    }
}