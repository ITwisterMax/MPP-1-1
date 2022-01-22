using System;
using System.Threading;
using TracerLibrary.Block;
using TracerLibrary.Model;
using Test.Classes;
using Writer.Block;
using Writer.Helper;

namespace Writer
{
    class Program
    {
        // Entry point
        static void Main(string[] args)
        {
            var tracer = new Tracer();
            var foo = new Foo(tracer);
            var bar = new Bar(tracer);

            // Test
            Thread anotherThread = new Thread(new ThreadStart(bar.InnerMethod));
            anotherThread.Start();
            foo.MyMethod();

            // Get test results
            TraceResult traceResult = tracer.GetTraceResult();

            // Print results
            SerializedTraceResult traceResultJson = Serializator.JsonSerialize(traceResult);
            Printer.ConsolePrint(traceResultJson);
            Printer.FilePrint("D:/", traceResultJson);

            Console.WriteLine();

            SerializedTraceResult traceResultXml = Serializator.XmlSerialize(traceResult);
            Printer.ConsolePrint(traceResultXml);
            Printer.FilePrint("D:/", traceResultXml);

            Console.ReadLine();
        }
    }
}
