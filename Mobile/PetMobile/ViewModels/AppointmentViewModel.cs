using PetApiClient.Interfaces;
using PetApiClient;
using PetApiClient.Services;
using PetMobile.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using PetMobile.Views;

namespace PetMobile.ViewModels
{
    public class AppointmentViewModel : BaseViewModel
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly IVetsService _vetsService;

        public ObservableRangeCollection<Vet> Vets { get; set; }
        public Pet Pet { get; set; }
        public AppointmentRM Appointment { get; set; }
        public Command GoToVetSelectionCommand { get; set; }

        public AppointmentViewModel() : this(PetApi.Instance, PetApi.Instance) { }
        public AppointmentViewModel(IAppointmentsService appointmentService, IVetsService vetsService)
        {
            _appointmentsService = appointmentService;
            _vetsService = vetsService;

            Vets = new ObservableRangeCollection<Vet>();
            Pet = new Pet();
            Appointment = new AppointmentRM { Date = DateTime.Now };

            GoToVetSelectionCommand = new Command(async () => await GoToVetSelection(),
                                                        () => !IsBusy);
        }

        private async Task GoToVetSelection()
        {
            ChangeIsBusy(true);

            if (!string.IsNullOrEmpty(Appointment.Title))
            {
                //  Successful validation
                await GetVets();
                await MasterNavigateTo(new AppointmentVetSelection(this));
            }
            else
            {
                //  Failed validation
                await DisplayAlert("Un momento", "Debes ingresar un título (motivo) a tu cita.");
            }

            ChangeIsBusy(false);
        }

        public async Task GetVets()
        {
            IEnumerable<Vet> enumerable = await _vetsService.GetAllVets();
            Vets.ReplaceRange(enumerable);
        }

        public async Task MakeAppointment(Vet vet)
        {
            var shouldContinue = await DisplayAlert("Cita",
                                $"Has seleccionado a {vet.Fullname} para tu cita con {Pet.Firstname}. ¿Deseas continuar?",
                                "SI", "NO");
            if (!shouldContinue)
                return;

            ChangeIsBusy(true);

            Appointment.VetId = vet.IdVet;
            Appointment.PetId = Pet.IdPet;
            var appointment = await _appointmentsService.PostAppointment(Appointment);

            ChangeIsBusy(false);

            await DisplayAlert("¡Correcto!",
                                $"¡Genial! " +
                                $"Has agendado una cita para {Pet.Firstname} " +
                                $"con el código {appointment.IdAppointment} " +
                                $"y fecha {Appointment.Date.Value.ToString("dd MMMM yyyy")}.");

            await MasterNavigateToRoot();
        }

        private void ChangeIsBusy(bool isBusy)
        {
            IsBusy = isBusy;
            GoToVetSelectionCommand.ChangeCanExecute();
        }
    }

}
