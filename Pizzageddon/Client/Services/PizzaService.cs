using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Pizzageddon.Client.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public PizzaService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public List<Pizza> Pizzas { get; set; }

        private async Task SetPizzas(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Pizza>>();
            Pizzas = response;
            _navigationManager.NavigateTo("/");
        }
        public async Task CreatePizza(Pizza pizza)
        {
            var result = await _http.PostAsJsonAsync("api/pizza", pizza);
            await SetPizzas(result);
        }

        public async Task DeletePizza(string id)
        {
            var result = await _http.DeleteAsync($"api/pizza/{id}");
            var response = await result.Content.ReadFromJsonAsync<List<Pizza>>();
            await SetPizzas(result);
        }

        public async Task GetPizzas()
        {
            var result = await _http.GetFromJsonAsync<List<Pizza>>("api/pizza");
            if (result != null)
                Pizzas = result;
        }

        public async Task<Pizza> GetSinglePizza(int id)
        {
            var result = await _http.GetFromJsonAsync<Pizza>($"api/pizza/{id}");
            if (result != null)
                return result;
            throw new Exception("Pizza not found...");
        }

        public async Task UpdatePizza(Pizza pizza)
        {
            var result = await _http.PutAsJsonAsync($"api/pizza/{pizza.Id}", pizza);
            var response = result.Content.ReadFromJsonAsync<List<Pizza>>();
            await SetPizzas(result);
        }
    }
}
