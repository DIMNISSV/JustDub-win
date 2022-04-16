using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace JustDub
{
    class Data
    {
        public static bool IsIn(string str, string word)
        {
            string tmp = str.Replace(word, "");
            if (tmp == str)
                return false;
            else
                return true;
        }
        public static void PlayVlc(string url)
        {
            if (Properties.Settings.Default.VLCPath != "" & File.Exists(Properties.Settings.Default.VLCPath))
            {
                Process.Start(Properties.Settings.Default.VLCPath, url);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Мы используем VLC плеер.\nЕсли он у вас есть - вы можете выбрать до него путь. (Да)\nА если нет, мы быстренько скачаем урезаную версию. (Нет)\nИли можете не делать этого и посмотреть в другом плеере... (Отмена)\nНе волнуйтесь эту операцию надо проделать всего разочек", "У вас есть VLC?", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                        dialog.Filter = "Исполняемые файлы (*.exe)|*.exe";
                        dialog.FilterIndex = 2;

                        bool? res = dialog.ShowDialog();

                        if (res == true)
                        {
                            Properties.Settings.Default.VLCPath = dialog.FileName;
                            PlayVlc(url);
                        }
                        break;
                    case MessageBoxResult.No:
                        string path = Environment.CurrentDirectory + @"\vlc.zip";
                        DownloadProgress dWin = new DownloadProgress();
                        dWin.Show();
                        new Thread(() =>
                        {
                            API.Download(Sets.Urls[4], path, dWin);
                            ZipFile.ExtractToDirectory(path, Environment.CurrentDirectory);
                            File.Delete(path);
                            Properties.Settings.Default.VLCPath = Environment.CurrentDirectory + @"\vlc\vlc.exe";
                            Properties.Settings.Default.Save();
                            dWin.Dispatcher.Invoke(() =>
                            {
                                dWin.Close();
                                PlayVlc(url);
                            });
                        }).Start();
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
        }
        internal static string RepairHtmlCharacters(string str)
        {
            str = str.Replace("&#39;", "'");
            str = str.Replace("&#58;", ":");
            str = str.Replace("&quot;", "\"").Replace("\\\"", "\"");
            str = str.Replace("&amp;", "&");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&nbsp;", " ");
            return str;
        }
    }
}
