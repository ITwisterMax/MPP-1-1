using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;
using TracerLibrary.Model;

namespace Writer.Model
{
    [Serializable]
    [JsonObject]
    public class ThreadResultSerialize
    {
        // Thread info (thread id, execution time)
        [JsonProperty("id")]
        [XmlAttribute("id")]
        public string threadId;

        [JsonProperty("time")]
        [XmlAttribute("time")]
        public string time;


        // Information about the methods executed in this thread
        [JsonProperty("methods")]
        [XmlElement("method")]
        public List<MethodResultSerialize> methodsDataList;

        public ThreadResultSerialize()
        {

        }

        public ThreadResultSerialize(ThreadResult threadResult)
        {
            // Get thread info
            threadId = threadResult.threadId;
            time = threadResult.time;

            // Write data in ThreadResultSerialize
            MethodResultSerialize[] methodsData;

            if (threadResult.methodsData.Count > 0)
            {
                methodsData = new MethodResultSerialize[threadResult.methodsData.Count];

                int i = 0;
                foreach (MethodResult item in threadResult)
                {
                    methodsData[i++] = new MethodResultSerialize(item);
                }
            }
            else
            {
                methodsData = new MethodResultSerialize[0];
            }

            methodsDataList = new List<MethodResultSerialize>(methodsData);
        }
    }
}
