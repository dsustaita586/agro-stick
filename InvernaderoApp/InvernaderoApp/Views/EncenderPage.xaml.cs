using InvernaderoApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace InvernaderoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EncenderPage : ContentPage
    {
        IMaterialModalPage loadingDialog;
        private IMqttClient client;
        public EncenderPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await InitMQTTV2();
        }
        private async Task InitMQTTV2()
        {
            try
            {
                var configuration = new MqttConfiguration
                {
                    Port = Utilidades.PORT
                };
                this.client = await MqttClient.CreateAsync(Utilidades.HOST, configuration);
                var sessionState = await this.client.ConnectAsync(new MqttClientCredentials(clientId: "AppMovil"));

                // todo correcto
                CardNotificacion.BackgroundColor = Color.FromHex("#83f296");
                LblTitulo.Text = "Conectado correctamente";
                LblSubtitulo.Text = $"Se conecto correctamente al dispositivo { Utilidades.HOST }";
            }
            catch (Exception ex)
            {
                CardNotificacion.BackgroundColor = Color.FromHex("#f28383");
                LblTitulo.Text = "Ocurrio un error";
                LblSubtitulo.Text = ex.InnerException.Message;
            }
        }

        private async void SwEstado_StateChanged(object sender, Syncfusion.XForms.Buttons.SwitchStateChangedEventArgs e)
        {
            try
            {
                loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Verificando los datos, por favor espere...", configuration: Utilidades.Instancia.loadingDialogConfiguration);

                var res = SwEstado.IsOn;
                string message = "";
                if (res == true)
                {
                    CardVentilador.BackgroundColor = Color.FromHex("#cdffbd");
                    message = "true";
                }
                else
                {
                    CardVentilador.BackgroundColor = Color.FromHex("#ffbdbd");
                    message = "false";
                }

                var EncodeMessage = new MqttApplicationMessage("room/light", Encoding.UTF8.GetBytes(message));
                await client.PublishAsync(EncodeMessage, MqttQualityOfService.ExactlyOnce); //QoS2
                await Task.Delay(TimeSpan.FromSeconds(1));

                // se oculta el loader
                if (loadingDialog != null) await loadingDialog.DismissAsync();
            }
            catch (Exception ex)
            {
                CardNotificacion.BackgroundColor = Color.FromHex("#f28383");
                LblTitulo.Text = "Ocurrio un error";
                LblSubtitulo.Text = ex.InnerException.Message;
                if (loadingDialog != null) await loadingDialog.DismissAsync();
            }
        }
    }
}