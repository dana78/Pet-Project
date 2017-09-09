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
    public partial class PetEditPage : ContentPage
    {
        /// <summary>
        /// Send null pet to create, an instance to update
        /// </summary>
        /// <param name="pet">Pet to update</param>
        public PetEditPage(Pet pet = null)
        {
            InitializeComponent();
            var viewModel = new PetEditViewModel(pet);
            if (pet == null)
            {
                FormTitle.Text = "Registro de mascota";
                viewModel.IsEditing = false;
            }
            else
            {
                FormTitle.Text = "Actualización de mascota";
                viewModel.IsEditing = true;
            }
            BindingContext = viewModel;
        }
    }
}