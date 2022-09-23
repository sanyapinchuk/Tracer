using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer;

namespace Serializer.Dto.Extentions
{
    public static class MethodInfoExtention
    {
        public static List<ThreadInfoDto> ToDto(this List<ThreadInfo> oldList)
        {
            var dto = new List<ThreadInfoDto>();
            foreach (var item in oldList)
            {
                var tempMethods = new List<MethodInfoDto>();

                AddListMethods(item.ThreadMethods, tempMethods);

                dto.Add(new ThreadInfoDto()
                {
                    Id = item.Id,
                    Time = item.Time,
                    Methods = tempMethods
                });
            }
            return dto;
        }

        private static void AddListMethods(List<Node<MethodInfo>> oldList, List<MethodInfoDto> dto)
        {
            foreach(var item in oldList)
            {
                var temp = new MethodInfoDto()
                {
                    ClassName = item.Data.ClassName,
                    MethodName = item.Data.MethodName,
                    Time = item.Data.Time,
                    Methods = new List<MethodInfoDto>()
                };
                if(item.Children != null && item.Children.Count>0)
                {
                    AddListMethods(item.Children, temp.Methods);
                }
                dto.Add(temp);
            }
        }
    }
}
