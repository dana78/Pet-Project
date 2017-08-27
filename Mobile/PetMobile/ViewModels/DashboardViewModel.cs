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

namespace PetMobile.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private IOwnersService _ownersService;
        public ObservableRangeCollection<Pet> Pets { get; set; }

        public DashboardViewModel() : this (PetApi.Instance){ }
        public DashboardViewModel(IOwnersService ownersService)
        {
            Pets = new ObservableRangeCollection<Pet>();
            _ownersService = ownersService;
        }

        public async Task OnAppearing()
        {
            if (Pets.Count > 0)
                return;

            IsBusy = true;

            Pets.ReplaceRange(await _ownersService.GetPets(Session.Owner.IdOwner.Value));

            IsBusy = false;
        }
    }
}
