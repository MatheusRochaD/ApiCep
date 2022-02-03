using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace ApiCep.Conexoes
{
    public static class ConsumoApi
    {
        public static readonly HttpClient _client = new HttpClient();

        public static T Get<T>(string url)
        {
            var response = _client.GetAsync(url).GetAwaiter().GetResult();

            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<T>(content);
            else
                throw new InvalidOperationException("Serviço de consulta ao cep indisponível no momento.");
        }
    }
}
