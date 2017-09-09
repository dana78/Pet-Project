using PetMobile.Data;
using PetMobile.Helpers;
using SQLite.Net.Interop;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PetMobile
{

    public partial class App : Application
    {
        public static IDatabase Database;

        public App(ISQLitePlatform platform)
        {
            InitializeComponent();

            Database = new SQLiteDatabase(platform);
            var user = Database.GetLoggedUser();
            if (user == null)
            {
                MainPage = new Views.LoginPage();
            }
            else
            {
                Session.Instance.Owner = user;
                MainPage = new Views.ShellPage();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
