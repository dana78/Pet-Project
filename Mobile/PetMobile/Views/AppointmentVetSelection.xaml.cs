using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PetMobile.ViewModels;
using PetApiClient;

namespace PetMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppointmentVetSelection : ContentPage
	{
        AppointmentViewModel viewModel;
		public AppointmentVetSelection (AppointmentViewModel appointmentViewModel)
		{
			InitializeComponent ();
            BindingContext = viewModel = appointmentViewModel;
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            (sender as ListView).SelectedItem = null;

            var vetSelected = e.Item as Vet;
            await viewModel.MakeAppointment(vetSelected);
        }
    }
}