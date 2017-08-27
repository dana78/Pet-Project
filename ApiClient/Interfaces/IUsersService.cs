using System.Threading.Tasks;

namespace PetApiClient.Interfaces
{
    public interface IUsersService
    {
        Task RegisterNewUser(Credentials newUser);
        Task<RequestResult<UserRoleInfo>> Login(Credentials userCredentials);
        Task ChangePassword(int userId, string newPassword);
    }
}
