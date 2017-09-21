using PetApiClient.Interfaces;
using System.Threading.Tasks;
using System.Net;
using System;
using System.Collections.Generic;
using PetApiClient.Models;

namespace PetApiClient.Services
{
    public class PetApi : BaseRestApi, IUsersService, IOwnersService, IAppointmentsService, IVetsService, IPetService
    {
        protected override string BaseUrl => Uris.BaseApiUrl;

        #region Singleton
        private static readonly PetApi _instance = new PetApi();
        public static PetApi Instance { get { return _instance; } }

        private PetApi() { }
        #endregion

        public async Task<int> RegisterNewUser(Credentials newUser)
        {
            var relativeUri = Uris.UriUsers;
            return await PostApiAsync<int>(relativeUri, newUser.ToJson());
        }

        public async Task<UserRoleInfo> Login(Credentials userCredentials)
        {
            var relativeUri = $"{Uris.UriUsers}/login";
            return await PostApiAsync<UserRoleInfo>(relativeUri, userCredentials.ToJson());
        }

        public async Task ChangePassword(int userId, string newPassword)
        {
            var relativeUri = $"{Uris.UriUsers}/{userId}/change-password";
            await PutApiAsync(relativeUri, newPassword);
        }

        public async Task<List<Pet>> GetPets(int ownerId)
        {
            var relativeUri = $"{Uris.UriOwners}/{ownerId}/{Uris.UriPets}";
            return await GetApiAsync<List<Pet>>(relativeUri);
        }

        public async Task<Appointment> PostAppointment(AppointmentRM appointment)
        {
            var relativeUri = Uris.UriAppointments;
            return await PostApiAsync<Appointment>(relativeUri, appointment.ToJson());
        }

        public async Task<IEnumerable<Vet>> GetAllVets()
        {
            var relativeUri = Uris.UriVets;
            return await GetApiAsync<IEnumerable<Vet>>(relativeUri);
        }

        public async Task<IEnumerable<Appointment>> GetVetAppointments(int vetId)
        {
            var relativeUri = $"{Uris.UriVets}/{vetId}/{Uris.UriAppointments}";
            return await GetApiAsync<IEnumerable<Appointment>>(relativeUri);
        }

        public Task<IEnumerable<Appointment>> GetPetAppointments(int petId)
        {
            throw new NotImplementedException();
        }

        public async Task ConfirmAppointment(int id)
        {
            var relativeUri = $"{Uris.UriAppointments}/{id}/confirm";
            await PutApiAsync(relativeUri, string.Empty);
        }

        public async Task<Appointment> GetAppointment(int id)
        {
            var relativeUri = $"{Uris.UriAppointments}/{id}";
            return await GetApiAsync<Appointment>(relativeUri);
        }

        public async Task<ClinicHistory> PostClinicHistory(ClinicHistoryRM history)
        {
            return await PostApiAsync<ClinicHistory>("histories", history);
        }

        public async Task<Pet> GetPet(int id)
        {
            return await GetApiAsync<Pet>($"{Uris.UriPets}/{id}");
        }

        public async Task<List<ClinicHistory>> GetPetClinicHistories(int petId)
        {
            return await GetApiAsync<List<ClinicHistory>>($"{Uris.UriPets}/{petId}/histories");
        }

        public async Task<Vet> PostVet(VetRM vet)
        {
            return await PostApiAsync<Vet>(Uris.UriVets, vet);
        }

        public async Task AttendAppointment(int id)
        {
            await PutApiAsync($"{Uris.UriAppointments}/{id}/attend", string.Empty);
        }

        public async Task<Pet> PostPet(PetRM pet)
        {
            return await PostApiAsync<Pet>(Uris.UriPets, pet.ToJson());
        }

        public async Task<Pet> UpdatePet(int petId, PetRM pet)
        {
            return await PutApiAsync<Pet>($"{Uris.UriPets}/{petId}", pet.ToJson());
        }
    }
}
