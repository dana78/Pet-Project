using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsToolkit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransparentNavigationPage : NavigationPage
    {
        public TransparentNavigationPage()
        {
            InitializeComponent();
        }

        public TransparentNavigationPage(Page root) : base(root)
        {
            InitializeComponent();

            MessagingService.Current.Subscribe<Color>(MessageKeys.ToolbarColor, (page, color) =>
            {
                BarBackgroundColor = color;
            });
        }
    }
}