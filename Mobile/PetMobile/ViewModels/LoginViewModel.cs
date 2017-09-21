using PetApiClient;
using PetApiClient.Interfaces;
using PetApiClient.Models;
using PetApiClient.Services;
using PetMobile.Helpers;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System.Net;
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
                if (ValidateCredentials(out string message))
                {
                    var connection = await Util.CheckApiConnection(_connectivityService);
                    if (!connection.Available)
                    {
                        await DisplayAlert("¡Ups!", connection.Message);
                        return;
                    }

                    var loginResult = await TryLogin(Credentials);
                    if (!loginResult.Success)
                    {
                        await DisplayAlert("¡Ups!", loginResult.Message);
                        return;
                    }

                    Owner user = loginResult.Result.Owner;
                    if (user == null)
                    {
                        await DisplayAlert("¡Ups!", "No tienes asignado un perfil de dueño de mascota.");
                        return;
                    }

                    //  Successful login
                    App.Database.SaveUser(user);
                    Session.Instance.Owner = user;

                    Application.Current.MainPage = new Views.ShellPage();
                }
                else
                {
                    await DisplayAlert("Un momento", message);
                }
            }
            finally
            {
                ChangeIsBusy(false);
            }
        }

        private async Task<RequestResult<UserRoleInfo>> TryLogin(Credentials credentials)
        {
            var result = new RequestResult<UserRoleInfo>();
            try
            {
                result.Result = await _usersService.Login(credentials);
                result.Success = true;
            }
            catch (ApiException e)
            {
                result.Success = false;
                switch (e.ErrorCode)
                {
                    case HttpStatusCode.NotFound:
                    case HttpStatusCode.BadRequest:
                        result.Message = "Las credenciales que ingresó no son correctas";
                        break;
                    default:
                        result.Message = "Algo anda fallando con nosotros :(";
                        break;
                }
            }

            return result;
        }

        private async Task MoveToRegistration()
        {
            await Navigation.PushModalAsync(new Views.RegistrationPage());
        }

        private bool ValidateCredentials(out string message)
        {
            message = string.Empty;
            bool isValid = true;

            if (string.IsNullOrEmpty(Credentials.Email))
            {
                message = "Ingresa un email válido.";
                isValid = false;
            }

            if (string.IsNullOrEmpty(Credentials.Password))
            {
                message = "Olvidaste ingresar tu contraseña.";
                isValid = false;
            }
            return isValid;
        }

        private void ChangeIsBusy(bool isBusy)
        {
            IsBusy = isBusy;
            LoginCommand.ChangeCanExecute();
        }
    }
}
