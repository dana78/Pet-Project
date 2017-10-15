using PetApiClient;
using PetApiClient.Interfaces;
using PetApiClient.Services;
using PetMobile.Helpers;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PetMobile.ViewModels
{
    public class PetEditViewModel : BaseViewModel
    {
        private readonly IPetService _petService;
        private readonly IConnectivity _connectivityService;

        private Pet _pet;
        public Pet Pet
        {
            get { return _pet; }
            set { _pet = value; RaisePropertyChanged(); }
        }

        public bool IsEditing { get; set; }
        public DateTime MinimumDate => DateTime.Now.Subtract(TimeSpan.FromDays(365 * 20));
        public DateTime MaximumDate => DateTime.Now;
        public Command SendPetCommand => new Command(async () => await SendPet(), () => !IsBusy);


        public PetEditViewModel(Pet pet) : this(PetApi.Instance, CrossConnectivity.Current, pet) { }
        public PetEditViewModel(IPetService petService, IConnectivity connectivityService, Pet pet)
        {
            _petService = petService;
            _connectivityService = connectivityService;
            _pet = pet ?? new Pet();
        }


        private async Task SendPet()
        {
            ChangeIsBusy(true);

            if (ValidateModel(out string message))
            {
                var connection = await Util.CheckApiConnection(_connectivityService);
                if (!connection.Available)
                {
                    await DisplayAlert("¡Ups!", connection.Message);
                    return;
                }

                if (IsEditing)
                {
                    await _petService.UpdatePet(Pet.IdPet.Value, Pet.ToRequestModel());
                    await DisplayAlert("Éxito", "¡Excelente! La información de tu mascota ha sido actualizada exitosamente.");                    
                }
                else
                {
                    Pet.IdOwner = Session.Owner.IdOwner;
                    await _petService.PostPet(Pet.ToRequestModel());
                    await DisplayAlert("Éxito", "¡Excelente! Tu mascota ha sido registrada exitosamente.");
                }

                ChangeIsBusy(false);
                await NavigateGoBack();
            }
            else
            {
                ChangeIsBusy(false);
                await DisplayAlert("¡Hey!", message);
            }
        }

        public bool ValidateModel(out string message)
        {
            message = string.Empty;
            bool isValid = true;

            if (string.IsNullOrEmpty(Pet.Firstname))
            {
                isValid = false;
                message = "Debes ponerle un nombre a tu mascota";
            }
            else if (string.IsNullOrEmpty(Pet.Breed))
            {
                isValid = false;
                message = "Debes asignarle una raza a tu mascota";
            }

            return isValid;
        }

        private void ChangeIsBusy(bool isBusy)
        {
            IsBusy = isBusy;
            SendPetCommand.ChangeCanExecute();
        }
    }
}
