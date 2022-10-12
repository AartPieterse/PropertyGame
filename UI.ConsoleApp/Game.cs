using PropertyGame;
using PropertyGame.Objects;

namespace UI.ConsoleApp
{
    internal class Game
    {
        private readonly Logic logic;
        private Wallet wallet;

        public Game(Logic logic, Wallet wallet)
        {
            this.logic = logic;
            this.wallet = wallet;
        }

        public void StartGame()
        {
            Console.WriteLine("Hi!\nStarting game...");

            Console.WriteLine("Welcome!");
            Console.WriteLine("Type 'start' to begin the game or 'rules' if you do not know how to play this game.");
            Console.WriteLine("Type 'exit' to exit");

            while (true)
            {
                string input = Console.ReadLine() ?? "no command";

                if (input.ToLower() == "rules")
                {
                    Rules();
                }

                if (input.ToLower() == "start")
                {
                    GameCore();
                }

                if (input.ToLower() == "exit")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Commmand not recognised");
                }
            }
        }

        private void GameCore()
        {
            int counter = 0;
            string line = "";

            while (!wallet.Properties.Exists(property => property.Name == "Monument"))
            {
                // Calculate and print the revenue of all properties
                int revenue = CalculateRevenue();
                wallet.Value += revenue;
                Console.WriteLine($"Balance: {wallet.Value} (+ {revenue})");

                Console.WriteLine("Waiting command...");

                // New command
                line = Console.ReadLine() ?? "no command";
                string[] lines = new string[2];
                lines = line.Split(" ");

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

                // Print an empty line
                Console.WriteLine();

                // Update turn counter
                counter++;
            }

            Console.WriteLine("Well played!");
            Console.WriteLine($"It took you {counter} turns to buy the monument!");
        }

        private void Rules()
        {
            Console.WriteLine(logic.GetRules());
        }

        public int CalculateRevenue()
        {
            int earnings = 0;

            foreach (Property prop in wallet.Properties)
            {
                prop.TotalEarnings += prop.Revenue;
                earnings += prop.Revenue;
            }

            return earnings;
        }

        private void BuyProperty(Property property)
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

        private void SellProperty(Property property)
        {
            if (!wallet.Properties.Exists(p => p.Name == property.Name))
            {
                Console.WriteLine($"You can't sell {property.Name} for you don't have one!");
                return;
            }

            if (wallet.Properties.Remove(wallet.Properties.First<Property>(p => p.Name == property.Name)))
            {
                wallet.Value += property.Sellvalue;
            }
        }

        private void PrintPrices()
        {
            Console.WriteLine("{0,-20} {1,7}\n", "Property", "Prices");

            foreach (Property prop in logic.GetPropertiesWithPrices())
            {
                Console.WriteLine("{0, -18} {1, 9}", prop.Name, prop.Price);
            }
        }

        private void PrintProperties()
        {
            IEnumerable<Property> props = wallet.Properties.OrderBy(property => property.Price);

            Console.WriteLine("{0, -15} {1, 5}\n", "Property", "Total earnings");

            foreach (Property prop in props)
            {
                Console.WriteLine("{0, -15} {1, 0}", prop.Name, prop.TotalEarnings);
            }
        }
    }
}
