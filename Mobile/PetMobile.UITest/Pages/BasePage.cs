using Xamarin.UITest;

namespace PetMobile.UITest.Pages
{
    public class BasePage
    {
        protected readonly IApp app;

        protected BasePage()
        {
            app = AppInitializer.App;
        }
    }
}
