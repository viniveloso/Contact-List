using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ContactList.Application.Dtos;
using ContactList.Application.Interfaces;

namespace ContactList.Infrastructure.Services
{
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://viacep.com.br/ws/");
        }

        public async Task<ViaCepResponseDto> GetAddressByCepAsync(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                return null;
            }

            var cleanCep = cep.Replace("-", "").Replace(".", "").Trim();

            try
            {
                var response = await _httpClient.GetAsync($"{cleanCep}/json/");
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var viaCepResponse = JsonSerializer.Deserialize<ViaCepResponseDto>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (viaCepResponse != null && viaCepResponse.Erro)
                {
                    return null;
                }
                return viaCepResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao consultar ViaCep: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Erro ao desserializar resposta do ViaCep: {ex.Message}");
                return null;
            }
        }
    }
}
