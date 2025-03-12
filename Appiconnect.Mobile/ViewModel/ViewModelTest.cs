using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiconnect.Mobile.ViewModel
{
    public class ViewModelTest : ObservableObject
    {
        //[ObservableProperty]
        public string Text { get; set; } = "Hola mundo";
    }
}
