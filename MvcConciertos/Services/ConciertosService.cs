using MvcConciertos.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace MvcConciertos.Services
{

    public class ConciertosService
    {
        private readonly HttpClient client;
        private readonly MediaTypeWithQualityHeaderValue headers;
        private readonly IConfiguration configuration;

        public ConciertosService(HttpClient httpClient, IConfiguration configuration)
        {
            client = httpClient;
            headers = new MediaTypeWithQualityHeaderValue("application/json");
            this.configuration = configuration;
        }

        public async Task<string> PreguntarIaAsync(string pregunta)
        {
            string baseUrl = configuration["ApiUrls:ApiIaUrl"] ?? "";
            string requestUrl = $"{baseUrl}?pregunta={Uri.EscapeDataString(pregunta)}";

            using (HttpResponseMessage response = await client.GetAsync(requestUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                return "Error al comunicarse con la IA.";
            }
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