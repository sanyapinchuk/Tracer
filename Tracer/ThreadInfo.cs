using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class ThreadInfo
    {
        public int Id { get; set; }
        public long Time { get; set; }
        public List<Node<TraceResult>> ThreadMethods { get; set; }
        public long OldTime { get; set; } 
        public Node<TraceResult>? CurrentNode { get; set; }

        public ThreadInfo()
        {
            ThreadMethods = new List<Node<TraceResult>>();
        }
    }
}
