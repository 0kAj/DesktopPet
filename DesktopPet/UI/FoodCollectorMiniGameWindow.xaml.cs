using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using DesktopPet.Attribute;
using DesktopPet.Handlers;
using DesktopPet.Interfaces;
using DesktopPet.MiniGames;

namespace DesktopPet.UI;

[MiniGame("Food Collector")]
public partial class FoodCollectorMiniGameWindow : MiniGameWindow
{
    private readonly Random rand = new();
    private double playerSpeed = 10;
    private int score;

    public FoodCollectorMiniGameWindow(PetBrain petBrain) : base(petBrain)
    {
        InitializeComponent();
        WindowHelper.FitToScreen(this);

        // route all key events to petwindow
        KeyDown += (sender, args) => _brain.PetWindow.RaiseEvent(args);

        // gameTimer = new DispatcherTimer();
        // gameTimer.Interval = TimeSpan.FromMilliseconds(20);
        // gameTimer.Tick += GameLoop; //todo fix the slow down when pet moves
    }

    public override string GameName => "Food Collector";

    public override void Start()
    {
        // init Pet as playable
        _brain.InitFromMovementTemplate(PetBrain.MovementTemplate.BasicPetController);

        // gameTimer.Start(); //todo gamewindow
    }

    public override void End()
    {
        // gameTimer.Stop(); //todo gamewindow
        _brain.InitFromMovementTemplate(PetBrain.MovementTemplate.DefaultPet);
        Close();
    }

    protected override void Tick() //todo fix the slow down when pet moves
    {
        // Zufällig neues Futter erzeugen
        if (rand.Next(0, 30) == 1)
        {
            var food = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Gold
            };
            Canvas.SetLeft(food, rand.Next(0, (int)GameCanvas.ActualWidth - 20));
            Canvas.SetTop(food, 0);
            GameCanvas.Children.Add(food);
        }

        // Bewegung & Kollision
        for (var i = GameCanvas.Children.Count - 1; i >= 0; i--)
            if (GameCanvas.Children[i] is Ellipse food)
            {
                var top = Canvas.GetTop(food) + 5;
                Canvas.SetTop(food, top);

                var foodRect = new Rect(Canvas.GetLeft(food), top, food.Width, food.Height);
                var playerRect = _brain.PetWindow.GetCollisionRect();

                if (foodRect.IntersectsWith(playerRect))
                {
                    GameCanvas.Children.Remove(food);
                    score++;
                    _brain.PetWindow.debugLabel.Content = $"Score: {score}";
                }
                else if (top > GameCanvas.ActualHeight)
                {
                    GameCanvas.Children.Remove(food);
                }
            }
    }
}