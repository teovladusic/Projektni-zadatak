using System.Linq;
using System.Threading.Tasks;

namespace Projektni_Zadatak_Project_Service.Helpers
{
    public interface ISortHelper<T>
    {
        IQueryable<T> ApplySort(IQueryable<T> entities, string orderByQueryString);
    }
}
