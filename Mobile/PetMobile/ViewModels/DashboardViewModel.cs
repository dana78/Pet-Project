using PetApiClient.Interfaces;
using PetApiClient;
using PetApiClient.Services;
using PetMobile.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;
using PetMobile.Views;
using Plugin.Connectivity.Abstractions;
using Plugin.Connectivity;

namespace PetMobile.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly IOwnersService _ownersService;
        private readonly IConnectivity _connectivityService;

        public ObservableRangeCollection<Pet> Pets { get; set; }
        public ICommand NavigateToPetCreationCommand => new Command(async () => await NavigateToPetCreation());


        public DashboardViewModel() : this(PetApi.Instance, CrossConnectivity.Current) { }
        public DashboardViewModel(IOwnersService ownersService, IConnectivity connectivityService)
        {
            Pets = new ObservableRangeCollection<Pet>();
            _ownersService = ownersService;
            _connectivityService = connectivityService;
        }

        public async Task OnAppearing()
        {
            if (Pets.Count > 0)
                return;

            IsBusy = true;

            var connectionStatus = await Util.CheckApiConnection(_connectivityService);
            if (connectionStatus.Available)
            {
                Pets.ReplaceRange(await _ownersService.GetPets(Session.Owner.IdOwner.Value));
                if (Pets.Count == 0)
                    MessagingCenter.Send(this, MessageKeys.Empty);
            }
            else
            {
                MessagingCenter.Send(this, MessageKeys.NoConnection);
                await DisplayAlert("Ups!", connectionStatus.Message);
            }

            IsBusy = false;
        }

        private async Task NavigateToPetCreation()
        {
            await NavigateTo(new PetEditPage(null));
        }
    }
}
