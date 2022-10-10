using PropertyGame.Objects;

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

                Console.WriteLine("What do you want to do? \n Buy properties or wait a round?");

                // Buying properties
                line = Console.ReadLine();
                string[] lines = line.Split(" ");

                if (lines[0] == "buy")
                {
                    foreach (Property p in properties)
                    {
                        if (lines[1].ToLower() == p.Name.ToLower())
                        {
                            // THIS DOES NOT WORK 
                            BuyProperty(p);
                            break;
                        }
                    }
                }
                if (lines[0] == "sell")
                {
                    foreach (Property p in properties)
                    {
                        if (lines[1].ToLower() == nameof(p).ToLower())
                        {
                            SellProperty(p);
                        }
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

                counter++;
            }

            Console.WriteLine("Well played!");
            Console.WriteLine($"It took you {counter} turns to buy the monument!");
        }

        static void Rules()
        {
            Console.WriteLine("Here comes the rules: ");
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
            foreach (Property prop in properties)
            {
                Console.WriteLine($" {prop.Price} ({prop.TotalEarnings}) \n");
            }
        }

        void PrintProperties()
        {
            IEnumerable<Property> props = wallet.Properties.OrderBy(property => property.Price);

            foreach (Property prop in props)
            {
                Console.WriteLine(prop.Name + " ( +" + prop.TotalEarnings + ")");
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
            if (wallet.Properties.Contains(property))
            {
                wallet.Properties.Remove(property);

                wallet.Value += property.Sellvalue;
            
            } else
            {
                Console.WriteLine("Can't sell, property is not owned");
            }
        }
    }
}
