using InvernaderoApp.Api;
using InvernaderoApp.Dtos;
using InvernaderoApp.Utils;
using Newtonsoft.Json;
using OpenNETCF.MQTT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace InvernaderoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (ValidarFormulario())
                {
                    var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Verificando los datos, por favor espere...", configuration: Utilidades.Instancia.loadingDialogConfiguration);
                    string json = await MetodosApi.Instancia.IniciarSesion(TxtUsuario.Text.Trim(), TxtPass.Text.Trim());

                    if (!string.IsNullOrEmpty(json))
                    {
                        Usuario usuario = JsonConvert.DeserializeObject<Usuario>(json);

                        if (usuario.code == 0)
                        {
                            if (Application.Current.Properties.ContainsKey("usuario")) Application.Current.Properties.Remove("usuario");
                            Application.Current.Properties.Add("usuario", JsonConvert.SerializeObject(usuario));
                            await Application.Current.SavePropertiesAsync();

                            await loadingDialog.DismissAsync();
                            Application.Current.MainPage = new MenuShell();
                        }
                        else
                        {
                            await loadingDialog.DismissAsync();
                            await MaterialDialog.Instance.AlertAsync(message: usuario.message, title: "Error", acknowledgementText: "Aceptar", configuration: Utilidades.Instancia.alertDialogConfiguration);
                        }
                    }
                    else
                    {
                        await MaterialDialog.Instance.SnackbarAsync(message: "Error: Consulte al administrador", msDuration: MaterialSnackbar.DurationLong);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await MaterialDialog.Instance.SnackbarAsync(message: "Error: Consulte al administrador", msDuration: MaterialSnackbar.DurationLong);
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