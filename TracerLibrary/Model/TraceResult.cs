using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using TracerLibrary.Model.Service;

namespace TracerLibrary.Model
{
    public class TraceResult : IEnumerable<ThreadResult>
    {
        // Information about threads
        public readonly ReadOnlyCollection<ThreadResult> threadsData;

        // Write data in TraceResult
        public TraceResult(ConcurrentDictionary<int, ThreadService> traceResult)
        {
            ThreadResult[] threadsData;

            if (traceResult != null)
            {
                threadsData = new ThreadResult[traceResult.Count];

                int i = 0;
                foreach (KeyValuePair<int, ThreadService> pair in traceResult)
                {
                    threadsData[i++] = new ThreadResult(pair.Value.threadId, pair.Value.time, pair.Value.tree);
                }
            }
            else
            {
                threadsData = new ThreadResult[0];
            }

            this.threadsData = new ReadOnlyCollection<ThreadResult>(threadsData);
        }

        // Realize GetEnumerator
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (ThreadResult threadResult in threadsData)
            {
                yield return threadResult;
            }
        }

        // Realize GetEnumerator
        IEnumerator<ThreadResult> IEnumerable<ThreadResult>.GetEnumerator()
        {
            foreach (ThreadResult threadResult in threadsData)
            {
                yield return threadResult;
            }    
        }
    }
}
