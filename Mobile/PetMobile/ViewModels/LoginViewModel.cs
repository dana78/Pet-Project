using PetApiClient;
using PetApiClient.Interfaces;
using PetApiClient.Services;
using PetMobile.Helpers;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PetMobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IConnectivity _connectivityService;
        private readonly IUsersService _usersService;


        public Credentials Credentials { get; set; }
        public Command LoginCommand { get; set; }
        public ICommand MoveToRegistrationCommand { get; set; }


        public LoginViewModel() : this(PetApi.Instance, CrossConnectivity.Current) { }
        public LoginViewModel(IUsersService usersService, IConnectivity connectivityService)
        {
            Credentials = new Credentials();
            LoginCommand = new Command(async () => await Login(), () => !IsBusy);
            MoveToRegistrationCommand = new Command(async () => await MoveToRegistration());

            _usersService = usersService;
            _connectivityService = connectivityService;
        }


        private async Task Login()
        {
            ChangeIsBusy(true);

            try
            {
                var connection = await Util.CheckApiConnection(_connectivityService);
                if (!connection.Available)
                {
                    await DisplayAlert("¡Ups!", connection.Message);
                    return;
                }

                var loginResult = await _usersService.Login(Credentials);
                if (!loginResult.Success)
                {
                    await DisplayAlert("¡Ups!", loginResult.Message);
                    return;
                }

                //  Successful login
                Owner user = loginResult.Result.Owner;
                App.Database.SaveUser(user);
                Session.Instance.Owner = user;

                MoveToMainPage();
            }
            finally
            {
                ChangeIsBusy(false);
            }
        }        

        private async Task MoveToRegistration()
        {
            await Navigation.PushModalAsync(new Views.RegistrationPage());
        }

        private void ChangeIsBusy(bool isBusy)
        {
            IsBusy = isBusy;
            LoginCommand.ChangeCanExecute();
        }

        /// <summary>
        /// Navigates to default main page depending on the profile the user has.
        /// If the user has both Vet and Owner profile, it will navigate as Vet by default.
        /// </summary>
        private void MoveToMainPage()
        {
            Page mainPage;
            if (Session.Instance.Vet != null)
                mainPage = new Views.Vet.MainPage();
            else
                mainPage = new Views.Owner.ShellPage();

            Application.Current.MainPage = mainPage;
        }
    }
}
