using System.Threading;
using TracerLibrary.Api;

namespace Test.Classes
{
    public class Bar
    {
        private ITracer tracer;

        public Bar(ITracer tracer)
        {
            this.tracer = tracer;
        }

        public void InnerMethod()
        {
            tracer.StartTrace();

            Thread.Sleep(200);

            tracer.StopTrace();
        }
    }
}
