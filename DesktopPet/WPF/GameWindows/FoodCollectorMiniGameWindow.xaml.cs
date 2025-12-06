using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DesktopPet.Attribute;
using DesktopPet.Engine;
using DesktopPet.Handlers;

namespace DesktopPet.WPF.GameWindows;

[MiniGame("Food Collector")]
public partial class FoodCollectorMiniGameWindow : MiniGameWindow
{
    private readonly Random _rand = new();
    private int _foodScore;

    public FoodCollectorMiniGameWindow(PetBrain petBrain) : base(petBrain) //todo I NEED VIEWMODEL
    {
        InitializeComponent();
        SizingHelper.FitToScreen(this);

        // route all key events to petwindow
        KeyDown += (sender, args) => _brain.PetWindow.RaiseEvent(args);

        SetDelta(20);

        TDisplay.Timer.Set(30);
        TDisplay.Timer.Timeout += () => End();

        TickStart += () => TDisplay.Timer.Start();
        TickStop += () => TDisplay.Timer.Stop();
    }

    public override string GameName => "Food Collector";
    protected override Canvas MiniGameUiCanvas => UiCanvas;

    public override void Start()
    {
        // init Pet as playable
        _brain.InitFromMovementTemplate(PetBrain.MovementTemplate.BasicPetController);

        StartTicking();
    }

    protected override void End()
    {
        base.End();
        StopTicking();
        _brain.InitFromMovementTemplate(PetBrain.MovementTemplate.DefaultPet);
        var rewardsWindow = new RewardsWindow(_foodScore, 0);
        rewardsWindow.Show();
        Close();
    }

    protected override void Tick()
    {
        // create food at random pos
        if (_rand.Next(0, 30) == 1)
        {
            var food = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(
                        new Uri("pack://application:,,,/Assets/Sprites/Food/apple.png"))
                }
            };
            Canvas.SetLeft(food, _rand.Next(0, (int)GameCanvas.ActualWidth - 20));
            Canvas.SetTop(food, 0);
            GameCanvas.Children.Add(food);
        }

        // food collision
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
                    _foodScore++;
                    CollectedFood += 1;
                    // _brain.PetWindow.debugLabel.Content = $"foodScore: {_foodScore}";
                    TDisplay.Timer.AddRemaining(1);
                }
                else if (top > GameCanvas.ActualHeight)
                {
                    GameCanvas.Children.Remove(food);
                }
            }
    }
}