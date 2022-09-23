using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.Dto
{
    public class ThreadInfoDto
    {
        public int Id { get; set; }
        public long Time { get; set; }
        public List<MethodInfoDto>? Methods { get; set; }
    }
}
