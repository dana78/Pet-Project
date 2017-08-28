using System.Threading.Tasks;

namespace PetApiClient.Interfaces
{
    public interface IUsersService
    {
        Task<int> RegisterNewUser(Credentials newUser);
        Task<RequestResult<UserRoleInfo>> Login(Credentials userCredentials);
        Task ChangePassword(int userId, string newPassword);
    }
}
