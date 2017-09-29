using PetApiClient;
using PetMobile.Helpers.Converters;
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
        private PetEditViewModel viewModel;

        /// <summary>
        /// Send null pet to create, an instance to update
        /// </summary>
        /// <param name="pet">Pet to update</param>
        public PetEditPage(Pet pet = null)
        {
            viewModel = new PetEditViewModel(pet);
            InitializeComponent();
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

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedIndex = picker.SelectedItem;

            var converter = new BoolToSexConverter();
            bool? sex = (bool?)converter.ConvertBack(selectedIndex, null, null, null);
            viewModel.Pet.Sex = sex;
        }
    }
}