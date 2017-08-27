using PetMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PetMobile.Views
{
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new LoginViewModel();

            RegisterLabel.GestureRecognizers.Add(new TapGestureRecognizer { Command = viewModel.MoveToRegistrationCommand });
            BaseViewModel.Navigation = this.Navigation;
        }
    }
}
