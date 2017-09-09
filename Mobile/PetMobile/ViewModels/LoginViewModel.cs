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

                Owner user = loginResult.Result.Owner;
                if(user == null)
                {
                    await DisplayAlert("¡Ups!", "No tienes asignado un perfil de dueño de mascota.");
                    return;
                }

                //  Successful login
                App.Database.SaveUser(user);
                Session.Instance.Owner = user;

                Application.Current.MainPage = new Views.ShellPage();
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
    }
}
