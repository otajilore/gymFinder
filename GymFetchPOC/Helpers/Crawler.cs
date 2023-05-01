using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace GymFetchPOC.Helpers
{
    /// <summary>
    /// Crawl through a web page and return all content on the page as a string.
    /// </summary>
    public class Crawler
    {

        public string crawlWebPage (string URL)
        {
            WebResponse myWebResponse;

            try
            {
                var myWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                myWebRequest.Method = "GET";
                myWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; WOW64; " +
                "Trident/4.0; SLCC1; .NET CLR 2.0.50727; Media Center PC 5.0; " +
                ".NET CLR 3.5.21022; .NET CLR 3.5.30729; .NET CLR 3.0.30618; " +
                "InfoPath.2; OfficeLiveConnector.1.3; OfficeLivePatch.0.0)";
                myWebResponse = myWebRequest.GetResponse();//Returns a response from an Internet resource

                Stream streamResponse = myWebResponse.GetResponseStream();//return the data stream from the internet
                                                                          //and save it in the stream

                StreamReader sreader = new StreamReader(streamResponse);//reads the data stream
                string content = sreader.ReadToEnd();
                return content + GetNewLinks(content);//reads it to the end
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return " ";
            }

        }

        private string GetNewLinks(string content)
        {
            Regex regexLink = new Regex("(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))");

            ISet<string> newLinks = new HashSet<string>();
            foreach (var match in regexLink.Matches(content))
            {
                if (!newLinks.Contains(match.ToString()) && checkUrl(match.ToString()))
                    newLinks.Add(match.ToString());
            }

            string s = " ";

            Parallel.ForEach(newLinks, (link) =>
            {
                s = s + readLinks(link);
            });

            return s;
        }

        private string readLinks (string URL)
        {
            WebRequest myWebRequest;
            WebResponse myWebResponse;

            try
            {
                myWebRequest = WebRequest.Create(URL);
                myWebResponse = myWebRequest.GetResponse();//Returns a response from an Internet resource

                Stream streamResponse = myWebResponse.GetResponseStream();//return the data stream from the internet
                                                                          //and save it in the stream

                StreamReader sreader = new StreamReader(streamResponse);//reads the data stream
                string content = sreader.ReadToEnd();
                return content;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return " ";
            }   
        }

        private bool checkUrl (string URL)
        {
            bool answer = false;

            if (URL.ToLower().Contains("http"))
            {
                answer = true;
            }

            if (URL.ToLower().Contains("https"))
            {
                answer = true;
            }

            if (URL.ToLower().Contains("facebook")
                || URL.ToLower().Contains("twitter")
                || URL.ToLower().Contains("yelp")
                || URL.ToLower().Contains("google")
                || URL.ToLower().Contains("apple")
                || URL.ToLower().Contains("blog")
                || URL.ToLower().Contains("linkedin")
                || URL.ToLower().Contains("youtube")
                || URL.ToLower().Contains("bing")
                || URL.ToLower().Contains("pinterest")
                || URL.ToLower().Contains("instagram"))
            {
                answer = false;
            }

            return answer;
        }
    }
}
