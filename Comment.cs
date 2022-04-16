using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDub
{
    internal class Comment
    {
        public Comment[] comments;
        public string post_id { get; set; }
        public string autor { get; set; }
        public string text { get; set; }
        internal static List<Comment> DeJson(string json)
        {
            List<Comment> resp;
            try
            {
                resp = JsonConvert.DeserializeObject<List<Comment>>(json);
            }
            catch (Exception)
            {
                resp = new List<Comment>();
            }
            return resp;
        }
        internal Comment[] GenAll()
        {
            string json = API.Get(Sets.Urls[6]);
            comments = DeJson(json).ToArray();
            return comments;
        }
        internal static List<Comment> Get(Comment[] com, string post_id)
        {
            List<Comment> resp = new List<Comment>();
            foreach (var comment in com)
            {
                resp = com.Where(c => c.post_id == post_id).ToList();
            }
            return resp;
        }
    }
}
