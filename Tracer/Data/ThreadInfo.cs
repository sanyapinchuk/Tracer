using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    [Serializable]
    public class ThreadInfo
    {
        public int Id { get; set; }
        public long Time { get; set; }
        public List<Node<MethodInfo>> ThreadMethods { get; set; }
        internal long OldTime { get; set; } 
        internal Node<MethodInfo>? CurrentNode { get; set; }

        public ThreadInfo()
        {
            ThreadMethods = new List<Node<MethodInfo>>();
        }
    }
}
