using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows;

namespace JustDub
{
    class API
    {
        internal static string Get(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
        internal static void Download(string url, string path, DownloadProgress dWin = null)
        {
            try
            {
                byte[] buf = new byte[4000];
                int bytesComplete = 0;

                WebRequest request = WebRequest.Create(url);
                if (request != null)
                {
                    WebRequest sizeRequest = WebRequest.Create(url);
                    sizeRequest.Method = "HEAD";
                    var c = sizeRequest.GetResponse().Headers.Get("Content-Length");
                    int totalByts = int.Parse(c);
                    if (dWin != null)
                    {
                        
                        dWin.Dispatcher.Invoke(() => {
                            dWin.SetMaximum(totalByts);
                            dWin.Title = "Скачивание...";
                        });
                    }


                    using (WebResponse response = request.GetResponse())
                    {
                        if (response != null)
                        {
                            Stream responseStream = response.GetResponseStream();
                            using (Stream fileStream = File.Create(path))
                            {
                                int bytes;
                                do
                                {
                                    bytes = responseStream.Read(buf, 0, buf.Length);
                                    bytesComplete += bytes;
                                    fileStream.Write(buf, 0, bytes);
                                    if (dWin != null)
                                        dWin.Dispatcher.Invoke(() => dWin.SetProgress(bytesComplete));
                                } 
                                while (bytes > 0);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}

