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
    public partial class PetProfilePage : ContentPage
    {
        private PetProfileViewModel ViewModel => BindingContext as PetProfileViewModel;

        public PetProfilePage(Pet pet)
        {
            InitializeComponent();
            ViewModel.Pet = pet;
        }
    }
}