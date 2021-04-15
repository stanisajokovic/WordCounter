using System.Threading.Tasks;

namespace WordCounterClient
{
    public interface IWordCounterAPIService
    {
        void Run();
        Task RunAsync();
    }
}