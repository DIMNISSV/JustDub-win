using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JustDub
{
    internal class Search
    {
        internal static WrapPanel SearchPanel { get; set; }
        internal static string q { get { return q; } set { searchFor(value, SearchPanel); } }

        private static void searchFor(string search, WrapPanel list)
        {
            foreach (StackPanel item in list.Children)
            {
                if (item.Tag.ToString().ToLower().Contains(search.ToLower()))
                {
                    item.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    item.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }
    }
}
