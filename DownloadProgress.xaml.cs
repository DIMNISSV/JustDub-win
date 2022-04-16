using System.Windows;
using System.Windows.Controls;

namespace JustDub
{
    /// <summary>
    /// Логика взаимодействия для DownloadProgress.xaml
    /// </summary>
    public partial class DownloadProgress : Window
    {
        public ProgressBar progress;
        public TextBlock maximumText;
        public TextBlock valueText;
        public DownloadProgress()
        {
            InitializeComponent();
            progress = progressBar;
            maximumText = max;
            valueText = val;
        }
        public void SetProgress(int value)
        {
            valueText.Text = (value / 1e+6).ToString();
            progress.Value = value / 1e+6;
        }
        public void SetMaximum(int value)
        {
            Dispatcher.Invoke(() =>
            {
                maximumText.Text = (value / 1e+6).ToString() + " Мб";
                progress.Maximum = value / 1e+6;
            });
        }
    }
}
