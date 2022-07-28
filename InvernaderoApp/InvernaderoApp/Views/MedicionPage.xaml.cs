using InvernaderoApp.Utils;
using OpenNETCF.MQTT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
    public partial class MedicionPage : ContentPage
    {
        IMaterialModalPage loadingDialog;
        Random random = new Random();
        public MedicionPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            Debug.WriteLine($"Method OnAppearing() ");
            await InitMQTTV2();
        }

        private async Task InitMQTTV2()
        {
            try
            {
                // loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Verificando los datos, por favor espere...", configuration: Utilidades.Instancia.loadingDialogConfiguration);
                var configuration = new MqttConfiguration
                {
                    Port = Utilidades.PORT
                };
                var client = await MqttClient.CreateAsync(Utilidades.HOST, configuration);
                var sessionState = await client.ConnectAsync(new MqttClientCredentials(clientId: "AppMovil"));

                // temp
                await client.SubscribeAsync("room/temp", MqttQualityOfService.ExactlyOnce); //QoS2

                // humedad
                await client.SubscribeAsync("room/humedad", MqttQualityOfService.ExactlyOnce); //QoS2

                // humedad
                await client.SubscribeAsync("room/ppm", MqttQualityOfService.ExactlyOnce); //QoS2

                client.MessageStream.Subscribe(msg => {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Debug.WriteLine(msg.Topic);
                        if (msg.Topic == "room/temp")
                        {
                            ValorTemp.Value = float.Parse(Encoding.UTF8.GetString(msg.Payload), CultureInfo.InvariantCulture);
                            LblValor.Text = $"{ Encoding.UTF8.GetString(msg.Payload) } °C";
                        }
                        else if (msg.Topic == "room/humedad")
                        {
                            ValorHumedad.Value = float.Parse(Encoding.UTF8.GetString(msg.Payload), CultureInfo.InvariantCulture);
                            LblValorHumedad.Text = $"{ Encoding.UTF8.GetString(msg.Payload) } %";
                        }
                        else if (msg.Topic == "room/ppm")
                        {
                            ValorPpm.Value = float.Parse(Encoding.UTF8.GetString(msg.Payload), CultureInfo.InvariantCulture);
                            LblValorPpm.Text = $"{ Encoding.UTF8.GetString(msg.Payload) } PPM";
                        }

                    });
                });


                // await loadingDialog.DismissAsync();

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
                if (loadingDialog != null) await loadingDialog.DismissAsync();
                // await MaterialDialog.Instance.SnackbarAsync(message: "Error: Consulte al administrador", msDuration: MaterialSnackbar.DurationLong);
            }
        }
    }
}