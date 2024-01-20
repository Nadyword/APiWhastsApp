using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TestAPIWhastasAppv2.Interfases;
using TestWhatsApp.Models;

namespace TestAPIWhastasAppv2.Service.WhatsAppCloud
{
    
    public class EnviarMensaje : IEnviarMensaje
    {
        public async Task<bool> Execute(object model)
        {
            DataWhatsApp datos = new DataWhatsApp();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
            string uri = datos.Endponit + "v18.0/" + datos.NumeroTelefonoID + "messages";
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {datos.Tokker}");

                var response = await client.PostAsync(uri, content);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
