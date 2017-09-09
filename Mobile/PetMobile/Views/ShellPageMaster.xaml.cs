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
    public partial class ShellPageMaster : ContentPage
    {
        public ListView ListView { get; set; }

        public ShellPageMaster()
        {
            InitializeComponent();

            BindingContext = new ShellMasterViewModel();
            ListView = MenuItemsListView;
        }
    }
}