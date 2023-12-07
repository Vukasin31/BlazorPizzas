using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;


namespace ContosoPizzaBlazor.Datas
{
    public class Pizza
    {      
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string? PizzaName { get; set; }
        public bool IsGlutenFree { get; set; }
    }
}