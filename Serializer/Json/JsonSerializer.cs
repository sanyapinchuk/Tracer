using System.Text.Json;
using Tracer;
using Serializer.Dto;
using Serializer.Dto.Extentions;

namespace Serializer
{
    public static class JsonSerializer
    {
        public static string Serialize(List<ThreadInfo> threads)
        {
            return System.Text.Json.JsonSerializer.Serialize(threads.ToDto(), 
                typeof(List<ThreadInfoDto>),
                new JsonSerializerOptions { WriteIndented = true });
        }
    }
}