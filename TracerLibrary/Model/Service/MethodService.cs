using System;
using System.Diagnostics;
using TracerLibrary.Helper;

namespace TracerLibrary.Model.Service
{
    public class MethodService
    {
        // Method info (thread id, method name, class name, execution time)
        private int threadId;

        public string methodName { get; private set; }

        public string className { get; private set; }

        public TimeSpan time { get; set; }


        // Element position in tree
        public TreeNode<MethodService> treeNode { get; set; }
        
        // Timer
        private Stopwatch stopWatch = new Stopwatch();

        public MethodService(int threadId, string methodName, string className)
        {
            this.threadId = threadId;
            this.methodName = methodName;
            this.className = className;
        }

        // Mark the start of method execution
        public void RunTimer()
        {
            stopWatch.Start();
        }

        // Mark the end of method execution and get execution time
        public void StopTimer()
        {
            stopWatch.Stop();
            time = stopWatch.Elapsed;

            if (treeNode.parent.data == null)
            {
                TraceService.AddThreadTime(threadId, time);
            }
        }
    }
}
