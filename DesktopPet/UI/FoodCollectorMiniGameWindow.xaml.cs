using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using DesktopPet.Interfaces;

namespace DesktopPet.UI;

public partial class FoodCollectorMiniGameWindow : Window, IMiniGame
{
    private Rectangle player;
    private DispatcherTimer gameTimer;
    private Random rand = new();
    private double playerSpeed = 10;
    private int score = 0;
    
    private PetWindow _petWindow;

    public FoodCollectorMiniGameWindow(PetWindow petWindow)
    {
        _petWindow = petWindow;
        // route all key events to petwindow
        KeyDown += (sender, args) =>  _petWindow.RaiseEvent(args);
        InitializeComponent();
    }

    public void Start()
    {
        // init Pet as playable
        _petWindow.InitFromTemplate(PetWindow.MovementTemplate.BasicPetController);
        // Canvas.SetLeft(player, 400);
        // Canvas.SetTop(player, 600);
        // GameCanvas.Children.Add(player);
        player = _petWindow.pet;

        // Game-Loop
        gameTimer = new DispatcherTimer();
        gameTimer.Interval = TimeSpan.FromMilliseconds(20);
        gameTimer.Tick += GameLoop;
        gameTimer.Start();
    }
    
    private void GameLoop(object sender, EventArgs e)
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
        for (int i = GameCanvas.Children.Count - 1; i >= 0; i--)
        {
            if (GameCanvas.Children[i] is Ellipse food)
            {
                double top = Canvas.GetTop(food) + 5;
                Canvas.SetTop(food, top);

                Rect foodRect = new Rect(Canvas.GetLeft(food), top, food.Width, food.Height);
                // Rect playerRect = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height); // todo fix
                Rect playerRect = _petWindow.GetPetRect();
                
                if (foodRect.IntersectsWith(playerRect))
                {
                    GameCanvas.Children.Remove(food);
                    score++;
                    _petWindow.debugLabel.Content = $"Score: {score}";
                }
                else if (top > GameCanvas.ActualHeight)
                {
                    GameCanvas.Children.Remove(food);
                }
            }
        }
    }
    
    public void End()
    {
        gameTimer.Stop();
        _petWindow.InitFromTemplate(PetWindow.MovementTemplate.DefaultPet);
        Close();
    }
}