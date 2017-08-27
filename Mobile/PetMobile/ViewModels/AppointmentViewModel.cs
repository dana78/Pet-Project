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

namespace PetMobile.ViewModels
{
    public class AppointmentViewModel : BaseViewModel
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly IVetsService _vetsService;

        public ObservableRangeCollection<Vet> Vets { get; set; }

        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                RaisePropertyChanged();
                Appointment.VetId = Vets[selectedIndex].IdVet;
            }
        }

        public Pet Pet { get; set; }
        public AppointmentRM Appointment { get; set; }
        public Command MakeAppointmentCommand { get; set; }

        public AppointmentViewModel() : this(PetApi.Instance, PetApi.Instance) { }
        public AppointmentViewModel(IAppointmentsService appointmentService, IVetsService vetsService)
        {
            _appointmentsService = appointmentService;
            _vetsService = vetsService;

            Vets = new ObservableRangeCollection<Vet>();
            Pet = new Pet();
            Appointment = new AppointmentRM();
            Appointment.Date = DateTime.Now;

            MakeAppointmentCommand = new Command(async () => await MakeAppointment(), () => !IsBusy);
        }

        public async Task OnAppearing()
        {
            IsBusy = true;

            IEnumerable<Vet> enumerable = await _vetsService.GetAllVets();
            Vets.AddRange(enumerable);

            IsBusy = false;
        }

        private async Task MakeAppointment()
        {
            ChangeIsBusy(true);

            Appointment.PetId = Pet.IdPet;
            var appointment = await _appointmentsService.PostAppointment(Appointment);

            ChangeIsBusy(false);

            await DisplayAlert("Correcto", $"Se ha registrado tu cita con el id {appointment.IdAppointment}.");
            await MasterNavigateBack();
        }

        private void ChangeIsBusy(bool isBusy)
        {
            IsBusy = isBusy;
            MakeAppointmentCommand.ChangeCanExecute();
        }
    }
}
