
using RPA_Test_New.Domain.Entities;

namespace RPA_Test_New.Domain.Interfaces
{
    public interface IRpaRepository
    {
        //SELECTs
        Task<AluraCredential> GetCredential(CancellationToken ct = default);

        //INSERTs
        Task<bool> InsertData(List<DataExtracted> dataExtracted, CancellationToken ct = default);
    }
}
