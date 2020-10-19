using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebScrapingForNarou
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 開始ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            // 今回サンプルで作成を行ったクラスをインスタンス化します。
            var scr = new Scraping();

            // 画面上からHTMLを取得するサイトのURLを取得します。
            string url = textBoxUrl.Text;

            // htmlを取得するメソッドを実行し、画面描画します。
            string infoStory = scr.GetInfoStory(url);
            textBoxMain.Text += infoStory;
        }
    }
}
