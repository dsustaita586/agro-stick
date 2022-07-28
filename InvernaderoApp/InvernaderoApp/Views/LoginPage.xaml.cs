using OpenNETCF.MQTT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvernaderoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (ValidarFormulario())
                {
                    Application.Current.MainPage = new MenuShell();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private bool ValidarFormulario()
        {
            bool bandera = true;

            SfTxtUsuario.HasError = false;
            if (string.IsNullOrEmpty(TxtUsuario.Text.Trim()))
            {
                SfTxtUsuario.ErrorText = "Campo requerido";
                SfTxtUsuario.HasError = true;
                bandera = false;
            }

            SfTxtPass.HasError = false;
            if (string.IsNullOrEmpty(TxtPass.Text.Trim()))
            {
                SfTxtPass.ErrorText = "Campo requerido";
                SfTxtPass.HasError = true;
                bandera = false;
            }

            return bandera;
        }
    }
}