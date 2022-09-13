
namespace Tracer
{
    public interface ITracer    
    {
        void StartTrace();​
    
        void StopTrace();​
        Node<TraceResult> GetTraceResult();

    }
}   
