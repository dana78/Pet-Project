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
    public partial class DashboardPage : ContentPage
    {
        DashboardViewModel viewModel;
        public DashboardPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new DashboardViewModel();
        }

        protected override async void OnAppearing()
        {
            NoPetsMessage.IsVisible = false;
            MessagingCenter.Subscribe<DashboardViewModel>(this, MessageKeys.Empty, (message) =>
            {
                NoPetsMessage.IsVisible = true;
            });
            MessagingCenter.Subscribe<DashboardViewModel>(this, MessageKeys.NoConnection, (message) =>
            {
                NoPetsMessage.IsVisible = false;
            });
            await viewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<DashboardViewModel>(this, MessageKeys.Empty);
            MessagingCenter.Unsubscribe<DashboardViewModel>(this, MessageKeys.NoConnection);
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