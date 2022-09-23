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
        private object _locker = new object();


        private ConcurrentDictionary<int, ThreadInfo> _threadsDictionary;
        private List<ThreadInfo> _threads;

        static Tracer()
        {
            _stopwatch.Start();
        }

        public Tracer()
        {
            _threads = new List<ThreadInfo>();
            _threadsDictionary = new ConcurrentDictionary<int, ThreadInfo>();
        }

        public List<ThreadInfo> GetTraceResult()
        {
            return _threads;
        }

        public void StartTrace()
        {
            ThreadInfo currentThread;
            {
                var currentThreadId = Thread.CurrentThread.ManagedThreadId;
                lock (_locker)
                {
                    if(_threadsDictionary.ContainsKey(currentThreadId))
                    {
                        currentThread = _threadsDictionary[currentThreadId];
                    }
                    else
                    {
                        currentThread = new ThreadInfo();
                        currentThread.Id = currentThreadId;
                        _threadsDictionary.TryAdd(currentThreadId, currentThread);
                        _threads.Add(currentThread);
                        currentThread.OldTime = _stopwatch.ElapsedMilliseconds;
                        currentThread.Time = 0;
                    }
                }
                

               
                StackTrace stackTrace = new StackTrace();
                var frame = stackTrace.GetFrame(1);
                var method = frame.GetMethod();
                
                
                if(currentThread.CurrentNode == null)
                {
                    currentThread.CurrentNode = new Node<MethodInfo>(new MethodInfo() { 
                        ClassName = method.DeclaringType.Name,
                        MethodName = method.Name
                    });
                    currentThread.CurrentNode.Parent = null;
                    //_root = _currentNode;
                    currentThread.ThreadMethods.Add(currentThread.CurrentNode);
                    currentThread.CurrentNode.Data.Time = _stopwatch.ElapsedMilliseconds;
                    //_stopwatch.Start();
                }
                else
                {
                   // 
                    var temp = new Node<MethodInfo>(new MethodInfo()
                    {
                        ClassName = method.DeclaringType.Name,
                        MethodName = method.Name,
                        Time = _stopwatch.ElapsedMilliseconds
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

            var tempTime = _stopwatch.ElapsedMilliseconds;
            currentThread.CurrentNode.Data.Time = tempTime - currentThread.CurrentNode.Data.Time;
            currentThread.CurrentNode = currentThread.CurrentNode.Parent;

            currentThread.Time = tempTime - currentThread.OldTime + currentThread.Time;
            currentThread.OldTime = tempTime;
        }
    }
}