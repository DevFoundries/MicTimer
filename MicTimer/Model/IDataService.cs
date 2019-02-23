using System.Threading.Tasks;

namespace MicTimer.Model
{
    public interface IDataService
    {
        Task<DataItem> GetData();
    }
}