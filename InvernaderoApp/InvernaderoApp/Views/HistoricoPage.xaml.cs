using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvernaderoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoricoPage : ContentPage
    {
        public HistoricoPage()
        {
            InitializeComponent();

            DpInicio.MaximumDate = DateTime.Today;
            DpFin.MaximumDate = DateTime.Today;
        }
    }
}