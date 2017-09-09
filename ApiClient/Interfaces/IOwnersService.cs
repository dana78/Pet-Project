using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetApiClient.Interfaces
{
    public interface IOwnersService
    {
        Task<List<Pet>> GetPets(int userId);        
    }
}
