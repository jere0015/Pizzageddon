namespace Pizzageddon.Client.Services
{
    public interface IPizzaService
    {
        List<Pizza> Pizzas { get; set; }
        Task GetPizzas();
        Task<Pizza> GetSinglePizza(int id);
        Task CreatePizza(Pizza pizza);
        Task UpdatePizza(Pizza pizza);
        Task DeletePizza(string id);
    }
}
