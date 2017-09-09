using PetApiClient.Interfaces;
using PetApiClient;
using PetApiClient.Services;
using PetMobile.Helpers;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetMobile.ViewModels
{
    public class AppointmentListViewModel : BaseViewModel
    {
        private readonly IAppointmentsService _appointmentsService;
        public ObservableRangeCollection<Appointment> Appointments { get; set; }


        public AppointmentListViewModel() : this (PetApi.Instance) { }
        public AppointmentListViewModel(IAppointmentsService appointmentService)
        {
            Appointments = new ObservableRangeCollection<Appointment>();
            _appointmentsService = appointmentService;
        }

        public async Task OnAppearing()
        {
            IsBusy = true;

            //Appointments.ReplaceRange(await _appointmentsService.GetPetAppointments());

            IsBusy = false;
        }
    }
}
