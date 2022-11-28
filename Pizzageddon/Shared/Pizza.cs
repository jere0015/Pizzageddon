using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzageddon.Shared
{
    public class Pizza
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Meat_toppings { get; set; } = null!;
        public string Vegetables { get; set; } = null!;
        public string Allergies { get; set; } = null!;
        public string Diets { get; set; } = null!;
    }
}
