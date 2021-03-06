﻿using PetMobile.Helpers;
using PetMobile.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

namespace PetMobile.ViewModels
{
    class ShellMasterViewModel : BaseViewModel
    {
        public ObservableCollection<ShellPageMenuItem> MenuItems { get; set; }

        public ShellMasterViewModel()
        {
            MenuItems = new ObservableCollection<ShellPageMenuItem>(new[]
            {
                new ShellPageMenuItem { Id = 0, Title = "Inicio", TargetType = typeof(DashboardPage) },
                new ShellPageMenuItem { Id = 1, Title = "Mis citas" },
                new ShellPageMenuItem { Id = 2, Title = "Configuración" },
                new ShellPageMenuItem { Id = -1, Title = "Cerrar sesión" }
            });
        }

    }
}
