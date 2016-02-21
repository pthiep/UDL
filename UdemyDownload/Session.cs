using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using HtmlAgilityPack;

namespace UdemyDownload
{
    public class Session
    {
        public string test;

        private string User;
        private string Pass;

        private string LOGIN_URL = "https://www.udemy.com/join/login-popup/?displayType=ajax&display_type=popup&showSkipButton=1&returnUrlAfterLogin=https%3A%2F%2Fwww.udemy.com%2F&next=https%3A%2F%2Fwww.udemy.com%2F&locale=en_US";
        private string LOGIN_GET = "https://www.udemy.com/join/login-popup";

        private string useragent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";

        CookieContainer cookieContainer = new CookieContainer();

        public Session(string user, string pass)
        {
            User = user;
            Pass = pass;
        }

        public string get_csrf_token()
        {
            var request = (HttpWebRequest)WebRequest.Create(LOGIN_GET);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responseString);
            return htmlDoc.DocumentNode.SelectSingleNode("//input[@type='hidden' and @name='csrfmiddlewaretoken']").Attributes["value"]
                    .Value;
        }

        public void Login()
        {
            string csrf_token = get_csrf_token();
            string postData = "isSubmitted=1&email=" + User + "&password=" + Pass + "&dislayType=ajax&csrfmiddlewaretoken=" + csrf_token;
            
            var request = (HttpWebRequest)WebRequest.Create(LOGIN_URL);
            
            var data = Encoding.ASCII.GetBytes(postData);

            test = postData.ToString();

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = useragent;
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.Referer = "https://www.udemy.com/join/login-popup";
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = false;
            request.UseDefaultCredentials = true;
            request.ContentLength = data.Length;

            request.CookieContainer = new CookieContainer();

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }


            //request.CookieContainer.GetCookies("access_token");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                                    
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }
}
