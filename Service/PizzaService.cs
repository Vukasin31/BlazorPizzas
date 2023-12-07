using ContosoPizzaBlazor.Datas;
using MongoDB.Driver;

namespace ContosoPizzaBlazor.Service
{
    public interface IPizzaService
    {
        void SaveOrUpdrate(Pizza pizza);
        Pizza GetPizza(string pizzaId);
        List<Pizza> GetPizzas();
        string Delete(string pizzaId);
    }
    public class PizzaService : IPizzaService
    {
        private MongoClient _mongoClient = null;
        private IMongoDatabase _database = null;
        private IMongoCollection<Pizza> _pizzaTable = null;
        public PizzaService()
        {
            _mongoClient = new MongoClient("mongodb://localhost:27017");
            _database = _mongoClient.GetDatabase("ContosoPizza");
            _pizzaTable = _database.GetCollection<Pizza>("Pizzas");
        }
        public string Delete(string pizzaId)
        {
            _pizzaTable.DeleteOne(x => x.Id == pizzaId);
            return "Deleted";
        }

        public List<Pizza> GetPizzas()
        {
            return _pizzaTable.Find(FilterDefinition<Pizza>.Empty).ToList();
        }

        public Pizza GetPizza(string pizzaId)
        {
            return _pizzaTable.Find(x => x.Id == pizzaId).FirstOrDefault();
        }

        public void SaveOrUpdrate(Pizza pizza)
        {
            var pizzaObj = _pizzaTable.Find(x => x.Id == pizza.Id).FirstOrDefault();
            if (pizzaObj != null)
            {
                 _pizzaTable.ReplaceOne(x => x.Id == pizza.Id, pizza);
            }
            else
            {
                _pizzaTable.InsertOne(pizza);
            }
        }
    }
}
