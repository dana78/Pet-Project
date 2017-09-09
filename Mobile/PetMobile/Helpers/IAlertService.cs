using System.Threading.Tasks;

namespace PetMobile.Helpers
{
    public interface IAlertService
    {
        Task<bool> DisplayAlert(string title, string message, string positive, string negative);
        Task DisplayAlert(string title, string message, string neutral = "OK");
    }
}
