using InvernaderoApp.Api;
using InvernaderoApp.Dtos;
using InvernaderoApp.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private bool estatus { get; set; }
        public EncenderPage()
        {
            InitializeComponent();
            estatus = false;
            SwEstado.IsEnabled = estatus;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            SwEstado.StateChanged -= SwEstado_StateChanged;
            await ListarEstatusDispositivo();
            await InitMQTTV2();
            SwEstado.IsEnabled = estatus;
            SwEstado.StateChanged += SwEstado_StateChanged;
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
                estatus = true;
            }
            catch (Exception ex)
            {
                CardNotificacion.BackgroundColor = Color.FromHex("#f28383");
                LblTitulo.Text = "Ocurrio un error";
                LblSubtitulo.Text = ex.InnerException.Message;
                estatus = false;
            }
        }

        private async Task ListarEstatusDispositivo()
        {
            try
            {
                var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Verificando los datos, por favor espere...", configuration: Utilidades.Instancia.loadingDialogConfiguration);
                string json = await MetodosApi.Instancia.ListarEstatusDispositivo();

                if (!string.IsNullOrEmpty(json))
                {
                    List<Dispositivo> Lst = JsonConvert.DeserializeObject<List<Dispositivo>>(json);

                    foreach (var l in Lst)
                    {

                        if (l.estatus == true)
                        {
                            CardVentilador.BackgroundColor = Color.FromHex("#cdffbd");
                            SwEstado.IsOn = true;
                        }
                        else
                        {
                            CardVentilador.BackgroundColor = Color.FromHex("#ffbdbd");
                            SwEstado.IsOn = false;
                        }
                    }
                }

                await loadingDialog.DismissAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await MaterialDialog.Instance.SnackbarAsync(message: "Error: Consulte al administrador", msDuration: MaterialSnackbar.DurationLong);
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
                if (ex.InnerException != null) LblSubtitulo.Text = ex.InnerException.Message; else LblSubtitulo.Text = ex.Message;
                if (loadingDialog != null) await loadingDialog.DismissAsync();
            }
        }
    }
}