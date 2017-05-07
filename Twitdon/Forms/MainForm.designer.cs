namespace Twitdon
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.タイムライン追加TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userpawoonetToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.publicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.アカウント管理AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.登録RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.削除DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userpawoonetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.タイムライン追加TToolStripMenuItem,
            this.アカウント管理AToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1132, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // タイムライン追加TToolStripMenuItem
            // 
            this.タイムライン追加TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userpawoonetToolStripMenuItem1});
            this.タイムライン追加TToolStripMenuItem.Name = "タイムライン追加TToolStripMenuItem";
            this.タイムライン追加TToolStripMenuItem.Size = new System.Drawing.Size(114, 20);
            this.タイムライン追加TToolStripMenuItem.Text = "タイムライン追加(&T)";
            // 
            // userpawoonetToolStripMenuItem1
            // 
            this.userpawoonetToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem,
            this.publicToolStripMenuItem});
            this.userpawoonetToolStripMenuItem1.Name = "userpawoonetToolStripMenuItem1";
            this.userpawoonetToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            this.userpawoonetToolStripMenuItem1.Text = "user@pawoo.net";
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.homeToolStripMenuItem.Text = "Home";
            // 
            // publicToolStripMenuItem
            // 
            this.publicToolStripMenuItem.Name = "publicToolStripMenuItem";
            this.publicToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.publicToolStripMenuItem.Text = "Public";
            // 
            // アカウント管理AToolStripMenuItem
            // 
            this.アカウント管理AToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.登録RToolStripMenuItem,
            this.削除DToolStripMenuItem});
            this.アカウント管理AToolStripMenuItem.Name = "アカウント管理AToolStripMenuItem";
            this.アカウント管理AToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
            this.アカウント管理AToolStripMenuItem.Text = "アカウント管理(&A)";
            // 
            // 登録RToolStripMenuItem
            // 
            this.登録RToolStripMenuItem.Name = "登録RToolStripMenuItem";
            this.登録RToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.登録RToolStripMenuItem.Text = "登録(&R)";
            this.登録RToolStripMenuItem.Click += new System.EventHandler(this.登録RToolStripMenuItem_Click);
            // 
            // 削除DToolStripMenuItem
            // 
            this.削除DToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userpawoonetToolStripMenuItem});
            this.削除DToolStripMenuItem.Name = "削除DToolStripMenuItem";
            this.削除DToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.削除DToolStripMenuItem.Text = "削除(&D)";
            // 
            // userpawoonetToolStripMenuItem
            // 
            this.userpawoonetToolStripMenuItem.Name = "userpawoonetToolStripMenuItem";
            this.userpawoonetToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.userpawoonetToolStripMenuItem.Text = "user@pawoo.net";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1132, 557);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(1132, 581);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Twitdon";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem タイムライン追加TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userpawoonetToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem publicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem アカウント管理AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 登録RToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 削除DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userpawoonetToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    }
}

