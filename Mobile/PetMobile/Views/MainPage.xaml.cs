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
    public partial class MainPage : ContentPage
    {
        DashboardViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new DashboardViewModel();
        }

        protected override async void OnAppearing()
        {
            await viewModel.OnAppearing();
        }

        private async Task Pets_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            (sender as ListView).SelectedItem = null;

            var petSelected = e.Item as Pet;
            var page = new PetProfilePage(petSelected);
            await viewModel.MasterNavigateTo(page);
        }
    }
}