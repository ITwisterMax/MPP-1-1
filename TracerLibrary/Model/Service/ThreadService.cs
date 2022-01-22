using System;
using System.Collections.Concurrent;
using TracerLibrary.Helper;

namespace TracerLibrary.Model.Service
{
    public class ThreadService
    {
        static object threadLocker = new object();

        // Thread info (thread id, execution time)
        public int threadId { get; private set; }
        
        public TimeSpan time { get; set; }


        // Information about the methods executed in this thread
        public ConcurrentStack<MethodService> methodsData;
        
        // Methods tree
        public TreeNode<MethodService> tree;

        public ThreadService(int threadId)
        {
            this.threadId = threadId;
            methodsData = new ConcurrentStack<MethodService>();
            tree = new TreeNode<MethodService>(null);
        }

        // Try add new method in stack and in tree
        public void PushMethod(MethodService method)
        {
            lock (threadLocker)
            {
                AddToTree(method);
            }

            methodsData.Push(method);
        }

        // Try get method from stack
        public MethodService PopMethod()
        {
            MethodService method;

            if (methodsData.TryPop(out method))
            {
                return method;
            }
            
            return null;  
        }

        // Try add new method in tree
        public void AddToTree(MethodService method)
        {
            TreeNode<MethodService> parentNode;
            MethodService parentMethod;

            if (methodsData.TryPeek(out parentMethod))
            {
                parentNode = parentMethod.treeNode;
            }
            else
            {
                parentNode = tree;
            }

            method.treeNode = parentNode.AddChild(method);
        }
    }
}
