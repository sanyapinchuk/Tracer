
namespace Tracer
{
    public interface ITracer    
    {
        void StartTrace();
        void StopTrace();
        List<ThreadInfo> GetTraceResult();

    }
}   
