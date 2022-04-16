using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Controls;

namespace JustDub
{
    class Post
    {
        public string id { get; set; }
        public string date { get; set; }
        public string title { get; set; }
        public string full_story { get; set; }
        public string xfields { get; set; }
        public string tags { get; set; }
        public string category { get; set; }
        internal static List<Post> DeJson(string json)
        {
            List<Post> resp;
            try
            {
                resp = JsonConvert.DeserializeObject<List<Post>>(json);
            }
            catch (System.Exception)
            {
                resp = new List<Post>();
            }
            return resp;
        }
        internal Dictionary<string, string> GetXfs()
        {
            Dictionary<string, string> xfields = new Dictionary<string, string>();
            foreach (string xf in this.xfields.Replace("||", "§").Split('§'))
            {
                string[] field = xf.Split('|');
                xfields.Add(field[0], field[1]);
            }
            return xfields;
        }
        internal string GetXf(string xf_name)
        {
            string result = "";
            if (GetXfs().TryGetValue(xf_name, out result))
                return result;
            return "";
        }
    }
}
