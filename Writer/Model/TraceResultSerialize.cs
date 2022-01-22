using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;
using TracerLibrary.Model;

namespace Writer.Model
{
    [Serializable]
    [XmlRoot("root")]
    [JsonObject]
    public class TraceResultSerialize
    {
        // Information about threads
        [JsonProperty("threads")]
        [XmlElement("thread")]
        public List<ThreadResultSerialize> threadsDataList;

        public TraceResultSerialize()
        {

        }

        // Write data in TraceResultSerialize
        public TraceResultSerialize(TraceResult traceResult)
        {
            ThreadResultSerialize[] threadsData;

            if (traceResult != null)
            {
                threadsData = new ThreadResultSerialize[traceResult.threadsData.Count];

                int i = 0;
                foreach (ThreadResult item in traceResult)
                {
                    threadsData[i++] = new ThreadResultSerialize(item);
                }
            }
            else
            {
                threadsData = new ThreadResultSerialize[0];
            }

            threadsDataList = new List<ThreadResultSerialize>(threadsData);
        }
    }
}
