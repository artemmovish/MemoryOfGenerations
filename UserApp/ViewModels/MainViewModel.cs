using CommunityToolkit.Mvvm.ComponentModel;
using Infastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        string message = "Для закрытия и открытия шапки нажмите F1";

        public Action OpenShapka;
        public Action CloseShapka;
    }
}
