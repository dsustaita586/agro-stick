using InvernaderoApp.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvernaderoApp.Views.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderView : ContentView
    {
        private Usuario usuario;
        public HeaderView()
        {
            InitializeComponent();
            this.usuario = JsonConvert.DeserializeObject<Usuario>(Application.Current.Properties["usuario"].ToString());

            lblNombre.Text = this.usuario.nombre.ToUpper();
        }
    }
}