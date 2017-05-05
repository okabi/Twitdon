namespace Twitdon
{
    partial class TimeLineStatus
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
            this.labelUser = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelContent = new System.Windows.Forms.Label();
            this.pictureDelete = new System.Windows.Forms.PictureBox();
            this.pictureFavorite = new System.Windows.Forms.PictureBox();
            this.pictureReblog = new System.Windows.Forms.PictureBox();
            this.pictureReply = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFavorite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureReblog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureReply)).BeginInit();
            this.SuspendLayout();
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelUser.Location = new System.Drawing.Point(3, 3);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(108, 20);
            this.labelUser.TabIndex = 0;
            this.labelUser.Text = "ユーザー @user";
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelInfo.Location = new System.Drawing.Point(3, 93);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(89, 18);
            this.labelInfo.TabIndex = 3;
            this.labelInfo.Text = "1分前 via Web";
            // 
            // labelContent
            // 
            this.labelContent.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelContent.Location = new System.Drawing.Point(4, 23);
            this.labelContent.Name = "labelContent";
            this.labelContent.Size = new System.Drawing.Size(213, 63);
            this.labelContent.TabIndex = 6;
            this.labelContent.Text = "あああああああああああああああああああああああああああああああああああああ";
            this.labelContent.UseMnemonic = false;
            // 
            // pictureDelete
            // 
            this.pictureDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureDelete.Image = global::Twitdon.Properties.Resources.delete;
            this.pictureDelete.Location = new System.Drawing.Point(201, 110);
            this.pictureDelete.Name = "pictureDelete";
            this.pictureDelete.Size = new System.Drawing.Size(16, 16);
            this.pictureDelete.TabIndex = 7;
            this.pictureDelete.TabStop = false;
            // 
            // pictureFavorite
            // 
            this.pictureFavorite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureFavorite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureFavorite.Image = global::Twitdon.Properties.Resources.star_before;
            this.pictureFavorite.Location = new System.Drawing.Point(179, 110);
            this.pictureFavorite.Name = "pictureFavorite";
            this.pictureFavorite.Size = new System.Drawing.Size(16, 16);
            this.pictureFavorite.TabIndex = 5;
            this.pictureFavorite.TabStop = false;
            // 
            // pictureReblog
            // 
            this.pictureReblog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureReblog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureReblog.Image = global::Twitdon.Properties.Resources.reblog_before;
            this.pictureReblog.Location = new System.Drawing.Point(157, 110);
            this.pictureReblog.Name = "pictureReblog";
            this.pictureReblog.Size = new System.Drawing.Size(16, 16);
            this.pictureReblog.TabIndex = 4;
            this.pictureReblog.TabStop = false;
            // 
            // pictureReply
            // 
            this.pictureReply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureReply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureReply.Image = global::Twitdon.Properties.Resources.reply;
            this.pictureReply.Location = new System.Drawing.Point(135, 110);
            this.pictureReply.Name = "pictureReply";
            this.pictureReply.Size = new System.Drawing.Size(16, 16);
            this.pictureReply.TabIndex = 2;
            this.pictureReply.TabStop = false;
            // 
            // TimeLineStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureDelete);
            this.Controls.Add(this.labelContent);
            this.Controls.Add(this.pictureFavorite);
            this.Controls.Add(this.pictureReblog);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.pictureReply);
            this.Controls.Add(this.labelUser);
            this.Name = "TimeLineStatus";
            this.Size = new System.Drawing.Size(220, 129);
            ((System.ComponentModel.ISupportInitialize)(this.pictureDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFavorite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureReblog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureReply)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.PictureBox pictureReply;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.PictureBox pictureReblog;
        private System.Windows.Forms.PictureBox pictureFavorite;
        private System.Windows.Forms.Label labelContent;
        private System.Windows.Forms.PictureBox pictureDelete;
    }
}
