using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.Dto
{
    public  class MethodInfoDto
    {
        public string? ClassName { get; set; }

        public string? MethodName { get; set; }

        public long Time { get; set; }

        public List<MethodInfoDto>? Methods { get; set; }
    }
}
