using System;
using System.Collections.Generic;

namespace JustDub
{
    class Link
    {
        public List<string> Files { get; set; }
        public int id;
        public void SetId(string url)
        {
            id = int.Parse(url.Split('?')[1].Split('&')[0].Split('=')[1]);
        }
        public void DeJson()
        {
            string url = string.Format(Sets.Urls[2], id);
            string[] eps = API.Get(url).Replace("[", "").Replace("]", "").Split(',');
            List<string> result = new List<string>();
            foreach (var i in eps)
            {
                result.Add(i.Replace("\"", ""));
            }
            Files = result;
            Files.RemoveAt(0);
            Files.RemoveAt(0);
        }
        internal Dictionary<string, Dictionary<string, string>> GetDict()
        {
            var episodes = new Dictionary<string, Dictionary<string, string>>();
            foreach (string fn in Files)
            {
                string[] naq = fn.Replace(".mp4", "").Split('-');
                if (episodes.TryGetValue(naq[0], out _))
                    episodes[naq[0]].Add(naq[1], fullLink(fn, id));
                else
                    episodes.Add(naq[0], new Dictionary<string, string>() 
                    { 
                        [naq[1]] = fullLink(fn, id)
                    });
            }
            return episodes;
        }
        private string fullLink(string link, int id)
        {
            return string.Format(Sets.Urls[3], id, link);
        }
    }
}
