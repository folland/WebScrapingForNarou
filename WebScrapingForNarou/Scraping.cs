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
using Newtonsoft.Json.Linq;
using System.Collections;

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

        //小説家になろうAPIについてのページ
        //https://dev.syosetu.com/man/api/

        //ofパラメータ
        //of=t-n など
        //t:title	小説名
        //n:ncode	Nコード
        //w:writer	作者名
        //ga:general_all_no	全掲載部分数です。短編の場合は1です。
        //l:length	小説文字数です。スペースや改行は文字数としてカウントしません。
        //ti:time	読了時間(分単位)です。読了時間は小説文字数÷500を切り上げした数値です。

        //outパラメータ
        //out=json JSON形式にする

        /// <summary>
        /// Nコード(書籍コード)から小説情報APIURLを取得
        /// </summary>
        /// <param name="ncode">Nコード(書籍コード)</param>
        /// <returns>小説情報APIURL</returns>
        private static string GetApiUrl(string ncode)
        {
            return "https://api.syosetu.com/novelapi/api/?out=json&of=t-n-w-ga-l-ti&ncode=" + ncode;
        }

        /// <summary>
        /// APIURLにアクセスした際に取得できるAPIDataを返します。
        /// </summary>
        /// <param name="apiUrl">APIURL</param>
        /// <returns>取得したAPIData</returns>
        public JArray GetApiData(string apiUrl)
        {

            WebClient wc = new WebClient();
            wc.Headers.Add("user-agent", "Get Narou Info");
            JArray apiData = new JArray();
            try
            {
                string data = wc.DownloadString(apiUrl);

                //まずはバイト配列に変換する
                byte[] bytes = Encoding.UTF8.GetBytes(data);

                //バイト配列をUTF8の文字コードとしてStringに変換する
                string stringUtf8 = System.Text.Encoding.UTF8.GetString(bytes);

                // デシリアライズ
                apiData = JArray.Parse(stringUtf8);
            }
            catch (WebException exc)
            {
                apiData.Add("error : " + exc.Message);
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
            JArray apiData = new JArray();
            if (nCode == "")
            {
                apiData.Add("error : 書籍コードエラー");
            }
            else
            {
                apiData = GetApiData(apiUrl);
            }
            string infoStory = "";
            JToken jDataLast = apiData.Last;
            JTokenType jTypeLast = jDataLast.Type;
            if (apiData.Count >= 2 && jTypeLast != JTokenType.String)
            {
                string title      = (string)apiData.Last["title"];
                string writer     = (string)apiData.Last["writer"];
                string numOfStory = (string)apiData.Last["general_all_no"];
                string numOfWord  = (string)apiData.Last["length"];
                string time       = (string)apiData.Last["time"];
                //書籍コード, 作者名 , 小説名  , 話数 , 文字数 , 読了時間
                infoStory = nCode + "\t" + writer + "\t" + title + "\t" + numOfStory + "\t" + numOfWord + "\t" + time + "\r\n";
            }
            else
            {
                infoStory = (string)apiData.ToString();
            }
            return infoStory;
        }
    }

}
