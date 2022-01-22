using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;
using TracerLibrary.Model;

namespace Writer.Model
{
    [Serializable]
    [JsonObject]
    public class MethodResultSerialize
    {
        // Method info (thread id, method name, class name, execution time)
        [JsonProperty("name")]
        [XmlAttribute("name")]
        public string methodName;

        [JsonProperty("time")]
        [XmlAttribute("time")]
        public string time;

        [JsonProperty("class")]
        [XmlAttribute("class")]
        public string className;


        // Information about the methods executed in this thread
        [JsonProperty("methods")]
        [XmlElement("method")]
        public List<MethodResultSerialize> methodsDataList;

        public MethodResultSerialize()
        {

        }

        public MethodResultSerialize(MethodResult methodResult)
        {
            // Get method info
            methodName = methodResult.methodName;
            className = methodResult.className;
            time = methodResult.time;

            // Write data in MethodResultSerialize
            MethodResultSerialize[] methodsData;

            if (methodResult.methodsData.Count > 0)
            {
                methodsData = new MethodResultSerialize[methodResult.methodsData.Count];

                int i = 0;
                foreach (MethodResult item in methodResult)
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
