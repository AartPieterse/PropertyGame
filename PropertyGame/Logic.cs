using PropertyGame.Objects;

namespace PropertyGame
{
    public class Logic
    {
        private readonly List<Property> AllProperties = new List<Property>();

        public Logic()
        {
            AllProperties.Add(new Box());
            AllProperties.Add(new Room());
            AllProperties.Add(new Lemonadestand());
            AllProperties.Add(new Apartment());
            AllProperties.Add(new House());
            AllProperties.Add(new Villa());
            AllProperties.Add(new Castle());
            AllProperties.Add(new Monument());
        }

        public string GetRules()
        {
            return "Here come the rules: \n" +

            "The goal of the game is to aqquire the befamed Monument. To achieve this you have to buy" +
                "the cheaper properties. They will earn income every round" +

            "\nThere are 4 actions to do, buy, sell, show prices and show owned properties." +
                "\nThose actions resemble the 4 keywords: 'buy', 'sell', 'prices' and 'properties'. " +
                "Of these 'buy' and 'sell' must be followed by the name of a property which has to be bought or sold. \n\n" +
                "Example: 'buy house' buys a house, and 'sell room' sells a room.";
        }

        public IEnumerable<Property> CalculateRevenue(IEnumerable<Property> properties)
        {
            int earnings = 0;

            foreach (Property prop in properties)
            {
                prop.TotalEarnings += prop.Revenue;
                earnings += prop.Revenue;
            }

            return properties;
        }

        public IEnumerable<Property> GetPropertiesWithPrices()
        {
            return AllProperties;
        }

    }
}
