using System;
using System.Collections.Generic;
using System.Text;

namespace Sample05
{
    public class Content
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int status { get; set; }
        public DateTime add_time { get; set; } = DateTime.Now;

        public DateTime? modify_time { get; set; }
    }
}
