using FormsToolkit;
using PetApiClient;
using PetMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace PetMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetProfilePage : ContentPage
    {
        private PetProfileViewModel ViewModel => BindingContext as PetProfileViewModel;
        private double _lastScroll;

        public PetProfilePage(Pet pet)
        {
            InitializeComponent();
            ViewModel.Pet = pet;
        }

        protected override void OnAppearing()
        {
            if (Parent is Xamarin.Forms.NavigationPage navigationPage)
                navigationPage.On<iOS>().EnableTranslucentNavigationBar();

            if(string.IsNullOrEmpty(Title))
                MessagingService.Current.SendMessage(MessageKeys.ToolbarColor, Color.Transparent);

            ParallaxScrollView.Scrolled += ParallaxScrollView_Scrolled;
        }

        protected override void OnDisappearing()
        {
            if (Parent is Xamarin.Forms.NavigationPage navigationPage)
                navigationPage.On<iOS>().DisableTranslucentNavigationBar();

            var color = (Color)App.Current.Resources["colorPrimary"];
            MessagingService.Current.SendMessage(MessageKeys.ToolbarColor, color);

            ParallaxScrollView.Scrolled -= ParallaxScrollView_Scrolled;
        }

        private void ParallaxScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            //double translation = 0;

            //if (_lastScroll < e.ScrollY)
            //{
            //    translation = 0 - ((e.ScrollY / 2));

            //    if (translation > 0) translation = 0;
            //}
            //else
            //{
            //    translation = 0 + ((e.ScrollY / 2));

            //    if (translation > 0) translation = 0;
            //}

            //HeaderPanel.TranslateTo(HeaderPanel.TranslationX, translation);
            //_lastScroll = e.ScrollY;

            if (e.ScrollY > (300 - PetTitle.Height))
            {
                var color = (Color)App.Current.Resources["colorPrimary"];
                MessagingService.Current.SendMessage(MessageKeys.ToolbarColor, color);
                Title = ViewModel.Pet.Fullname;
            }
            else
            {
                MessagingService.Current.SendMessage(MessageKeys.ToolbarColor, Color.Transparent);
                Title = string.Empty;
            }
        }
    }
}