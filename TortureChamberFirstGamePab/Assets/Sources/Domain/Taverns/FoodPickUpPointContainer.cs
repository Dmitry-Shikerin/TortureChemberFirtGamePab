namespace Sources.Domain.Taverns
{
    public class FoodPickUpPointContainer
    {
        public FoodPickUpPointContainer(
            FoodPickUpPoint beer,
            FoodPickUpPoint bread,
            FoodPickUpPoint meat,
            FoodPickUpPoint soup,
            FoodPickUpPoint wine)
        {
            Beer = beer;
            Bread = bread;
            Meat = meat;
            Soup = soup;
            Wine = wine;
        }

        public FoodPickUpPoint Beer { get; }
        public FoodPickUpPoint Bread { get; }
        public FoodPickUpPoint Meat { get; }
        public FoodPickUpPoint Soup { get; }
        public FoodPickUpPoint Wine { get; }
    }
}