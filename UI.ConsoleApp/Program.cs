using PropertyGame;
using PropertyGame.Objects;

namespace UI.ConsoleApp
{
    internal class ConsoleApp
    {
        private static void Main(string[] args)
        {
            Wallet wallet = new() { Value = 10 };

            Logic logic = new();

            Game game = new Game(logic, wallet);
            game.StartGame();
        }
    }
}
