using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS_Server
{
    public class Announcement
    {
        public string m_id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string recipient { get; set; }
        public DateTime senddate { get; set; }
    }
}
