
using RPA_Test_New.Domain.Entities;

namespace RPA_Test_New.Domain.Interfaces
{
    public interface IRpaRepository
    {
        //INSERTs
        Task<bool> InsertData(DataExtracted data, CancellationToken ct = default);
    }
}
