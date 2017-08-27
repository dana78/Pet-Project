using PetApiClient;

namespace PetMobile.Data
{
    public interface IDatabase
    {
        void SaveUser(Owner obj);
        void CleanDatabase();
        Owner GetLoggedUser();
    }


}
