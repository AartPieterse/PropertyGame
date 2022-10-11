using PropertyGame.Objects;
using System.Linq;

namespace PropertyGame
{
    internal class Logic
    {
        readonly Wallet wallet;

        readonly List<Property> properties = new List<Property>();

        public Logic()
        {
            properties.Add(new Box());
            properties.Add(new Room());
            properties.Add(new Lemonadestand());
            properties.Add(new Apartment());
            properties.Add(new House());
            properties.Add(new Villa());
            properties.Add(new Castle());
            properties.Add(new Monument());

            wallet = new()
            {
                Value = 10
            };

            Console.WriteLine("Starting game...");

            Console.WriteLine("Welcome!");
            Console.WriteLine("Type 'start' to begin the game or 'rules' if you do not know how to play this game.");
            Console.WriteLine("Type 'exit' to exit");

            string? input = Console.ReadLine();
            if (input == "rules") Rules();
            if (input == "start") Start();
            if (input == "exit") Environment.Exit(0);
            else Console.WriteLine("Commmand not recognised");  

            Start();

            Console.ReadKey();
        }

        void Start()
        {
            Console.WriteLine("Let's start the game.");

            int counter = 0;
            string? line = "";

            while (!wallet.Properties.Exists(property => property.Name == "Monument"))
            {
                // Revenue
                int revenue = CalculateRevenue();
                wallet.Value += revenue;
                Console.WriteLine($"Balance: {wallet.Value} (+ {revenue})");

                Console.WriteLine("Waiting command...");

                // Buying properties
                line = Console.ReadLine();
                string[] lines = line.Split(" ");

                if (lines[0] == "buy")
                {
                    switch (lines[1].ToLower())
                    {
                        case "box":
                            BuyProperty(new Box());
                            break;
                        case "b":
                            BuyProperty(new Box());
                            break;
                        case "room":
                            BuyProperty(new Room());
                            break;
                        case "r":
                            BuyProperty(new Room());
                            break;
                        case "lemonadestand":
                            BuyProperty(new Lemonadestand());
                            break;
                        case "l":
                            BuyProperty(new Lemonadestand());
                            break;
                        case "apartment":
                            BuyProperty(new Apartment());
                            break;
                        case "a":
                            BuyProperty(new Apartment());
                            break;
                        case "house":
                            BuyProperty(new House());
                            break;
                        case "h":
                            BuyProperty(new House());
                            break;
                        case "villa":
                            BuyProperty(new Villa());
                            break;
                        case "v":
                            BuyProperty(new Villa());
                            break;
                        case "castle":
                            BuyProperty(new Castle());
                            break;
                        case "c":
                            BuyProperty(new Castle());
                            break;
                        case "monument":
                            BuyProperty(new Monument());
                            break;
                        case "m":
                            BuyProperty(new Monument());
                            break;
                        default:
                            Console.WriteLine("Invalid command");
                            break;
                    }
                }
                else if (lines[0] == "sell")
                {
                    switch (lines[1].ToLower())
                    {
                        case "box":
                            SellProperty(new Box());
                            break;
                        case "b":
                            SellProperty(new Box());
                            break;
                        case "room":
                            SellProperty(new Room());
                            break;
                        case "r":
                            SellProperty(new Room());
                            break;
                        case "lemonadestand":
                            SellProperty(new Lemonadestand());
                            break;
                        case "l":
                            SellProperty(new Lemonadestand());
                            break;
                        case "apartment":
                            SellProperty(new Apartment());
                            break;
                        case "a":
                            SellProperty(new Apartment());
                            break;
                        case "house":
                            SellProperty(new House());
                            break;
                        case "h":
                            SellProperty(new House());
                            break;
                        case "villa":
                            SellProperty(new Villa());
                            break;
                        case "v":
                            SellProperty(new Villa());
                            break;
                        case "castle":
                            SellProperty(new Castle());
                            break;
                        case "c":
                            SellProperty(new Castle());
                            break;
                        case "monument":
                            SellProperty(new Monument());
                            break;
                        case "m":
                            SellProperty(new Monument());
                            break;
                        default:
                            Console.WriteLine("Invalid command");
                            break;
                    }
                }
                else if (lines[0] == "prices")
                {
                    PrintPrices();
                }
                else if (lines[0] == "properties")
                {
                    PrintProperties();
                }
                else if (lines[0] == "exit")
                {
                    Environment.Exit(0);
                }

                Console.WriteLine();

                counter++;
            }

            Console.WriteLine("Well played!");
            Console.WriteLine($"It took you {counter} turns to buy the monument!");
        }

        static void Rules()
        {
            Console.WriteLine("Here come the rules: \n");

            Console.WriteLine("The goal of the game is to aqquire the befamed Monument. To achieve this you have to buy" +
                "the cheaper properties. They will earn income every round");

            Console.WriteLine("There are 4 actions to do, buy, sell, show prices and show owned properties.");
            Console.WriteLine("Those actions resemble the 4 keywords: 'buy', 'sell', 'prices' and 'properties'. " +
                "Of these 'buy' and 'sell' must be followed by the name of a property which has to be bought or sold. \n\n" +
                "Example: 'buy house' buys a house, and 'sell room' sells a room.");

        }

        int CalculateRevenue()
        {
            int earnings = 0;

            foreach (Property prop in wallet.Properties)
            {
                prop.TotalEarnings += prop.Revenue;
                earnings += prop.Revenue;
            }

            return earnings;
        }

        void PrintPrices()
        {
            Console.WriteLine("{0,-20} {1,5}\n", "Property", "Total earnings");

            foreach (Property prop in properties)
            {
                Console.WriteLine("{0, -20} {1, 9}", prop.Name, prop.Price);
            }
        }

        void PrintProperties()
        {
            IEnumerable<Property> props = wallet.Properties.OrderBy(property => property.Price);

            Console.WriteLine("{0, -15} {1, 5}\n", "Property", "Total earnings");

            foreach (Property prop in props)
            {
                Console.WriteLine("{0, -15} {1, 0}", prop.Name, prop.TotalEarnings);
            }
        }

        void BuyProperty(Property property)
        {
            if (property.Price <= wallet.Value)
            {
                wallet.Properties.Add(property);

                wallet.Value -= property.Price;

            }
            else
            {
                Console.WriteLine($"Not enough money to buy {property.Name}");
            }
        }

        void SellProperty(Property property)
        {
            // Check wallet on existing property --> Delete one of them            

            if (wallet.Properties.Remove(wallet.Properties.First<Property>(p => p.Name == property.Name)))
            {
                wallet.Value += property.Sellvalue;
            }
            else
            {
                Console.WriteLine("Can't sell, property is not owned");
            }
        }
    }
}
