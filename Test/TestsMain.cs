using System.Threading;
using NUnit.Framework;
using TracerLibrary.Api;
using TracerLibrary.Block;
using TracerLibrary.Model;
using Test.Classes;

namespace Test
{
    [TestFixture]
    public class TestsMain
    {
        private ITracer tracer;

        private Foo foo;

        private Bar bar;

        [SetUp]
        public void Setup()
        {
            tracer = new Tracer();
            foo = new Foo(tracer);
            bar = new Bar(tracer);
        }

        [Test]
        public void OneThreadTest()
        {
            foo.MyMethod();
            TraceResult traceResult = tracer.GetTraceResult();

            Assert.NotNull(traceResult.threadsData[0]);

            Assert.AreEqual(1, traceResult.threadsData.Count);

            Assert.AreEqual("MyMethod", traceResult.threadsData[0].methodsData[0].methodName);
            Assert.AreEqual("InnerMethod", traceResult.threadsData[0].methodsData[0].methodsData[0].methodName);

            Assert.AreEqual("MyMethod", traceResult.threadsData[0].methodsData[1].methodName);
            Assert.AreEqual("InnerMethod", traceResult.threadsData[0].methodsData[1].methodsData[0].methodName);

            Assert.GreaterOrEqual(System.Convert.ToInt32(
                traceResult.threadsData[0].time.Substring(0, traceResult.threadsData[0].time.Length - 2)
                ), 400);
        }

        [Test]
        public void TwoThreadsTest()
        {
            Thread anotherThread = new Thread(new ThreadStart(bar.InnerMethod));
            anotherThread.Start();
            foo.MyMethod();
            TraceResult traceResult = tracer.GetTraceResult();

            Assert.NotNull(traceResult.threadsData[0]);
            Assert.NotNull(traceResult.threadsData[1]);

            Assert.AreEqual(2, traceResult.threadsData.Count);

            Assert.AreEqual("MyMethod", traceResult.threadsData[0].methodsData[0].methodName);
            Assert.AreEqual("InnerMethod", traceResult.threadsData[0].methodsData[0].methodsData[0].methodName);

            Assert.AreEqual("MyMethod", traceResult.threadsData[0].methodsData[1].methodName);
            Assert.AreEqual("InnerMethod", traceResult.threadsData[0].methodsData[1].methodsData[0].methodName);

            Assert.AreEqual("InnerMethod", traceResult.threadsData[1].methodsData[0].methodName);

            Assert.GreaterOrEqual(System.Convert.ToInt32(
                traceResult.threadsData[0].time.Substring(0, traceResult.threadsData[0].time.Length - 2)
                ), 400);
            Assert.GreaterOrEqual(System.Convert.ToInt32(
                traceResult.threadsData[1].time.Substring(0, traceResult.threadsData[1].time.Length - 2)
                ), 200);
        }
    }
}
