using InvernaderoApp.Api;
using InvernaderoApp.Dtos;
using InvernaderoApp.Utils;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class HistoricoPage : ContentPage
    {

        public HistoricoPage()
        {
            InitializeComponent();

            DpInicio.MaximumDate = DateTime.Today;
            DpFin.MaximumDate = DateTime.Today;

            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void BtnBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Verificando los datos, por favor espere...", configuration: Utilidades.Instancia.loadingDialogConfiguration);
                string json = await MetodosApi.Instancia.Historico(DpInicio.Date, DpFin.Date);

                if (!string.IsNullOrEmpty(json))
                {
                    Debug.WriteLine(json);
                    Historico historico = JsonConvert.DeserializeObject<Historico>(json);

                    List<ChartEntry> lstTemp = new List<ChartEntry>();
                    List<ChartEntry> lstH = new List<ChartEntry>();
                    List<ChartEntry> lstP = new List<ChartEntry>();
                    foreach (var tem in historico.temperatura)
                    {
                        lstTemp.Add(new ChartEntry(128)
                        {
                            Label = tem.fecha.ToString("dd/MM/yyyy"),
                            ValueLabel = tem.dato.ToString("#.##"),
                            Color = SKColor.Parse(Utilidades.Instancia.GetRandomHex())
                        });
                    }
                    chartViewTemp.Chart = new LineChart() { Entries = lstTemp };

                    // humedad
                    foreach (var tem in historico.humedad)
                    {
                        lstH.Add(new ChartEntry(128)
                        {
                            Label = tem.fecha.ToString("dd/MM/yyyy"),
                            ValueLabel = tem.dato.ToString("#.##"),
                            Color = SKColor.Parse(Utilidades.Instancia.GetRandomHex())
                        });
                    }
                    chartViewHum.Chart = new LineChart() { Entries = lstH };

                    // ppm
                    foreach (var tem in historico.ppm)
                    {
                        lstP.Add(new ChartEntry(128)
                        {
                            Label = tem.fecha.ToString("dd/MM/yyyy"),
                            ValueLabel = tem.dato.ToString("#.##"),
                            Color = SKColor.Parse(Utilidades.Instancia.GetRandomHex())
                        });
                    }
                    chartViewP.Chart = new LineChart() { Entries = lstP };

                }

                await loadingDialog.DismissAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await MaterialDialog.Instance.SnackbarAsync(message: "Error: Consulte al administrador", msDuration: MaterialSnackbar.DurationLong);
            }
        }
    }
}