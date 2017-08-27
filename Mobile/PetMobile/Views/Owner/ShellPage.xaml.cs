using PetMobile.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetMobile.Views.Owner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShellPage : MasterDetailPage
    {
        public ShellPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        protected override void OnAppearing()
        {
            BaseViewModel.Navigation = this.Navigation;
            BaseViewModel.Master = this;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as ShellPageMenuItem;
            if (item == null)
                return;

            MasterPage.ListView.SelectedItem = null;

            //  Sign out clicked?
            if (item.Id != -1)
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;
                Detail = new NavigationPage(page);
                IsPresented = false;
            }
            else
            {
                await ManageSignOutClick();
            }            
        }

        private async Task ManageSignOutClick()
        {
            var shouldSignOut = await DisplayAlert(
                "Cerrar sesión", 
                "Está a punto de cerrar sesión. ¿Desea continuar?", 
                "SI", "NO");
            if (shouldSignOut)
            {
                App.Database.CleanDatabase();
                Application.Current.MainPage = new LoginPage();
                IsPresented = false;
            }
        }
    }
}