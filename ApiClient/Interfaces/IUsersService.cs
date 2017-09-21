using PetApiClient.Models;
using System.Threading.Tasks;

namespace PetApiClient.Interfaces
{
    public interface IUsersService
    {
        Task<int> RegisterNewUser(Credentials newUser);
        Task<UserRoleInfo> Login(Credentials userCredentials);
        Task ChangePassword(int userId, string newPassword);
    }
}
