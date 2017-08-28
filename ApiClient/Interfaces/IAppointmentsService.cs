using PetApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetApiClient.Interfaces
{
    public interface IAppointmentsService
    {
        Task<IEnumerable<Appointment>> GetVetAppointments(int vetId);
        Task<IEnumerable<Appointment>> GetPetAppointments(int petId);
        Task<Appointment> PostAppointment(AppointmentRM appointment);
        Task<Appointment> GetAppointment(int id);
        Task ConfirmAppointment(int id);
        Task AttendAppointment(int id);
    }
}
