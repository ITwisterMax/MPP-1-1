using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;
using TracerLibrary.Model;
using Writer.Model;

namespace Writer.Helper
{
    public static class Serializator
    {
        // Json serialization
        public static SerializedTraceResult JsonSerialize(TraceResult traceResult)
        {
            try
            {
                var serializeTraceResult = new TraceResultSerialize(traceResult);

                var serializer = new JsonSerializer();
                serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.TypeNameHandling = TypeNameHandling.Auto;
                serializer.Formatting = Formatting.Indented;

                var stringBuilder = new StringBuilder();

                using (var stringWriter = new StringWriter(stringBuilder))
                using (var writer = new JsonTextWriter(stringWriter))
                {
                    serializer.Serialize(writer, serializeTraceResult);
                }

                string serializedData = stringBuilder.ToString();
                var serializedTraceResult = new SerializedTraceResult(serializedData, SerializedTraceResult.TFormat.JSON);

                return serializedTraceResult;
            }
            catch
            {
                return null;
            }
        }

        // Xml serialization
        public static SerializedTraceResult XmlSerialize(TraceResult traceResult)
        {
            try
            {
                var serializeTraceResult = new TraceResultSerialize(traceResult);

                XmlSerializer serializer = new XmlSerializer(typeof(TraceResultSerialize));

                StringBuilder stringBuilder = new StringBuilder();
                using (StringWriter stringWriter = new StringWriter(stringBuilder))
                {
                    serializer.Serialize(stringWriter, serializeTraceResult);
                }

                string serializedData = stringBuilder.ToString();
                var serializedTraceResult = new SerializedTraceResult(serializedData, SerializedTraceResult.TFormat.XML);
                
                return serializedTraceResult;
            }
            catch
            {
                return null;
            }
        }
    }

    public class SerializedTraceResult
    {
        // Serialization format
        public enum TFormat
        {
            JSON = 1,
            XML = 2
        }

        // Serialized data
        public string data { get; private set; }

        // Serialization format
        public TFormat format { get; private set; }

        public SerializedTraceResult(string data, TFormat format)
        {
            this.data = data;
            this.format = format;
        }
    }
}
