using System;
using System.Collections.Generic;
using System.IO;
using Tweetinvi;
using Tweetinvi.Models;
using System.Threading.Tasks;
using Tweetinvi.Parameters;
using TweetSharp;
using System.Text;
using System.Linq;
using System.Timers;
using System.Net;

namespace photo
{
    class Program
    {
        private static string apiKey = "x5WGo8u2JfKR2UEXYoWeU9Y7H";
        private static string apiSecret = "N2cibKo1tmGo5rgSvNnHE7V8TZQUFeuUFa5gwFBuhbPkzdSD4s";
        private static string accessToken = "405517463-J0hDBvO4s0K632Fss5tWzLmjA4hbXo6WqUzTDkG6";
        private static string accessTokenSecret = "j9ALpZ1NS0NnZ3JzlLjRLo2d0TyTVLPaNpIZIvwCSTOKi";

        private static TwitterService service = new TwitterService(apiKey, apiSecret, accessToken, accessTokenSecret);
        private static int currentImageID = 0;
        private static List<string> imageList = new List<string> { $"C:/Users/Kamogelo.Ramantsima/source/repos/photo/photo/bin/Debug/netcoreapp3.1/pictures/happy-new-year-2023.jpg" };
        static void Main(string[] args)
        {
            string path = @"C:\Users\Kamogelo.Ramantsima\source\repos\photo\photo\bin\Debug\netcoreapp3.1\task.txt";

            using(StreamWriter writer = new StreamWriter(path, true))
             {
                writer.WriteLine($"{DateTime.Now.ToString()} - task running");
            }

            Random randNum = new Random();
            string[] newYear = { "Gelukkige nuwejaar","uJabulela unyaka omusha","Mahlohonolo a selemo se secha","Nyak'omtsha","Happy New Year","Unyaka omusha omuhle","Mahlohonolo a selemo se secha","Itumelele ngwaga o mosha","Ngumnyaka lomusha",
                                 "Ntsako wa lembe lerintshwa","Nwaha muswa wa vhudi"};


            Console.WriteLine($"{DateTime.Now} - Bot Started");
            SendMediaTweet(newYear[randNum.Next(11)] + $"\n\n Bot Started - { DateTime.Now} ",currentImageID);

            Console.Read();

        }
        private static void SendTweet(String _status) {

            service.SendTweet(new SendTweetOptions { Status = _status }, (tweet, response) =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"<{DateTime.Now}> - Tweet Sent!");
                    Console.ResetColor();
                }
                else {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"<ERROR> "+response.Error.Message);
                    Console.ResetColor();
                }
            });
        
        }
        private static void SendMediaTweet(string _status, int imageID) {
            using (var stream = new FileStream(imageList[imageID], FileMode.Open)) {
                service.SendTweetWithMedia(new SendTweetWithMediaOptions
                {
                    Status = _status,
                    Images= new Dictionary<string, Stream> { { imageList[imageID],stream} }

                });
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"<{DateTime.Now}> - Tweet Sent!");
                Console.ResetColor();

                if ((currentImageID + 1) == imageList.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("<BOT> -End of image array");
                    Console.ResetColor();
                    currentImageID = 0;

                }
                else {
                    currentImageID++;
    }
}

                }
            }
        
        }