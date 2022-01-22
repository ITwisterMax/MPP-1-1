using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TracerLibrary.Helper;
using TracerLibrary.Model.Service;

namespace TracerLibrary.Model
{
    public class ThreadResult : IEnumerable<MethodResult>
    {
        // Const values to convert units of measurement 
        private const int SECONDS = 1000;
        
        private const int MINUTES = 60 * SECONDS;
        
        private const int HOURS = 60 * MINUTES;


        // Thread info (thread id, execution time)
        public readonly string threadId;

        public readonly string time;


        // Information about the methods executed in this thread
        public readonly ReadOnlyCollection<MethodResult> methodsData;

        public ThreadResult(int threadId, TimeSpan time, TreeNode<MethodService> methodsTree)
        {
            // Get thread info
            this.threadId = threadId.ToString();

            this.time = String.Format("{0}ms", time.Hours * HOURS + time.Minutes * MINUTES + 
                time.Seconds * SECONDS + time.Milliseconds);

            MethodResult[] methodsData;

            // Write data in ThreadResult
            if (methodsTree.children.Count > 0)
            {
                methodsData = new MethodResult[methodsTree.children.Count];

                int i = 0;
                foreach (TreeNode<MethodService> node in methodsTree.children)
                {
                    methodsData[i++] = new MethodResult(node.data.methodName, node.data.className, node.data.time, node);
                }
            }
            else
            {
                methodsData = new MethodResult[0];
            }

            this.methodsData = new ReadOnlyCollection<MethodResult>(methodsData);
        }

        // Realize GetEnumerator
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (MethodResult methodTraceResult in methodsData)
            {
                yield return methodTraceResult;
            }
        }

        // Realize GetEnumerator
        IEnumerator<MethodResult> IEnumerable<MethodResult>.GetEnumerator()
        {
            foreach (MethodResult methodTraceResult in methodsData)
            {
                yield return methodTraceResult;
            }
        }
    }
}
