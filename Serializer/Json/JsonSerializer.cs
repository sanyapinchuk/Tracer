using System.Text.Json;
using Tracer;
using Serializer.Dto;
using Serializer.Dto.Extentions;
using Serializer.Interfaces;
namespace Serializer.Json
{
    public class JsonSerializer: ITraceResultSerializer<List<ThreadInfo>>
    {
        public string Serialize(List<ThreadInfo> threads)
        {
            return System.Text.Json.JsonSerializer.Serialize(threads.ToDto(),
               typeof(List<ThreadInfoDto>),
               new JsonSerializerOptions { WriteIndented = true });
        }
    }
}