using System.Threading;
using TracerLibrary.Api;

namespace Test.Classes
{
    public class Foo
    {
        private Bar bar;
        
        private ITracer tracer;

        public Foo(ITracer tracer)
        {
            this.tracer = tracer;
            bar = new Bar(tracer);
        }

        public void MyMethod()
        {
            tracer.StartTrace();

            Thread.Sleep(100);
            bar.InnerMethod();

            tracer.StopTrace();

            tracer.StartTrace();

            Thread.Sleep(100);
            bar.InnerMethod();

            tracer.StopTrace();
        }
    }
}
