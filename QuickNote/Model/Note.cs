using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNote.Model
{
    public class Note
    {
        public string Format { get; set; }
        public List<string> Keys { get; set; }
        public List<string> Values { get; set; }
        public List<string> Tags { get; set; }
        public string TopView { get; set; }
    }
}
