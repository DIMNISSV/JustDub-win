using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JustDub
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Posts posts;
        private Comment comment = new Comment();
        public MainWindow()
        {
            InitializeComponent();
            searchEdt.BorderBrush = Sets.Colors[1];
            FontSize = Sets.FontSize;
            comment.GenAll();
            posts = new Posts();
            posts.comments = comment.comments;
            posts.data = API.Get(Sets.Urls[0]);
            posts.xfs_sp = xfs_sp;
            posts.players = players;
            posts.wplist = list;
            posts.dshow_post = show_post;
            posts.sp_comments = commentsBlock;
            posts.wplist.Visibility = Visibility.Collapsed;
            posts.dshow_post.Visibility = Visibility.Collapsed;
            posts.Load();
            Search.SearchPanel = posts.wplist;
            posts.wplist.Visibility = Visibility.Visible;
        }
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            posts.wplist.Visibility = Visibility.Visible;
            posts.dshow_post.Visibility = Visibility.Collapsed;
        }
        private void searchEdt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (posts.wplist.Visibility != Visibility.Collapsed && searchEdt.Text != "" && searchEdt.Text.Length > 3)
                Search.q = searchEdt.Text;
            if (searchEdt.Text == "")
                Search.q = "";
        }
        private void TimeTable_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TimeTable timeTable = new TimeTable();
            timeTable.posts = posts;
            timeTable.Show();
        }

        private void SendCom_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Авторизуйтесь чтобы оставлять комментарии.\nХм.. точно, в этой версии нет авторизации.\nПопробуйте позже.");
        }

        private void Favorites_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Авторизуйтесь чтобы просмотреть закладки.\nХм.. точно, в этой версии нет авторизации.\nПопробуйте позже.");
        }
        private void Menu_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ContextMenu cm = new ContextMenu();
            MenuItem outI = new MenuItem() { Header = "Выход" };
            outI.Click += (s, ev) =>
            {
                Application.Current.Shutdown();
            };
            MenuItem aboutI = new MenuItem() { Header = "О программе" };
            aboutI.Click += (s, ev) =>
            {
                MessageBox.Show(
                    "Программа для просмотра постов с сайта justdub.ru." +
                    "\nВерсия: " + Sets.Version +
                    "\nАвтор: dimnissv" +
                    "\nПри любых вопросах обращайтесь в вк: vk.com/dimnissv"
                    );
            };
            MenuItem checkVersI = new MenuItem() { Header = "Проверить обновления" };
            checkVersI.Click += (s, ev) =>
            {
                var last = API.Get(Sets.Urls[7]);
                if (last != Sets.Version)
                {
                    MessageBoxResult result = MessageBox.Show("Доступна более новая версия. Скачать её?", "Доступно обновление!", MessageBoxButton.YesNoCancel);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            string path = Environment.CurrentDirectory + @"\update.exe";
                            DownloadProgress dWin = new DownloadProgress();
                            dWin.Show();
                            new Thread(() =>
                            {
                                var t = string.Format(Sets.Urls[8], last);
                                API.Download(t, path, dWin);

                                Process.Start(path);
                                // закрываем приложение
                                dWin.Dispatcher.Invoke(() =>
                                {
                                    dWin.Close();
                                    Application.Current.Shutdown();
                                });
                            }).Start();
                            break;
                        case MessageBoxResult.No:
                            break;
                        case MessageBoxResult.Cancel:
                            break;
                    }
                }
                else
                    MessageBox.Show("Программа уже обновлена.");
            };
            cm.Items.Add(aboutI);
            cm.Items.Add(checkVersI);
            cm.Items.Add(outI);
            
            cm.PlacementTarget = sender as Image;
            cm.IsOpen = true;
        }
    }
}
