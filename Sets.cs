using System.Windows.Media;

namespace JustDub
{
    class Sets
    {
        public static int FontSize = 15;
        public static string Version = "0.1.1.1";

        public static SolidColorBrush[] Colors { get; } =
        {
            new SolidColorBrush(Color.FromRgb(32, 32, 32)),
            new SolidColorBrush(Color.FromRgb(227, 106, 52)),
            new SolidColorBrush(Color.FromArgb(35, 0, 0, 0)),
            new SolidColorBrush(Color.FromRgb(255,255,255))
        };

        public static string[] Urls { get; } =
        {
            "http://api.justdub.ru/?posts&sort_by=new",
            "http://api.justdub.ru/?posts&id=",
            "http://vid.justdub.ru/player/?id={0}&for_app",
            "http://vid.justdub.ru/storage/{0}/{1}",
            "http://api.justdub.ru/vlc.zip",
            "http://api.justdub.ru/?posts&cats=",
            "http://api.justdub.ru/?comments",
            "http://api.justdub.ru/bin/Windows/version.txt",
            "http://api.justdub.ru/bin/Windows/v{0}/JustDub-v{0}.exe"
        };

        public static int[] PlayerTimes =
        {
            1000
        };
    }
}
