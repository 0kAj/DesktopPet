using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DesktopPet.Attribute;
using DesktopPet.Data.Pet;
using DesktopPet.Handlers;
using DesktopPet.MiniGames;

namespace DesktopPet.UI.GameWindows;

[MiniGame("Food Collector")]
public partial class FoodCollectorMiniGameWindow : MiniGameWindow
{
    private readonly Random rand = new();
    private double playerSpeed = 10;
    private int score;

    public FoodCollectorMiniGameWindow(PetBrain petBrain) : base(petBrain)
    {
        InitializeComponent();
        SizingHelper.FitToScreen(this);

        // route all key events to petwindow
        KeyDown += (sender, args) => _brain.PetWindow.RaiseEvent(args);
        
        SetDelta(20);
        
        UpdateUi();
        
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

    public override void End()
    {
        base.End();
        StopTicking();
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
                Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(
                        new Uri("pack://application:,,,/Assets/Sprites/Food/apple.png"))
                }
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
                    CollectedFood += 1;
                    TDisplay.Timer.AddRemaining(2);
                    UpdateUi();
                }
                else if (top > GameCanvas.ActualHeight)
                {
                    GameCanvas.Children.Remove(food);
                }
            }
    }

    private void UpdateUi()
    {
        //update score
        CDisplay.Thirst_tb.Text = PetManager.Instance.GetAttribute(_brain.Name, "collectedThirst", "0");
        
        CDisplay.Food_tb.Text = PetManager.Instance.GetAttribute(_brain.Name, "collectedFood", "0");
    }
}