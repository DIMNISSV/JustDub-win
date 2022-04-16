using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JustDub
{
    internal class Posts
    {
        internal string data;
        internal Comment[] comments;
        internal WrapPanel wplist = new WrapPanel();
        internal DockPanel dshow_post = new DockPanel();
        internal StackPanel xfs_sp;
        internal StackPanel players;
        internal StackPanel sp_comments;
        public void Load()
        {
            List<Post> posts = Post.DeJson(data);
            foreach (Post post in posts)
            {
                StackPanel ssBlock = new StackPanel(); 
                Image poster = new Image();
                TextBlock title = new TextBlock();
                Post_View post_View = new Post_View();
                post_View.post = post;
                post_View.xfs = post.GetXfs();

                poster.MaxHeight = 250;
                poster.Source = post_View.GetPoster();
                poster.Margin = new Thickness(1);
                title.Text = post.title;
                title.HorizontalAlignment = HorizontalAlignment.Center;
                title.TextWrapping = TextWrapping.NoWrap;
                ssBlock.Name = "p" + post.id;
                ssBlock.Width = 180;
                ssBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
                ssBlock.MouseEnter += StackPanel_MouseEnter;
                ssBlock.MouseLeave += StackPanel_MouseLeave;
                ssBlock.MouseUp += StackPanel_MouseUp;
                ssBlock.ToolTip = new ToolTip() { Content = new TextBlock() { Text = title.Text } };
                ssBlock.Tag = title.Text;
                foreach (string x in post_View.xfs.Keys)
                {
                    ssBlock.Tag += "§" + x + "|" + post_View.xfs[x];
                }
                ssBlock.Children.Add(poster);
                ssBlock.Children.Add(title);
                wplist.Children.Add(ssBlock);
            }
        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is StackPanel sp)
            {
                sp.Background = Sets.Colors[1];
            }
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is StackPanel sp)
            {
                sp.Background = Sets.Colors[0];
            }
        }
        private void StackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is StackPanel sp)
            {
                wplist.Visibility = Visibility.Collapsed;
                dshow_post.Visibility = Visibility.Visible;
                Post post = Post.DeJson(API.Get(Sets.Urls[1] + sp.Name.Replace("p", "")))[0];
                Post_View post_View = new Post_View();
                post_View.post = post;
                post_View.xfs = post.GetXfs();
                xfs_sp.Children.Clear();
                players.Children.Clear();
                StackPanel panel;
                post_View.comments = Comment.Get(comments, post.id).ToArray();
                post_View.GetBlocks().TryGetValue("xfs", out panel);
                xfs_sp.Children.Add(panel);
                post_View.GetBlocks().TryGetValue("pls", out panel);
                players.Children.Add(panel);
                post_View.GetBlocks().TryGetValue("desc", out panel);
                players.Children.Add(panel);
                post_View.GetBlocks().TryGetValue("coms", out panel);
                sp_comments.Children.Add(panel);
            }
        }

    }
}
