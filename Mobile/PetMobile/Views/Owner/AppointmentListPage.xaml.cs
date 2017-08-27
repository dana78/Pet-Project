using PetMobile.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetMobile.Views.Owner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentListPage : ContentPage
    {
        AppointmentListViewModel viewModel;
        public AppointmentListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new AppointmentListViewModel();
        }

        protected async override void OnAppearing()
        {
            await viewModel.OnAppearing();
        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}