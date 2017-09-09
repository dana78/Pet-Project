using PetApiClient;
using PetMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppointmentPage : ContentPage
	{
        AppointmentViewModel viewModel;

        public AppointmentPage (Pet pet)
		{
			InitializeComponent ();
            BindingContext = viewModel = new AppointmentViewModel();
            viewModel.Pet = pet;
		}

    }
}