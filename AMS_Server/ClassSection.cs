using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS_Server
{
    public class ClassSection
    {
        public ClassSection(string _sec_id, string _sec_name) {
            this.sec_id = _sec_id;
            this.sec_name = _sec_name;
        }
        public string sec_id { get; set; }
        public string sec_name { get; set; }
    }
}
