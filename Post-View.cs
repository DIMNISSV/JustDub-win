using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JustDub
{
    class Post_View
    {
        public Post post;
        public Comment[] comments;
        public Dictionary<string, string> xfs;
        internal Dictionary<string, StackPanel> GetBlocks()
        {
            Dictionary<string, StackPanel> result = new Dictionary<string, StackPanel>();
            Dictionary<string, string> xfs = post.GetXfs();
            result.Add("xfs", GetXfsBlock());
            result.Add("pls", GenPlsBtn());
            result.Add("desc", GenDescBlock());
            result.Add("coms", GenCommentsBlock());
            return result;
        }

        private StackPanel GenCommentsBlock()
        {
            StackPanel result = new StackPanel();
            result.Orientation = Orientation.Vertical;
            if (comments == null)
                return result;
            foreach (Comment c in comments)
            {
                StackPanel comment = new StackPanel();
                comment.Orientation = Orientation.Vertical;
                TextBlock author = new TextBlock();
                author.Text = c.autor;
                author.FontWeight = FontWeights.Bold;
                TextBlock text = new TextBlock();
                text.Text = Data.RepairHtmlCharacters(c.text);
                text.TextWrapping = TextWrapping.Wrap;
                comment.Children.Add(author);
                comment.Children.Add(text);
                result.Children.Add(comment);
            }
            return result;
        }

        private StackPanel GenDescBlock()
        {
            StackPanel sp = new StackPanel();
            TextBlock desc = new TextBlock();
            desc.Text = Data.RepairHtmlCharacters(post.full_story);
            desc.TextWrapping = TextWrapping.Wrap;
            desc.TextAlignment = TextAlignment.Left;
            desc.FontSize = Sets.FontSize;
            sp.Orientation = Orientation.Vertical;
            sp.Children.Add(GenTitle());
            sp.Children.Add(desc);
            return sp;
        }

        private TextBlock GenTitle()
        {
            TextBlock tb = new TextBlock();
            tb.Text = Data.RepairHtmlCharacters(post.title);
            tb.FontSize = Sets.FontSize + 4;
            tb.FontWeight = FontWeights.Bold;
            return tb;
        }

        private StackPanel GenPlsBtn()
        {
            StackPanel plsBtns = new StackPanel();
            foreach (KeyValuePair<string, string> pl in PostFields.Players)
            {
                if (xfs.TryGetValue(pl.Key, out string url))
                {
                    if (Data.IsIn(url, "vid.justdub.ru"))
                    {
                        plsBtns.Children.Add(GetPlayer(url));
                        continue;
                    }
                    Button btn = new Button();
                    btn.Click += delegate {
                        ShowPlayer(url);
                    };
                    btn.Content = pl.Value;
                    plsBtns.Children.Add(btn);
                }
            }
            return plsBtns;
        }
        internal ImageSource GetPoster()
        {
            string data = post.GetXf("poster");
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            if (data != "")
                data = "https://justdub.ru/uploads/posts/" + data;
            else
                data = "https://justdub.ru/templates/JustDub/images/no_poster.png";
            bi.UriCachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.CacheIfAvailable);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.UriSource = new Uri(data);
            bi.DecodePixelWidth = 250;
            bi.EndInit();
            return bi;
        }
        private StackPanel GetXfsBlock()
        {
            StackPanel sp = new StackPanel();
            Image poster = new Image();
            poster.MaxHeight = 270;
            poster.Source = GetPoster();
            sp.Children.Add(poster);
            foreach (var xf in xfs)
            {
                string name = xf.Key;
                string val = xf.Value;
                string tmp;
                if (PostFields.Xfields.TryGetValue(xf.Key, out tmp))
                {
                    TextBlock text = new TextBlock();
                    name = tmp;
                    name = Data.RepairHtmlCharacters(name);
                    val = Data.RepairHtmlCharacters(val);
                    if (xf.Value.Length >= 80)
                    {
                        text.MouseUp += delegate { MessageBox.Show(val); };
                        val = "Показать";
                    }
                    text.Text += name + ": " + val;
                    sp.Children.Add(text);
                }
            }
            return sp;
        }
        internal StackPanel GetPlayer(string url)
        {
            Link files = new Link();
            files.SetId(url);
            files.DeJson();
            Dictionary<string, Dictionary<string, string>> links = files.GetDict();
            StackPanel playlist = new StackPanel();
            foreach (KeyValuePair<string, Dictionary<string, string>> link in links)
            {
                StackPanel epBlock = new StackPanel();
                TextBlock title = new TextBlock();
                StackPanel buttons = new StackPanel();

                title.Text = "Серия " + link.Key;
                foreach (KeyValuePair<string, string> ql in link.Value)
                {
                    Button quality = new Button();
                    quality.Content = ql.Key;
                    quality.Name = "e" + link.Key + "q" + ql.Key;
                    quality.Margin = new Thickness(5,0,5,0);

                    quality.Click += delegate (object s, RoutedEventArgs e)
                    {
                        if (s is Button ep)
                        {
                            var tmp = ep.Name.Replace("e", "").Split('q');
                            links.TryGetValue(tmp[0], out Dictionary<string, string> tmp1);
                            tmp1.TryGetValue(tmp[1], out url);
                            Data.PlayVlc(url);
                        }
                    };
                    buttons.Children.Add(quality);
                }
                buttons.Orientation = Orientation.Horizontal;
                epBlock.Orientation = Orientation.Horizontal;
                epBlock.Margin = new Thickness(5);
                epBlock.Children.Add(title);
                epBlock.Children.Add(buttons);
                playlist.Children.Add(epBlock);
            }
            return playlist;
        }
        internal void ShowWebPlayer(string url)
        {
            Process.Start("https://vid.justdub.ru/player/to_ifr.php?url=" + url);
        }
        internal void ShowPlayer(string url)
        {
            if (Data.IsIn(url, "magnet"))
                Process.Start(url);
            else
                ShowWebPlayer(url);
        }

    }
}
