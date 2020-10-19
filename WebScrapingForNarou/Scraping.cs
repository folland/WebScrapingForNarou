using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace WebScrapingForNarou
{
    public class Scraping
    {

        /// <summary>
        /// URLからNコード(書籍コード)までのスラッシュ / の数
        /// </summary>
        private static int SlCountOfNcode = 3;

        /// <summary>
        /// 引数urlからNコード(書籍コード)を取得
        /// </summary>
        /// <param name="url">URL(アドレス)</param>
        /// <returns>Nコード(書籍コード)</returns>
        public string GetNCode(string url)
        {
            string ncode = "";
            char s;
            int slCount = 0;
            for (int i = 0; i < url.Length; i++)
            {
                s = url[i];
                if (s == '/')
                {
                    slCount += 1;
                }
                if ((slCount == SlCountOfNcode) && (s != '/'))
                {
                    ncode += s;
                }
            }

            return ncode;
        }

        ///// <summary>
        ///// Nコード(書籍コード)から小説情報URLを取得
        ///// </summary>
        ///// <param name="ncode">Nコード(書籍コード)</param>
        ///// <returns>URL(アドレス)</returns>
        //private static string GetInfoUrl(string ncode)
        //{
        //    return "https://ncode.syosetu.com/novelview/infotop/ncode/" + ncode + "/";
        //}

        ///// <summary>
        ///// 引数urlにアクセスした際に取得できるHTMLを返します。
        ///// </summary>
        ///// <param name="url">URL(アドレス)</param>
        ///// <returns>取得したHTML</returns>
        //public string GetHtml(string url)
        //{
        //    //// 指定されたURLに対してのRequestを作成します。
        //    //var req = (HttpWebRequest)WebRequest.Create(url);

        //    //// html取得文字列
        //    //string html = "";

        //    //// 指定したURLに対してReqestを投げてResponseを取得します。
        //    //using (var res = (HttpWebResponse)req.GetResponse())
        //    //using (var resStream = res.GetResponseStream())
        //    //// 取得した文字列をUTF8でエンコードします。
        //    //using (var sr = new StreamReader(resStream, Encoding.UTF8))
        //    //{
        //    //    // HTMLを取得する。
        //    //    html = sr.ReadToEnd();
        //    //}

        //    WebClient wc = new WebClient();
        //    string html = "";
        //    try
        //    {
        //        html = wc.DownloadString(url);
        //    }
        //    catch (WebException exc)
        //    {
        //        html = exc.Message;
        //    }

        //    return html;
        //}

        ///// <summary>
        ///// 正規化表現を使用してHTMLからタイトルを取得します。
        ///// </summary>
        ///// <param name="html">HTML文字列</param>
        ///// <returns>HTML文字列から取得したタイトル</returns>
        //public string GetTitle(string html)
        //{

        //    // 正規化表現
        //    // 大文字小文字区別なし       : RegexOptions.IgnoreCase
        //    // 「.」を改行にも適応する設定: RegexOptions.Singleline
        //    // ?<title>により"title"というグループ名をつけている
        //    // .*?により任意の文字の並びを表している
        //    var reg = new Regex(@"<title>(?<title>.*?)</title>",
        //                  RegexOptions.IgnoreCase | RegexOptions.Singleline);

        //    // html文字列内から条件にマッチしたデータを抜き取ります。
        //    var m = reg.Match(html);

        //    // 条件にマッチした文字列内からKey("title")にマッチした値を抜き取ります。
        //    return m.Groups["title"].Value;

        //}

        ///// <summary>
        ///// 正規化表現を使用してHTMLから話数を取得します。
        ///// </summary>
        ///// <param name="html">HTML文字列</param>
        ///// <returns>話数</returns>
        //public string GetNumOfStory(string html)
        //{

        //    // 正規化表現
        //    // 大文字小文字区別なし       : RegexOptions.IgnoreCase
        //    // 「.」を改行にも適応する設定: RegexOptions.Singleline
        //    var reg = new Regex(@"全(?<numOfStory>.*?)部分");

        //    // html文字列内から条件にマッチしたデータを抜き取ります。
        //    var m = reg.Match(html);

        //    // 条件にマッチした文字列内からKey("numOfStory")にマッチした値を抜き取ります。
        //    return m.Groups["numOfStory"].Value;

        //}

        ///// <summary>
        ///// 正規化表現を使用してHTMLから文字数を取得します。
        ///// </summary>
        ///// <param name="html">HTML文字列</param>
        ///// <returns>文字数</returns>
        //public string GetNumOfWord(string html)
        //{

        //    // 正規化表現
        //    // 大文字小文字区別なし       : RegexOptions.IgnoreCase
        //    // 「.」を改行にも適応する設定: RegexOptions.Singleline
        //    // ?<title>により"title"というグループ名をつけている
        //    // .*?により任意の文字の並びを表している
        //    var reg = new Regex(@"<td>(?<numOfWord>.*?文字)</td>",
        //                  RegexOptions.IgnoreCase | RegexOptions.Singleline);

        //    // html文字列内から条件にマッチしたデータを抜き取ります。
        //    var m = reg.Match(html);

        //    // 条件にマッチした文字列内からKey("numOfWord")にマッチした値を抜き取ります。
        //    return m.Groups["numOfWord"].Value;

        //}


        //小説家になろうAPIについてのページ
        //https://dev.syosetu.com/man/api/

        //t:title	小説名
        //n:ncode	Nコード
        //w:writer	作者名
        //ga:general_all_no	全掲載部分数です。短編の場合は1です。
        //l:length	小説文字数です。スペースや改行は文字数としてカウントしません。
        //ti:time	読了時間(分単位)です。読了時間は小説文字数÷500を切り上げした数値です。

        /// <summary>
        /// Nコード(書籍コード)から小説情報APIURLを取得
        /// </summary>
        /// <param name="ncode">Nコード(書籍コード)</param>
        /// <returns>小説情報APIURL</returns>
        private static string GetApiUrl(string ncode)
        {
            return "https://api.syosetu.com/novelapi/api/?of=t-n-w-ga-l-ti&ncode=" + ncode;
        }

        /// <summary>
        /// APIURLにアクセスした際に取得できるAPIDataを返します。
        /// </summary>
        /// <param name="apiUrl">APIURL</param>
        /// <returns>取得したAPIData</returns>
        public APIDataNarou GetApiData(string apiUrl)
        {

            WebClient wc = new WebClient();
            wc.Headers.Add("user-agent", "Get Narou Info");
            APIDataNarou apiData = new APIDataNarou();
            try
            {
                string data = wc.DownloadString(apiUrl);

                //参考URL : https://www.atmarkit.co.jp/ait/articles/1709/13/news018.html
                string output = JsonConvert.SerializeObject(data);
                apiData = JsonConvert.DeserializeObject<APIDataNarou>(output);
            }
            catch (WebException exc)
            {
                apiData.Comment = exc.Message;
            }

            return apiData;
            
        }

        /// <summary>
        /// URLから小説情報を取得して返します
        /// </summary>
        /// <param name="URL">URL</param>
        /// <returns>小説情報(書籍コード, 作者名 , 小説名  , 話数 , 文字数 , 読了時間)</returns>
        public string GetInfoStory(string url)
        {

            string nCode = GetNCode(url);
            string apiUrl = GetApiUrl(nCode);
            APIDataNarou apiData = GetApiData(apiUrl);
            string infoStory = "";
            if (apiData.Comment == null)
            {
                string title = apiData.Data["t"];
                string writer = apiData.Data["w"];
                string numOfStory = apiData.Data["ga"];
                string numOfWord = apiData.Data["l"];
                string time = apiData.Data["ti"];
                //書籍コード, 作者名 , 小説名  , 話数 , 文字数 , 読了時間
                infoStory = nCode + "\t" + writer + "\t" + title + "\t" + numOfStory + "\t" + numOfWord + "\t" + time;
            }
            else
            {
                infoStory = apiData.Comment;
            }
            return infoStory;
        }
    }

    public class APIDataNarou
    {
        // 先のSampleDataと比べて、DescriptionとUpdateDateがない
        public Dictionary<string, string> Data { get; set; }
        // Commentが余分にある
        public string Comment { get; set; }
    }

}
