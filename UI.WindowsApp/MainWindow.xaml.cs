using PropertyGame;
using PropertyGame.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace UI.WindowsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer timer;

        private Logic logic;

        private Wallet wallet;

        private ObservableCollection<Property> OCProperties = new();

        public MainWindow()
        {
            logic = new();

            InitializeComponent();

            wallet = new() { Value = 10, Properties = new List<Property>() };

            OCProperties.CollectionChanged += HandleChange;

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            (IEnumerable<Property> list, int earnings) = logic.CalculateRevenue(OCProperties);

            wallet.Value += earnings;
            OCProperties = (ObservableCollection<Property>)list;

            // Updating the Label which displays the current second
            ValueLabel.Content = wallet.Value;

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            ObservableCollection<Property> list = sender as ObservableCollection<Property>;

            Property? changedProperty = list[^1];

            switch (changedProperty.Name)
            {
                case "Box":
                    LabelBoxCount.Content = LabelBoxCount.Content += "|";
                    break;
                case "Room":
                    LabelRoomCount.Content = LabelRoomCount.Content += "|";
                    break;
                case "Lemonadestand":
                    LabelLemonadestandCount.Content = LabelLemonadestandCount.Content += "|";
                    break;
            }
        }

        private void BuyBox_Click(object sender, RoutedEventArgs e)
        {
            Box box = new();

            wallet.Value -= box.Price;

            OCProperties.Add(box);

            LabelBoxCount.Content = "Owned: " + 1;
        }

        private void BuyProperty_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            string[] content = button.Content.ToString().Split(" ");

            Property newProp = null;
            switch(content[1])
            {
                case "Box":
                    newProp = new Box();
                    break;
                case "Room":
                    newProp = new Room();
                    break;
                case "Lemonadestand":
                    newProp = new Lemonadestand();
                    break;
            }

            OCProperties.Add(newProp);
        }

        private void BuyLemonadestand_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
