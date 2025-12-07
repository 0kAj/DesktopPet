using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopPet.Handlers;
using DesktopPet.MiniGames;

namespace DesktopPet.WPF.WindowViewModels;

public partial class MiniGameViewModel : ObservableObject
{
    [ObservableProperty]
    private int _foodScore;
    
    [ObservableProperty]
    private int _thirstScore;

    protected const int MaxTicksForSpeedMultiplier = 200;
    protected const int SpeedMultiplier = 5;
    private int _tickCounter;

    
    public ObservableCollection<CollectableViewModel> Collectables { get; } = new();

    protected readonly Random Rand = new();

    public event Action? GameFinished;
    public event Action<int>? AddRemainingTime;
    
    private double _canvasWidth;
    private double _canvasHeight;
    private readonly PetBrain _brain;
    
    // SpeedMultiplier * velocity for first MaxTicksForSpeedMultiplier ticks
    public double SpeedFactor => _tickCounter <= MaxTicksForSpeedMultiplier ? 1.0 / SpeedMultiplier : 1.0;

    public MiniGameViewModel(PetBrain brain)
    {
        _brain = brain;
        _brain.InitFromMovementTemplate(PetBrain.MovementTemplate.BasicPetController);
    }

    public void SetCanvasSize(double canvasWidth, double canvasHeight)
    {
        _canvasWidth = canvasWidth;
        _canvasHeight = canvasHeight;
    }

    [RelayCommand]
    private void FinishGame() => GameFinished?.Invoke();
    
    public virtual void Tick()
    {
        _tickCounter++;

        MoveAndCollideCollectables();
    }
    
    protected void CreateFood()
    {
        CreateFood(Rand.Next(0, (int)_canvasWidth - 20), 0, 2);
    }
    
    protected void CreateFood(double posX, double posY, int speed)
    {
        CreateCollectable(CollectableType.FOOD, "pack://application:,,,/Assets/Sprites/Food/apple.png", posX, posY, speed);
    }

    protected void CreateThirst()
    {
        CreateThirst(Rand.Next(0, (int)_canvasWidth - 20), 0, 2);
    }
    
    protected void CreateThirst(double posX, double posY, int speed)
    {
        CreateCollectable(CollectableType.THIRST, "pack://application:,,,/Assets/Sprites/Food/WaterBottle.png", posX, posY, speed);
    }

    protected void CreateCollectable(CollectableType type, string uri, double posX, double posY, int speed)
    {
        var c = new CollectableViewModel()
        {
            Type = type,
            ImageUri = new Uri(uri),
            X = posX,
            Y = posY,
            Speed = speed
        };
        Collectables.Add(c);
    }

    private void MoveAndCollideCollectables()
    {
        for (int i = Collectables.Count - 1; i >= 0; i--)
        {
            var collectable = Collectables[i];
            // move food
            if (_tickCounter < MaxTicksForSpeedMultiplier)
                collectable.Move(collectable.Speed / SpeedFactor);
            else
                collectable.Move(collectable.Speed);

            // collide
            var foodRect = collectable.CollisionRect;
            var playerRect = _brain.PetWindow.GetCollisionRect();

            if (foodRect.IntersectsWith(playerRect))
            {
                // Collision with player
                switch (collectable.Type)
                {
                    case CollectableType.FOOD: FoodScore++; break;
                    case CollectableType.THIRST: ThirstScore++; break;
                }
                AddRemainingTime?.Invoke(1);
                Collectables.Remove(collectable);
            }
            else if (collectable.Y > _canvasHeight)
            {
                // out of screen (bottom)
                Collectables.Remove(collectable);
            }
        }
    }
}