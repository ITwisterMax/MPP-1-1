using System.Threading;
using System.Diagnostics;
using TracerLibrary.Api;
using TracerLibrary.Model;
using TracerLibrary.Model.Service;

namespace TracerLibrary.Block
{
    public class Tracer : ITracer
    {
        static object threadLocker = new object();
        
        public void StartTrace()
        {
            // Get info about current method
            StackTrace stackTrace = new StackTrace(true);
            StackFrame stackFrame = stackTrace.GetFrame(1);

            // Get info about current thread
            int threadId = Thread.CurrentThread.ManagedThreadId;
            ThreadService thread = TraceService.GetThread(threadId);
            
            // Add new thread if it isn't exist
            if (thread == null)
            {
                thread = new ThreadService(threadId);
                TraceService.AddThread(thread);
            }

            // Get thread id, metod name and class name
            MethodService method  = new MethodService(
                threadId,
                stackFrame.GetMethod().Name, 
                stackFrame.GetMethod().DeclaringType.Name
            );

            // Add new method
            lock (threadLocker)
            {
                thread.PushMethod(method);
            }          

            method.RunTimer();
        }

        public void StopTrace()
        {
            // Get info about current thread
            int threadId = Thread.CurrentThread.ManagedThreadId;
            ThreadService thread = TraceService.GetThread(threadId);

            if (thread == null)
            {
                return;
            }

            // Get info about execution method
            MethodService method = thread.PopMethod();

            method.StopTimer();
        }

        // Get final informations
        public TraceResult GetTraceResult()
        {
            return new TraceResult(TraceService.GetTrace());
        }
    }
}
