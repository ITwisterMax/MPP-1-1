using System;
using System.Text;
using System.IO;
using Writer.Helper;

namespace Writer.Block
{
    public static class Printer
    {
        // Console output
        public static void ConsolePrint(SerializedTraceResult serializedTraceResult)
        {
            Console.WriteLine(serializedTraceResult.data);
        }

        // File output
        public static void FilePrint(string pathToFile, SerializedTraceResult serializedTraceResult)
        {
            try
            {
                using (FileStream fstream = new FileStream($"{pathToFile}\\Output.{serializedTraceResult.format}", FileMode.OpenOrCreate))
                {
                    byte[] data = Encoding.Default.GetBytes(serializedTraceResult.data);
                    fstream.Write(data, 0, data.Length);
                }
            }
            catch
            {

            }
        }
    }
}
