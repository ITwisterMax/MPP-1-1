using TracerLibrary.Model;

namespace TracerLibrary.Api
{   
    public interface ITracer
    {
        void StartTrace();

        void StopTrace();
        
        TraceResult GetTraceResult();
    }
}
