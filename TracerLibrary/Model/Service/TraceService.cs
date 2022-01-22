using System;
using System.Collections.Concurrent;

namespace TracerLibrary.Model.Service
{
    public static class TraceService
    {
        // Information about threads
        private static ConcurrentDictionary<int, ThreadService> threadsData;

        static TraceService()
        {
            threadsData = new ConcurrentDictionary<int, ThreadService>();
        }

        // Add new thread
        public static bool AddThread(ThreadService thread)
        {
            return threadsData.TryAdd(thread.threadId, thread);
        }

        // Get thread with specifical id
        public static ThreadService GetThread(int threadId)
        {
            ThreadService result = null;

            threadsData.TryGetValue(threadId, out result);

            return result;
        }

        // Add thread execution time
        public static bool AddThreadTime(int threadId, TimeSpan time)
        {
            ThreadService thread = null;

            if (!threadsData.TryGetValue(threadId, out thread))
            {
                return false;
            }
               
            thread.time = thread.time.Add(time);
            
            return true;
        }

        // Get information about threads
        public static ConcurrentDictionary<int, ThreadService> GetTrace()
        {
            return threadsData;
        }
    }
}
