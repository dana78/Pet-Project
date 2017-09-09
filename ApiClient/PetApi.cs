using PetApiClient.Interfaces;
using System.Threading.Tasks;
using System.Net;
using System;
using System.Collections.Generic;
using PetApiClient;

namespace PetApiClient.Services
{
    public class PetApi : BaseRestApi, IUsersService, IOwnersService, IAppointmentsService, IVetsService, IPetService
    {
        protected override string BaseUrl => Constants.BaseApiUrl;

        #region Singleton
        private static readonly PetApi _instance = new PetApi();
        public static PetApi Instance { get { return _instance; } }

        private PetApi() { }
        #endregion

        public async Task<int> RegisterNewUser(Credentials newUser)
        {
            var relativeUri = Constants.UriUsers;
            return await PostApiAsync<int>(relativeUri, newUser.ToJson());
        }

        public async Task<RequestResult<UserRoleInfo>> Login(Credentials userCredentials)
        {
            var result = new RequestResult<UserRoleInfo>();

            var relativeUri = $"{Constants.UriUsers}/login";
            try
            {
                result.Result = await PostApiAsync<UserRoleInfo>(relativeUri, userCredentials.ToJson());
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

        public async Task ChangePassword(int userId, string newPassword)
        {
            var relativeUri = $"{Constants.UriUsers}/{userId}/change-password";
            await PutApiAsync(relativeUri, newPassword);
        }

        public async Task<List<Pet>> GetPets(int ownerId)
        {
            var relativeUri = $"{Constants.UriOwners}/{ownerId}/pets";
            return await GetApiAsync<List<Pet>>(relativeUri);
        }

        public async Task<Appointment> PostAppointment(AppointmentRM appointment)
        {
            var relativeUri = $"appointments";
            return await PostApiAsync<Appointment>(relativeUri, appointment.ToJson());
        }

        public async Task<IEnumerable<Vet>> GetAllVets()
        {
            var relativeUri = $"vets";
            return await GetApiAsync<IEnumerable<Vet>>(relativeUri);
        }

        public async Task<IEnumerable<Appointment>> GetVetAppointments(int vetId)
        {
            var relativeUri = $"vets/{vetId}/appointments";
            return await GetApiAsync<IEnumerable<Appointment>>(relativeUri);
        }

        public Task<IEnumerable<Appointment>> GetPetAppointments(int petId)
        {
            throw new NotImplementedException();
        }

        public async Task ConfirmAppointment(int id)
        {
            var relativeUri = $"appointments/{id}/confirm";
            await PutApiAsync(relativeUri, string.Empty);
        }

        public async Task<Appointment> GetAppointment(int id)
        {
            var relativeUri = $"appointments/{id}";
            return await GetApiAsync<Appointment>(relativeUri);
        }

        public async Task<ClinicHistory> PostClinicHistory(ClinicHistoryRM history)
        {
            return await PostApiAsync<ClinicHistory>("histories", history);
        }

        public async Task<Pet> GetPet(int id)
        {
            return await GetApiAsync<Pet>($"pets/{id}");
        }

        public async Task<List<ClinicHistory>> GetPetClinicHistories(int petId)
        {
            return await GetApiAsync<List<ClinicHistory>>($"pets/{petId}/histories");
        }

        public async Task<Vet> PostVet(VetRM vet)
        {
            return await PostApiAsync<Vet>("vets", vet);
        }

        public async Task AttendAppointment(int id)
        {
            await PutApiAsync($"appointments/{id}/attend", string.Empty);
        }

        public async Task<Pet> PostPet(PetRM pet)
        {
            return await PostApiAsync<Pet>("pets", pet.ToJson());
        }

        public async Task<Pet> UpdatePet(int petId, PetRM pet)
        {
            return await PutApiAsync<Pet>($"pets/{petId}", pet.ToJson());
        }
    }
}
