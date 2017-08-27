using PetApiClient;
using PetMobile.Views.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PetMobile.ViewModels
{
    public class PetProfileViewModel : BaseViewModel
    {
        private Pet _pet;
        public Pet Pet
        {
            get { return _pet; }
            set { _pet = value; RaisePropertyChanged(); }
        }

        public ICommand GoToAppointmentCommand => new Command(async () => await GoToAppointment());

        public PetProfileViewModel(Pet pet)
        {
            Pet = pet;
        }
        public PetProfileViewModel()
        {
            Pet = new Pet();
        }

        private async Task GoToAppointment()
        {
            await MasterNavigateTo(new AppointmentPage(Pet));
        }
    }
}
