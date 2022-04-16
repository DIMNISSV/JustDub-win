using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JustDub
{
    /// <summary>
    /// Логика взаимодействия для TimeTable.xaml
    /// </summary>
    public partial class TimeTable : Window
    {
        internal Posts posts;
        internal StackPanel[] days = new StackPanel[7];
        public TimeTable()
        {
            InitializeComponent();
            posts = new Posts();
            // цикл по дням недели и заполнение их в массив days StackPanel
            for (byte i = 0; i < 7; i++)
            {
                posts.data = API.Get(Sets.Urls[5] + (8+i).ToString());
                WrapPanel wrap = new WrapPanel();
                wrap.Orientation = Orientation.Horizontal;
                wrap.Margin = new Thickness(2);

                posts.wplist = wrap;
                posts.Load();
                
                TextBlock title = new TextBlock() { Text = PostFields.weekDays[i], FontSize = Sets.FontSize, FontWeight = FontWeights.Bold };
                days[i] = new StackPanel();
                days[i].Orientation = Orientation.Vertical;
                days[i].Margin = new Thickness(5);
                days[i].HorizontalAlignment = HorizontalAlignment.Left;
                days[i].VerticalAlignment = VerticalAlignment.Top;
                days[i].Height = 300;
                days[i].Children.Add(title);
                days[i].Children.Add(wrap);
                main.Children.Add(days[i]);
            }
        }
    }
}
