namespace Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone
{
    public class Dish : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double foodValue { get; set; }
        public double Squirrels { get; set; }
        public double Oil { get; set; }
        public double Carbohydrates { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public int typeDishId { get; set; }
        public TypeDish TypeDish { get; set; }
        public bool isVegan { get; set; }
        public bool isFitness { get; set; }
    }
}
