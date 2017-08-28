using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetApiClient.Interfaces
{
    public interface IVetsService
    {
        Task<IEnumerable<Vet>> GetAllVets();
        Task<Vet> PostVet(VetRM vet);
    }
}
