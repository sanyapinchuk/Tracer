using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private TraceResult _traceResult = new TraceResult();
        private bool thereIsChildren;
        private int currentTreeIndex = -1;
        TraceResult currentTrace;
        private Node<TraceResult> _currentNode;
        private Node<TraceResult> _root;
        private Stopwatch _stopwatch;

        public Tracer()
        {
            thereIsChildren = true;
        }

        public Node<TraceResult> GetTraceResult()
        {
            return _root;
        }

        public void StartTrace()
        {
            /*if(!thereIsChildren)
            {
                 _currentNode = _currentNode.Parent;
            }*/
           
            {
                
                StackTrace stackTrace = new StackTrace();
                var frame = stackTrace.GetFrame(0);
                var method = frame.GetMethod();
                
                if(_currentNode == null)
                {
                    _currentNode = new Node<TraceResult>(new TraceResult() { ClassName = method.DeclaringType.Name,
                    MethodName = method.Name});
                    _currentNode.Parent = null;
                    _root = _currentNode;
                    thereIsChildren = true;
                    _currentNode.Data.Time = 0;
                    Stopwatch.StartNew();
                }
                else
                {
                   // 
                    _stopwatch.Stop();
                    var temp = new Node<TraceResult>(new TraceResult()
                    {
                        ClassName = method.DeclaringType.Name,
                        MethodName = method.Name,
                        Time = _stopwatch.ElapsedMilliseconds
                    });
                    _currentNode.Children.Add(temp);
                    temp.Parent = _currentNode;
                    _currentNode = temp;
                    _stopwatch.Start();
                }

            }
        }

        public void StopTrace()
        {
            _currentNode.Data.Time = _stopwatch.ElapsedMilliseconds - _currentNode.Data.Time;
            _currentNode= _currentNode.Parent;
        }
    }
}
