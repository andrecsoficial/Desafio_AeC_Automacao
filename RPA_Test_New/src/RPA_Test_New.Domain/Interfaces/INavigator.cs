using RPA_Test_New.Domain.Entities;

namespace RPA_Test_New.Domain.Interfaces
{
    public interface INavigator
    {
        //Alura Interface
        Task<ResultProcess> NavigationAlura(string url, string searchWord);
    }
}
