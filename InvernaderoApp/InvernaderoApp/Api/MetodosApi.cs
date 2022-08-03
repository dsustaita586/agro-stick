using InvernaderoApp.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InvernaderoApp.Api
{
    class MetodosApi
    {
        public static MetodosApi Instancia = new MetodosApi();

        public async Task<string> IniciarSesion(string usuario, string password)
        {
            string resultado = "";

            try
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                // content body
                var data = new Dictionary<string, object>() {
                    { "username", usuario },
                    { "password", password }
                };

                string json = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient(httpClientHandler);
                HttpResponseMessage response = await client.PostAsync(string.Concat(Utilidades.API_URL, "/auth"), httpContent);
                resultado = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                resultado = "";
                Debug.WriteLine(ex.Message);
            }

            return resultado;
        }

        public async Task<string> Historico(DateTime inicio, DateTime fin)
        {
            string resultado = "";

            try
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                // content body
                var data = new Dictionary<string, object>() {
                    { "inicio", inicio.ToString("yyyy-MM-dd") },
                    { "fin", fin.ToString("yyyy-MM-dd") }
                };

                string json = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient(httpClientHandler);
                HttpResponseMessage response = await client.PostAsync(string.Concat(Utilidades.API_URL, "/historico"), httpContent);
                resultado = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                resultado = "";
                Debug.WriteLine(ex.Message);
            }

            return resultado;
        }

        #region DISPOSITIVO

        public async Task<string> ListarEstatusDispositivo()
        {
            string resultado = "";

            try
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                HttpClient client = new HttpClient(httpClientHandler);
                HttpResponseMessage response = await client.GetAsync(string.Concat(Utilidades.API_URL, "/dispositivo"));
                resultado = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                resultado = "";
                Debug.WriteLine(ex.Message);
            }

            return resultado;
        }

        public async Task<string> ActualizarEstatusDispositivo()
        {
            string resultado = "";

            try
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                // content body
                var data = new Dictionary<string, object>() { };

                string json = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient(httpClientHandler);
                HttpResponseMessage response = await client.PostAsync(string.Concat(Utilidades.API_URL, "/dispositivo"), httpContent);
                resultado = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                resultado = "";
                Debug.WriteLine(ex.Message);
            }

            return resultado;
        }

        #endregion
    }
}
