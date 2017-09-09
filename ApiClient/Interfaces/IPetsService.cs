using System.Threading.Tasks;

namespace PetApiClient.Interfaces
{
    public interface IPetService
    {
        Task<Pet> PostPet(PetRM pet);
        Task<Pet> UpdatePet(int petId, PetRM pet);
    }
}
