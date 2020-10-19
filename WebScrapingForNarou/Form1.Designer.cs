namespace WebScrapingForNarou
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            this.StartButton = new System.Windows.Forms.Button();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.URL = new System.Windows.Forms.Label();
            this.MainLabel = new System.Windows.Forms.Label();
            this.textBoxMain = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.StartButton.Location = new System.Drawing.Point(657, 57);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(131, 32);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "START";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.textBoxUrl.Location = new System.Drawing.Point(12, 28);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(776, 23);
            this.textBoxUrl.TabIndex = 1;
            // 
            // URL
            // 
            this.URL.AutoSize = true;
            this.URL.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.URL.Location = new System.Drawing.Point(12, 9);
            this.URL.Name = "URL";
            this.URL.Size = new System.Drawing.Size(37, 16);
            this.URL.TabIndex = 2;
            this.URL.Text = "URL";
            // 
            // MainLabel
            // 
            this.MainLabel.AutoSize = true;
            this.MainLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.MainLabel.Location = new System.Drawing.Point(12, 93);
            this.MainLabel.Name = "MainLabel";
            this.MainLabel.Size = new System.Drawing.Size(72, 16);
            this.MainLabel.TabIndex = 3;
            this.MainLabel.Text = "取得内容";
            // 
            // textBoxMain
            // 
            this.textBoxMain.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.textBoxMain.Location = new System.Drawing.Point(12, 112);
            this.textBoxMain.Multiline = true;
            this.textBoxMain.Name = "textBoxMain";
            this.textBoxMain.Size = new System.Drawing.Size(776, 326);
            this.textBoxMain.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBoxMain);
            this.Controls.Add(this.MainLabel);
            this.Controls.Add(this.URL);
            this.Controls.Add(this.textBoxUrl);
            this.Controls.Add(this.StartButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Label URL;
        private System.Windows.Forms.Label MainLabel;
        private System.Windows.Forms.TextBox textBoxMain;
    }
}

