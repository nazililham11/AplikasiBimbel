using System.Data;

namespace AplikasiBimbel.Controller
{
    public interface IDatabaseEntity<T>
    {
        T Entity(IDataReader reader);

        T Entity(IDataReader reader, int[] orders);
    }
}
