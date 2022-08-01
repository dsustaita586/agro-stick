using InvernaderoApp.Utils;
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
    public partial class MenuShell : Shell
    {
        public MenuShell()
        {
            InitializeComponent();
        }

        private async void BtnSalir_Clicked(object sender, EventArgs e)
        {
            try
            {
                var salir = await MaterialDialog.Instance.ConfirmAsync(message: "¿Está seguro que desea salir de la aplicación?",
                                    title: "Salir",
                                    confirmingText: "Salir",
                                    dismissiveText: "Cancelar",
                                    configuration: Utilidades.Instancia.alertDialogConfiguration);

                if (salir == true)
                {
                    if (Application.Current.Properties.ContainsKey("usuario")) Application.Current.Properties.Remove("usuario");

                    await Application.Current.SavePropertiesAsync();
                    Application.Current.MainPage = new LoginPage();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}