namespace PropertyGame.Objects
{

    public abstract class Property
    {
        public Property(string name, int price, int revenue)
        {
            Name = name;
            Price = price;
            Revenue = revenue;
            TotalEarnings = 0;
        }

        public string Name { get; set; }

        public int Price { get; set; }

        public int Revenue { get; set; }

        public int TotalEarnings { get; set; }

        public int Sellvalue => Price / 2;
    }


    public class Box : Property
    {
        public Box() : base(nameof(Box), 5, 1)
        {

        }
    }

    public class Room : Property
    {
        public Room() : base(nameof(Room), 100, 10)
        {

        }
    }

    public class Lemonadestand : Property
    {
        public Lemonadestand() : base(nameof(Lemonadestand), 1000, 50)
        {

        }
    }

    public class Apartment : Property
    {
        public Apartment() : base(nameof(Apartment), 50000, 800)
        {

        }
    }

    public class House : Property
    {
        public House() : base(nameof(House), 800000, 2500)
        {

        }
    }

    public class Villa : Property
    {
        public Villa() : base(nameof(Villa), 2000000, 15000)
        {

        }
    }

    public class Castle : Property
    {
        public Castle() : base(nameof(Castle), 2000000, 15000)
        {

        }
    }

    public class Monument : Property
    {
        public Monument() : base(nameof(Monument), 100000000, 0)
        {

        }
    }
}
