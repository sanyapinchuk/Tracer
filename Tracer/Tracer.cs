using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private static Stopwatch _stopwatch = new Stopwatch();
        private TraceResult _traceResult;
        private bool thereIsChildren;
        private int currentTreeIndex = -1;
        TraceResult currentTrace;
        
        private Node<TraceResult> _root;



        private ConcurrentDictionary<int, ThreadInfo> _threadsDictionary;
        private List<ThreadInfo> _threads;

        static Tracer()
        {
            stopwatch.Start();
        }

        public Tracer()
        {
            _traceResult = new TraceResult();
            _threads = new List<ThreadInfo>();
            _threadsDictionary = new ConcurrentDictionary<int, ThreadInfo>();
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
            ThreadInfo currentThread;
            {
                var currentThreadId = Thread.CurrentThread.ManagedThreadId;
                if(_threadsDictionary.ContainsKey(currentThreadId))
                {
                    currentThread = _threadsDictionary[currentThreadId];
                }
                else
                {
                    currentThread = new ThreadInfo();
                    currentThread.Id = currentThreadId;
                    _threadsDictionary.TryAdd(currentThreadId, currentThread);
                    currentThread.Time = stopwatch.ElapsedMilliseconds;
                }

               
                StackTrace stackTrace = new StackTrace();
                var frame = stackTrace.GetFrame(0);
                var method = frame.GetMethod();
                
                
                if(currentThread.CurrentNode == null)
                {
                    currentThread.CurrentNode = new Node<TraceResult>(new TraceResult() { ClassName = method.DeclaringType.Name,
                    MethodName = method.Name});
                    currentThread.CurrentNode.Parent = null;
                    //_root = _currentNode;
                    currentThread.ThreadMethods.Add(currentThread.CurrentNode);
                    thereIsChildren = true;
                    currentThread.CurrentNode.Data.Time = stopwatch.ElapsedMilliseconds;
                    //_stopwatch.Start();
                }
                else
                {
                   // 
                    var temp = new Node<TraceResult>(new TraceResult()
                    {
                        ClassName = method.DeclaringType.Name,
                        MethodName = method.Name,
                        Time = stopwatch.ElapsedMilliseconds
                    });
                    currentThread.CurrentNode.Children.Add(temp);
                    temp.Parent = currentThread.CurrentNode;
                    currentThread.CurrentNode = temp;
                }

            }
        }

        public void StopTrace()
        {
            ThreadInfo currentThread;
            var currentThreadId = Thread.CurrentThread.ManagedThreadId;
            if (_threadsDictionary.ContainsKey(currentThreadId))
            {
                currentThread = _threadsDictionary[currentThreadId];
            }
            else
                throw new Exception("StopTrace was calling before StartTrace");

            currentThread.CurrentNode.Data.Time = stopwatch.ElapsedMilliseconds - currentThread.CurrentNode.Data.Time;
            currentThread.CurrentNode = currentThread.CurrentNode.Parent;
            currentThread.Time = stopwatch.ElapsedMilliseconds - currentThread.Time;
        }
    }
}
