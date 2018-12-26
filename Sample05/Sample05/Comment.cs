using System;
using System.Collections.Generic;
using System.Text;

namespace Sample05
{
    public class Comment
    {
        public int id { get; set; }
        public int content_id { get; set; }
        public string content { get; set; }
        public DateTime add_time { get; set; } = DateTime.Now;

    }
}
