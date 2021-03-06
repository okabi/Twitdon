﻿namespace Twitdon
{
    partial class TimeLineFrame
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelTimeLineName = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.PictureBox();
            this.panel = new Twitdon.ScrollablePanel();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTimeLineName
            // 
            this.labelTimeLineName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(40)))));
            this.labelTimeLineName.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelTimeLineName.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTimeLineName.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelTimeLineName.ForeColor = System.Drawing.SystemColors.Control;
            this.labelTimeLineName.Location = new System.Drawing.Point(0, 0);
            this.labelTimeLineName.Name = "labelTimeLineName";
            this.labelTimeLineName.Size = new System.Drawing.Size(324, 20);
            this.labelTimeLineName.TabIndex = 0;
            this.labelTimeLineName.Text = "User@twitter";
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeButton.Image = global::Twitdon.Properties.Resources.close;
            this.closeButton.Location = new System.Drawing.Point(306, 2);
            this.closeButton.Margin = new System.Windows.Forms.Padding(2);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(16, 16);
            this.closeButton.TabIndex = 3;
            this.closeButton.TabStop = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.AutoScroll = true;
            this.panel.Location = new System.Drawing.Point(0, 20);
            this.panel.Margin = new System.Windows.Forms.Padding(0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(324, 396);
            this.panel.TabIndex = 2;
            this.panel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel_Scroll);
            // 
            // TimeLineFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.labelTimeLineName);
            this.Name = "TimeLineFrame";
            this.Size = new System.Drawing.Size(324, 416);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTimeLineName;
        private ScrollablePanel panel;
        private System.Windows.Forms.PictureBox closeButton;
    }
}
