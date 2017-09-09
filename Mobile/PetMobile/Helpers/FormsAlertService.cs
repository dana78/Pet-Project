using System.Threading.Tasks;

namespace PetMobile.Helpers
{
    public class FormsAlertService : IAlertService
    {
        public async Task<bool> DisplayAlert(string title, string message, string positive, string negative)
        {
            return await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, positive, negative);
        }

        public async Task DisplayAlert(string title, string message, string neutral = "OK")
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, neutral);
        }
    }
}
