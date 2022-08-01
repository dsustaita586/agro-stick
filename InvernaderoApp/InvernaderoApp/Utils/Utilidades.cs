using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace InvernaderoApp.Utils
{
    class Utilidades
    {
        public const string API_URL = "http://192.168.1.125:3000/api";
        private readonly static Utilidades _instancia = new Utilidades();
        public const string HOST = "proxy16.rt3.io";
        public const int PORT = 39480;
        

        public static Utilidades Instancia
        {
            get
            {
                return _instancia;
            }
        }
        public string QuitarNulos(Object val)
        {
            string cadena = "";

            if (val == null)
                return cadena;

            cadena = val.ToString() == "null" ? "" : val.ToString();

            return cadena;
        }

        public bool CheckURLValid(string source)
        {
            Uri uriResult;
            return Uri.TryCreate(source, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
        }

        public MaterialAlertDialogConfiguration alertDialogConfiguration = new MaterialAlertDialogConfiguration
        {
            BackgroundColor = Color.FromHex("#1d2d50"),
            TitleTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY),
            MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8),
            TintColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY),
            CornerRadius = 8,
            ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32),
            ButtonAllCaps = false
        };

        public MaterialLoadingDialogConfiguration loadingDialogConfiguration = new MaterialLoadingDialogConfiguration
        {
            BackgroundColor = Color.FromHex("#c7a100"),
            MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8),
            TintColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY),
            CornerRadius = 8,
            ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32)
        };

        public MaterialSimpleDialogConfiguration simpleDialogConfiguration = new MaterialSimpleDialogConfiguration
        {
            BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY),
            TitleTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY),
            TextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8),
            CornerRadius = 8,
            ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32)
        };

        public MaterialSnackbarConfiguration snackbarConfiguration = new MaterialSnackbarConfiguration
        {
            BackgroundColor = Color.FromHex("#ad0000"),
            TintColor = Color.White,
            MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8)
        };

        public MaterialSnackbarConfiguration snackbarSuccess = new MaterialSnackbarConfiguration
        {
            BackgroundColor = Color.FromHex("#038510"),
            TintColor = Color.White,
            MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8)
        };

        public bool CheckConnection()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                return true;
            }

            return false;
        }

        public int VerificarDia()
        {
            int verifica = 0;
            //Creo la varible 
            string hora = DateTime.Now.ToString("HH");

            //Convierto hora a entero
            int time = Convert.ToInt32(hora);

            if (time == 4 || time == 5 || time == 6)
            {
                verifica = 0;   // madrugada
            }
            else if (time == 7 || time == 8 || time == 9 || time == 10 || time == 11 || time == 12 || time == 13 || time == 14 || time == 15 || time == 16 || time == 17 || time == 18)
            {
                verifica = 1;   // dia
            }
            else if (time == 19 || time == 20)
            {
                verifica = 2;   // tarde
            }
            else if (time == 21 || time == 22 || time == 0 || time == 1 || time == 2 || time == 3)
            {
                verifica = 3;   // noche
            }

            return verifica;
        }

        public bool verificarInternet()
        {
            var current = Connectivity.NetworkAccess;

            if (current == Xamarin.Essentials.NetworkAccess.Internet)
            {
                return true;
            }
            return false;
        }

        public bool VerificarImagen(string url)
        {
            bool respuesta = false;

            if (url == "-1")
                return false;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "HEAD";

            try
            {
                request.GetResponse();
                respuesta = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                respuesta = false;
            }

            return respuesta;
        }

        public string GetRandomHex()
        {
            string color = "";

            var random = new Random();
            color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"

            return color;
        }
    }
}
