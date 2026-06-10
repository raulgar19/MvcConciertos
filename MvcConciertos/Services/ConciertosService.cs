using MvcConciertos.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MvcConciertos.Services
{

    public class ConciertosService
    {
        private readonly HttpClient client;
        private readonly MediaTypeWithQualityHeaderValue headers;

        public ConciertosService(HttpClient httpClient)
        {
            client = httpClient;
            headers = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<CategoriaEvento>> GetCategoriasAsync()
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(headers);

            using (HttpResponseMessage response = await client.GetAsync("api/conciertos/categorias"))
            {
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<CategoriaEvento>>(json) ?? new List<CategoriaEvento>();
                }
                return new List<CategoriaEvento>();
            }
        }

        public async Task<List<Evento>> GetEventosAsync()
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(headers);

            using (HttpResponseMessage response = await client.GetAsync("api/conciertos/eventos"))
            {
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Evento>>(json) ?? new List<Evento>();
                }
                return new List<Evento>();
            }
        }

        public async Task<List<Evento>> GetEventosByCategoriaAsync(int idCategoria)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(headers);

            using (HttpResponseMessage response = await client.GetAsync($"api/conciertos/eventos/categoria/{idCategoria}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Evento>>(json) ?? new List<Evento>();
                }
                return new List<Evento>();
            }
        }
    }
}