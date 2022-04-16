using System.Collections.Generic;

namespace JustDub
{
    class PostFields
    {
        internal static Dictionary<string, string> Players = new Dictionary<string, string>()
        {
            ["player"] = "НАШ ПЛЕЕР",
            ["kodik"] = "KODIK",
            ["torrent"] = "СКАЧАТЬ"
        };
        internal static Dictionary<string, string> Xfields = new Dictionary<string, string>()
        {
            ["orig_title"] = "Оригинальное название",
            ["other_title"] = "Другие названия",
            ["mdl_id"] = "ID MyDoramaList",
            ["sk_id"] = "ID Shikimory",
            ["kp_id"] = "ID KinoPoisk",
            ["imdb_id"] = "ID IMDB",
            ["kinopoisk"] = "Рейтинг KinoPoisk",
            ["shikimory"] = "Рейтинг Shikimory",
            ["imdb"] = "Рейтинг IMDB",
            ["r"] = "Рейтинг",
            ["quality"] = "Качество",
            ["tip"] = "Тип (аниме)",
            ["country"] = "Страны",
            ["time"] = "Длительность",
            ["year"] = "Год",
            ["actors"] = "Актеры",
            ["director"] = "Режиссеры",
            ["sound"] = "Озвучивали",
            ["sound-engineers"] = "Работали со звуком",
            ["transletors"] = "Перевод",
            ["status"] = "Статус",
            ["episodes"] = "Озвучено серий",
            ["torrent_info"] = "Информация о торренте"
        };
        internal static Dictionary<byte, string> weekDays = new Dictionary<byte, string>()
        {
            [0] = "Понедельник",
            [1] = "Вторник",
            [2] = "Среда",
            [3] = "Четверг",
            [4] = "Пятница",
            [5] = "Суббота",
            [6] = "Воскресенье"
        };
    }
}
