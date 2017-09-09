using PetApiClient;
using PetMobile.Helpers;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PetMobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public static INavigation Navigation { get; set; }
        public static MasterDetailPage Master { get; set; }
        public static IAlertService AlertService { get; set; } = new FormsAlertService();
        public Session Session => Session.Instance;

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public async Task DisplayAlert(string title, string message, string cancel = "OK")
        {
            await AlertService.DisplayAlert(title, message, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await AlertService.DisplayAlert(title, message, accept, cancel);
        }

        public async Task NavigateTo(Page pageView)
        {
            await Navigation.PushModalAsync(pageView);
        }

        public async Task MasterNavigateTo(Page pageView)
        {
            Master.IsPresented = false;
            await Master.Detail.Navigation.PushAsync(pageView);
        }

        public async Task MasterNavigateBack()
        {
            await Master.Detail.Navigation.PopAsync();
        }

        public async Task MasterNavigateToRoot()
        {
            await Master.Detail.Navigation.PopToRootAsync();
        }

        public async Task NavigateGoBack()
        {
            await Navigation.PopAsync();
        }
    }
}
